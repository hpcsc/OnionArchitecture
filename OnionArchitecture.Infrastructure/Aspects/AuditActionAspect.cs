using OnionArchitecture.Core.Infrastructure.Auditing;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnionArchitecture.Infrastructure.Aspects
{
    [Serializable]
    public sealed class AuditActionAttribute : OnMethodBoundaryAspect
    {
        public static Func<IAuditRepository> AuditRepositoryFactory { get; set; }
        public static Func<IUnitOfWork> UnitOfWorkFactory { get; set; }

        public string Action { get; set; }
        public Type AuditorType { get; set; }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var argument = args.Arguments.First();
            if (!(argument is IAuditable))
            {
                throw new ArgumentException("Input for method to be audited must implement IAuditable");
            }


            var auditRepository = AuditRepositoryFactory();
            var unitOfWork = UnitOfWorkFactory();
            
            var audit = new Audit
            {
                Action = Action,
                Time = DateTime.Now,
                UserId = (argument as IAuditable).UserId,
                AuditedValues = GetAuditedValues(argument)
            };

            auditRepository.Add(audit);
            unitOfWork.Commit();
        }

        private List<AuditedValue> GetAuditedValues(object argument)
        {
            var auditorType = AuditorType ?? typeof(PublicPropertiesAuditor<>);
            var auditorGenericType = auditorType.MakeGenericType(argument.GetType());
            var auditor = Activator.CreateInstance(auditorGenericType);

            var auditMethod = auditorGenericType.GetMethod("Audit");
            return (List<AuditedValue>)auditMethod.Invoke(auditor, new object[] { argument });
        }
    }
}
