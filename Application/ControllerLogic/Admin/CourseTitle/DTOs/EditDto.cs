using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerLogic.Admin.CourseTitle.DTOs
{
    public record EditDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
    public class EditDtoValidator : AbstractValidator<EditDto>
    {
        public EditDtoValidator()
        {

            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("عنوان الزامی است.")
                .MaximumLength(255).WithMessage("عنوان نمی‌تواند بیشتر از 255 کاراکتر باشد.");

            RuleFor(dto => dto.Description).NotEmpty()
                .MaximumLength(1000).WithMessage("توضیحات نمی‌تواند بیشتر از 1000 کاراکتر باشد.");

            RuleFor(dto => dto.CourseId).NotEmpty()
                .GreaterThanOrEqualTo(0).WithMessage("شناسه دوره باید بزرگتر یا مساوی با صفر باشد.");
        }
    }
}
