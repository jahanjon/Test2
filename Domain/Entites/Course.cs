using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Course
    {
        public int Id { get; set; }
        public string Banner { get; set; }
        public BannerType BannerType { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string RoyeshLink { get; set; }
        public string Centers { get; set; }
        public string Demo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TimeLine { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CoachId { get; set; }
        public Coach Coach { get; set; }
        public int SubCategoryId { get; set; }

        public SubCategory SubCategory { get; set; }
        public ICollection<CourseTitle> CourseTitles { get; set; }
      
        public ICollection<UserCourse> UserCourses { get; set; }


    }
    public enum  BannerType
    {
        Photo,
        Video
    }
}

