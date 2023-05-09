using CommentProject.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.BusinessLayer.ValidationRules
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.CommentDetails).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.CommentDetails).MaximumLength(500).WithMessage("Maksimum 500 karakter girişi yapılabilir");
            RuleFor(x => x.CommentDetails).MinimumLength(2).WithMessage("Minimum 2 karakter girişi yapılmalıdır");
        }
    }
}
