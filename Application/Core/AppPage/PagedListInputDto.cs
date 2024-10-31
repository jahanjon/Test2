using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.AppPage
{
    //public class PagedListInputDto
    //{

    //    public int PageNumber { get; set; }
    //    public int ItemsCountPerPage { get; set; }
    //}

    //public class PagedListInputDtoValidator : AbstractValidator<PagedListInputDto>
    //{
    //    public PagedListInputDtoValidator()
    //    {
    //        RuleFor(x => x.PageNumber)
    //            .GreaterThan(0).WithMessage("شماره صفحه باید بزرگتر از 0 باشد");
    //        RuleFor(x => x.ItemsCountPerPage)
    //            .GreaterThan(0).WithMessage("تعداد ایتم ها در صفحه باید بیشتر از 0 باشد")
    //            .LessThanOrEqualTo(200).WithMessage("تعداد ایتم ها در صفحه باید کمتر یا مساوی با 200 باشد");
    //    }
    //}
}
