using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Domain.Entities.Catalog.App;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(p => p.UserName).IsUnique();
            builder.Property(p => p.IsActive).HasDefaultValue(true);
            builder.Property(p => p.UserName)
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasComment("نام کاربری");

            builder.Property(p => p.PasswordHash)
                .HasColumnType("nvarchar(max)")
                .IsRequired()
                .HasComment("کلمه عبور");

            builder.Property(p => p.Email)
                .HasColumnType("nvarchar(100)")
                .HasComment("ایمیل");

            builder.Property(p => p.FirstName)
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasComment("نام");

            builder.Property(p => p.LastName)
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasComment("نام خانوادگی");
            // Relations
            //builder.HasMany(d => d.<<relatedList>>).WithOne(p => p.UserNavigation)
            //.HasForeignKey(f => f.CreatedBy)
            //.OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(d => d.Outgoings).WithOne(p => p.UserNavigation)
            //.HasForeignKey(f => f.CreatedBy)
            //.OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(d => d.PurchaseRequests).WithOne(p => p.UserNavigation)
            //.HasForeignKey(f => f.UserRef)
            //.HasConstraintName("FK_User_PurchaseRequests")
            //.OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(d => d.UserRoles).WithOne(p => p.UserNavigation)
            //.HasForeignKey(f => f.UserRef)
            //.HasConstraintName("FK_User_UserRoles")
            //.OnDelete(DeleteBehavior.Restrict);

        }
    }


    public class LookupConfig : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.Property(p => p.Type)
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasComment("نوع");

            builder.Property(p => p.Value)
                .HasColumnType("nvarchar(100)")
                .IsRequired()
                .HasComment("مقدار");

            builder.HasIndex(p => new { p.Type, p.Code })
                .IsUnique(true);
        }
    }
    
    public class RawMaterialConfig : IEntityTypeConfiguration<RawMaterial>
    {
        public void Configure(EntityTypeBuilder<RawMaterial> builder)
        {
            builder.Property(p => p.MaterialCode)
            .HasColumnType("nvarchar(50)")
            .IsRequired()
            .HasComment("کد کالا");

            builder.Property(p => p.Name)
            .HasColumnType("nvarchar(100)")
            .IsRequired()
            .HasComment("نام کالا");

            builder.Property(p => p.Description)
            .HasColumnType("nvarchar(500)")
            .HasComment("توضیحات");

            builder.Property(p => p.MajorUnitRef)
            .IsRequired()
            .HasComment("واحد اصلی");

            builder.Property(p => p.SecondaryUnitRef)
            .HasComment("واحد فرعی");

            builder.Property(p => p.TechnicalSpecification)
            .HasColumnType("nvarchar(256)")
            .HasComment("مشخصه فنی");

           // builder.HasMany(p => p.IncomingDetails).WithOne(p => p.RawMaterialNavigation)
           //.HasForeignKey(p => p.MaterialRef)
           //.HasConstraintName("FK_RawMaterial_IncomingDetails")
           //.OnDelete(DeleteBehavior.Restrict);

           // builder.HasMany(p => p.Inventories).WithOne(p => p.RawMaterialNavigation)
           //.HasForeignKey(p => p.MaterialRef)
           //.HasConstraintName("FK_RawMaterial_Inventories")
           //.OnDelete(DeleteBehavior.Restrict);

           // builder.HasMany(p => p.InventoryHistories).WithOne(p => p.RawMaterialNavigation)
           //.HasForeignKey(p => p.MaterialRef)
           //.HasConstraintName("FK_RawMaterial_InventoryHistories")
           //.OnDelete(DeleteBehavior.Restrict);

           // builder.HasMany(p => p.OutgoingDetails).WithOne(p => p.RawMaterialNavigation)
           //.HasForeignKey(p => p.MaterialRef)
           //.HasConstraintName("FK_RawMaterial_OutgoingDetails")
           //.OnDelete(DeleteBehavior.Restrict);

           // builder.HasMany(p => p.PurchaseRequestDetails).WithOne(p => p.RawMaterialNavigation)
           //.HasForeignKey(p => p.MaterialRef)
           //.HasConstraintName("FK_RawMaterial_PurchaseRequestDetails")
           //.OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class TrialConfig : IEntityTypeConfiguration<Trail>
    {    
        public void Configure(EntityTypeBuilder<Trail> builder) 
        {
            builder.ToTable("AuditTrials", SchemaNames.Dbo);            
        }

        //TODO:
    }
}
