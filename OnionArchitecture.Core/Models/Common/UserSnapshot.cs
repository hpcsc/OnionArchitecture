namespace OnionArchitecture.Core.Models.Common
{
    public class UserSnapshot
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public UserStatus Status { get; set; }
    }
}
