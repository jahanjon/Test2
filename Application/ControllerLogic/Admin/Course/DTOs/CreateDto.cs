using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerLogic.Admin.Course.DTOs
{
    public class CreateDto
    {
        public int CoachId { get; set; }
        public int SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public string RoyeshLink { get; set; }
        public string Centers { get; set; }
        public string Demo { get; set; }
        public BannerType BannerType { get; set; }
        public string Url { get; set; }
        public string TimeLine { get; set; }
    }
    public class CreateDtoValidator : AbstractValidator<CreateDto>
    {
        public CreateDtoValidator()
        {
            RuleFor(dto => dto.Banner)
                .NotEmpty().WithMessage("تصویر بنر اجباری است.")
                .MaximumLength(1000).WithMessage("مسیر تصویر بنر نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد.");

            RuleFor(dto => dto.BannerType)
                .IsInEnum().WithMessage("نوع بنر نامعتبر است.");

            RuleFor(dto => dto.Url)
                .NotEmpty().WithMessage("آدرس اینترنتی الزامی میباشد")
                .MaximumLength(500).WithMessage("آدرس اینترنتی نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد.");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("توضیحات  اجباری است.")
                .MaximumLength(3000).WithMessage("توضیحات نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد.");

            RuleFor(dto => dto.RoyeshLink)
               
                 .MaximumLength(1000).WithMessage("توضیحات نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد.");

            RuleFor(dto => dto.Title)
                .NotEmpty().WithMessage("عنوان اجباری است.")
                .MaximumLength(100).WithMessage("عنوان نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.");


            RuleFor(dto => dto.TimeLine)
                .NotEmpty().WithMessage("مسیر زمان بندی  اجباری است.")
                .MaximumLength(1000).WithMessage("مسیر زمان‌بندی نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد.");

            RuleFor(dto => dto.CoachId)
                .NotEmpty().WithMessage(" شناسه مربی اجباری است.")
                .GreaterThan(0).WithMessage("شناسه مربی نامعتبر است.");

            RuleFor(dto => dto.SubCategoryId)
                .NotEmpty().WithMessage("شناسه مجموعه اجباری است.")
                 .GreaterThan(0).WithMessage("شناسه مجموعه نامعتبر است.");
        }
    }
}
