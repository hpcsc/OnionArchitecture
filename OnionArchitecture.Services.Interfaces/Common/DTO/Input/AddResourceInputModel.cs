
namespace OnionArchitecture.Services.Interfaces.Common.DTO.Input
{
    public class AddResourceInputModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
