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
    public class CourseTitleConfiguration : IEntityTypeConfiguration<CourseTitle>
    {
        public void Configure(EntityTypeBuilder<CourseTitle> builder)
        {
           builder.HasOne(x=>x.Course)
                .WithMany(x=>x.CourseTitles)
                .HasForeignKey(x=>x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
