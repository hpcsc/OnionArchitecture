using System.Collections.Generic;
namespace OnionArchitecture.Services.Interfaces.Common.DTO
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ResourceDTO> Children { get; set; }
    }
}
