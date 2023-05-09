using System.ComponentModel.DataAnnotations;

namespace CommentProject.Models.AppUserViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string PasswordNew { get; set; }

        public string PasswordNewConfirm { get; set; }
    }
}
