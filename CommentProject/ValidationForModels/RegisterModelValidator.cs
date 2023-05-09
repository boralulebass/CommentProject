using CommentProject.EntityLayer.Concrete;
using CommentProject.Models.AppUserViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.ValidationForModels
{
    public class RegisterModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(X => X.Mail).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.Mail).MinimumLength(7).WithMessage("Minimum 7 karakter girişi yapılmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Minimum 2 karakter girişi yapılmalıdır.");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Minimum 2 karakter girişi yapılmalıdır.");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Maksimum 20 karakter girişi yapılmalıdır.");
            RuleFor(x => x.Surname).MaximumLength(20).WithMessage("Maksimum 20 karakter girişi yapılmalıdır.");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.UserName).MinimumLength(2).WithMessage("Minimum 2 karakter girişi yapılmalıdır.");
            RuleFor(x => x.UserName).MaximumLength(20).WithMessage("Maksimum 20 karakter girişi yapılmalıdır.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifreniz boş bırakılamaz.")
                     .MinimumLength(8).WithMessage("Şifreniz en az 8 karakter içermelidir.")
                     .MaximumLength(16).WithMessage("Şifre uzunluğunuz en fazla 16 olabilir.")
                     .Matches(@"[A-Z]+").WithMessage("Şifreniz en az bir büyük harf içermelidir.")
                     .Matches(@"[a-z]+").WithMessage("Şifreniz en az bir küçük harf içermelidir.")
                     .Matches(@"[0-9]+").WithMessage("Şifreniz en az bir rakam içermelidir.")
                     .Matches(@"[\!\?\*\.]+").WithMessage("Şifreniz en az bir sembol içermelidir (!? *.).");
        }
    }
}
