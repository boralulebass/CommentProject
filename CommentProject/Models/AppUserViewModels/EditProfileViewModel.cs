namespace CommentProject.Models.AppUserViewModels
{
    public class EditProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
