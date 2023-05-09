using CommentProject.EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.BusinessLayer.ValidationRules
{
    public class TitleAddValidator : AbstractValidator<Title>
    {
        public TitleAddValidator() 
        {
            RuleFor(x=>x.TitleName).NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
            RuleFor(x => x.TitleName).MaximumLength(70).WithMessage("Maksimum 70 karakter girişi yapılmalıdır");
            RuleFor(x => x.TitleName).MinimumLength(2).WithMessage("Minimum 2 karakter girişi yapılmalıdır");
        }
    }
}
