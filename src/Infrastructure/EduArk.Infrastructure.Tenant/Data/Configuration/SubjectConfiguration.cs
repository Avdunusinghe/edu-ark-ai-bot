using EduArk.Domain.Entities.Tenant;
using EduArk.Infrastructure.Tenant.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduArk.Infrastructure.Tenant.Data.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            //Set Subject Table Name
            builder.ToTable(EntityConstants.SUBJECT_ENTITY_TABLE_NAME);

            //Set Subject Table Primary Key
            builder.HasKey(x => x.Id);

            //Set Subject Table AlternateKey
            builder.HasAlternateKey(x => x.SubjectCode);

            //Set Subject Table AlternateKey
            builder.HasAlternateKey(x => x.Name);

            //For Nullable Relationship with Subject Table for PerentSubject
            builder.HasOne<Subject>(x => x.PerentSubject)
                   .WithMany(c => c.ChildBasketSubjects)
                   .HasForeignKey(f => f.ParentBasketSubjectId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);

            //For  Relationship with Subject Table for SubjectStream
            builder.HasOne<SubjectStream>(x => x.SubjectStream)
                   .WithMany(s => s.Subjects)
                   .HasForeignKey(f => f.SubjectStreamId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For  Relationship with Subject Table for CreatedByUser
            builder.HasOne<User>(u => u.CreatedByUser)
                   .WithMany(s => s.CreatedSubjects)
                   .HasForeignKey(f => f.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);

            //For  Relationship with Subject Table for UpdatedByUser
            builder.HasOne<User>(u => u.UpdatedByUser)
                   .WithMany(s => s.UpdatedSubjects)
                   .HasForeignKey(f => f.UpdatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true);
        }
    }
}
