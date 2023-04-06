using System.ComponentModel.DataAnnotations;

namespace P229Allup.Areas.Manage.ViewModels.AccountViewModels
{
    public class LoginVM
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemindMe { get; set; }
    }
}
