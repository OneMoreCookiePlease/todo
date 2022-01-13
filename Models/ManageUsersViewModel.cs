using Microsoft.AspNetCore.Identity;

namespace Todo.Models
{
    public class ManageUsersViewModel
    {
        public IdentityUser[] Administrators { get; set; }
        public IdentityUser[] Everyone { get; set; }

        public ManageUsersViewModel(IdentityUser[] admins, IdentityUser[] all) {
            Administrators = admins;
            Everyone = all;
        }

    }
}