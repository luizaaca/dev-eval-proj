using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.Infrastructure.Mapping;

public class SaleMapping : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Date)
            .IsRequired();

        builder.Property(s => s.CustomerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.BranchName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Ignore(s => s.TotalAmount);

        builder.Property(s => s.Status).HasConversion<string>().HasMaxLength(20).IsRequired();

        builder.HasMany(s => s.Items)
            .WithOne(i => i.Sale)
            .HasForeignKey(i => i.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}