using System;
using System.Collections.Generic;

namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    [Serializable]
    public class CustomPrincipalSerializationModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<string> Roles { get; set; }
    }
}
