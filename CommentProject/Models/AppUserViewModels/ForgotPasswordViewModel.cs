using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace CommentProject.Models.AppUserViewModels
{
    public class ForgotPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
