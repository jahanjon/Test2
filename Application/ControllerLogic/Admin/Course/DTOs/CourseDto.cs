using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ControllerLogic.Admin.Course.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Banner { get; set; }
        public BannerType BannerType { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string RoyeshLink { get; set; }
        public string TimeLine { get; set; }
        public string Centers { get; set; }
        public string Demo { get; set; }
        public bool IsHidden { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CoachId { get; set; }
        public Domain.Coach Coach { get; set; }
        public int SubCategoryId { get; set; }
        public Domain.SubCategory SubCategory { get; set; }
    }
}
