using System.Collections.Generic;

namespace LeLeInstitute.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<RoleViewModel> UserInRoles { get; set; }
    }
}