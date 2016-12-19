using Newtonsoft.Json;
using OnionArchitecture.Core.Models.Common;
using System.Collections.Generic;
using System.Reflection;

namespace OnionArchitecture.Core.Infrastructure.Auditing
{
    public class PublicPropertiesAuditor<T> : IAuditor<T>
    {
        private PropertyInfo[] _properties;

        public PublicPropertiesAuditor()
        {
            _properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public List<AuditedValue> Audit(T entity)
        {
            var values = new List<AuditedValue>();

            foreach (var p in _properties)
            {
                if (!p.CanRead) continue;

                var getter = p.GetGetMethod(false);
                if (getter == null) continue;

                var value = p.GetValue(entity, null);
                
                values.Add(new AuditedValue
                    {
                        Name = p.Name,
                        Value = value == null ? 
                            string.Empty : 
                            Serialize(value)
                    });
            }

            return values;
        }

        private string Serialize(object value)
        {
            var valueType = value.GetType();
            if(valueType.IsValueType || value is string)
            {
                return value.ToString();
            }

            return JsonConvert.SerializeObject(value);
        }
    }
}
