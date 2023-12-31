using EduArk.Domain.Entities.Tenant;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduArk.Infrastructure.Tenant.Common;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class SubjectAcademicLevelConfiguration : IEntityTypeConfiguration<SubjectAcademicLevel>
    {
        public void Configure(EntityTypeBuilder<SubjectAcademicLevel> builder)
        {
            //Set SubjectAcademicLevel Table Name
            builder.ToTable(EntityConstants.SUBJECT_ACADEMIC_LEVEL_ENTITY_TABLE_NAME);

            //Set SubjectAcademicLevel Table Primary Key
            builder.HasKey(x => new { x.SubjectId, x.AcademicLevelId });

            //For Nullable Relationship with SubjectAcademicLevel Table for Subject
            builder.HasOne<Subject>(s => s.Subject)
                   .WithMany(sa => sa.SubjectAcademicLevels)
                   .HasForeignKey(f => f.SubjectId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For  Relationship with SubjectAcademicLevel Table for AcademicLevel
            builder.HasOne<AcademicLevel>(a => a.AcademicLevel)
                   .WithMany(sa => sa.SubjectAcademicLevels)
                   .HasForeignKey(f => f.AcademicLevelId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

        }
    }
}
