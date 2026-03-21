using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DonKeyNielas.Entities;
namespace DonKeyNielas.Data.Configurations;

public class InviteCodeConfiguration : IEntityTypeConfiguration<InviteCode>
{
    public void Configure(EntityTypeBuilder<InviteCode> builder)
    {
        builder.ToTable("invite_code");
        builder.HasKey(ic => ic.Id);
        builder.Property(ic => ic.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        builder.Property(ic => ic.Code)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("code");
        builder.Property(ic => ic.QtyUsed)
            .IsRequired()
            .HasColumnName("used_qty")
            .HasDefaultValue(0);
        builder.Property(ic => ic.MaxUses)
            .IsRequired()
            .HasColumnName("max_uses")
            .HasDefaultValue(15);
    }
}
