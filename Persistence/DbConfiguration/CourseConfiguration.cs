using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasIndex(x=>x.Url).IsUnique();

            builder.HasOne(x => x.Coach)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CoachId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.SubCategory)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
