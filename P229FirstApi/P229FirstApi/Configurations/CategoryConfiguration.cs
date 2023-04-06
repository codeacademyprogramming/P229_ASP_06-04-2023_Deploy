using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P229FirstApi.Entities;

namespace P229FirstApi.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.ToTable("Nov");

            //builder.HasKey(r => r.asdasd);

            //builder.Property(r=>r.asdasd).IsRequired().UseIdentityColumn();

            builder.Property(b=>b.CreatedBy).HasMaxLength(50).HasDefaultValue("System");
            builder.Property(b=>b.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(b => b.Name).HasMaxLength(50);

            //One To Many
            //builder.HasOne(b=>b.elebe).WithMany(b=>b.coxluq).HasForeignKey(b=>b.test).OnDelete(DeleteBehavior.Restrict);

            //One To One
            //builder.HasOne(b => b.One).WithOne(b => b.Category).HasForeignKey<Category>(b => b.OneId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
