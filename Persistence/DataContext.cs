using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
  
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTitle> CourseTitle { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder?.ApplyConfiguration<NewsLetter>(new NewsLetterConfiguration());
            builder?.ApplyConfiguration<CourseTitle>(new CourseTitleConfiguration());


        }
    }
}
