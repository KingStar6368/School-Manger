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
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(p => p.Price)
                .HasComment("مبلغ");

            builder.HasMany(d => d.PayBills).WithOne(p => p.BillNavigation)
            .HasForeignKey(f => f.BillRef)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p=>p.ServiceContractNavigation).WithMany(b=>b.BillNavigation)
                .HasForeignKey(fk=>fk.ServiceContractRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x=>!x.IsDeleted);
        }
    }
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(p => p.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired()
            .HasComment("نام ماشین");

            builder.Property(e => e.carType)
            .HasConversion<int>();

            builder.HasOne(d => d.DriverNavigation).WithMany(p => p.Cars)
            .HasForeignKey(f => f.DriverRef)
            .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class ChequeConfig : IEntityTypeConfiguration<Cheque>
    {
        public void Configure(EntityTypeBuilder<Cheque> builder)
        {
            builder.Property(p => p.CheckSerial)
            .HasColumnType("nvarchar(24)")
            .IsRequired()
            .HasComment("سریال چک");

            builder.Property(p => p.CheckSayadNumber)
                .HasColumnType("nvarchar(24)")
                .HasComment("شناسه صیاد");

            //builder.HasMany(d => d.).Withone(p => p.Cheques)
            //            .HasForeignKey(f => f.ServiceContractRef)
            //            .OnDelete(DeleteBehavior.Restrict);
            //builder.HasQueryFilter(x => !x.IsDeleted);

        }
    }
    public class ChildConfig : IEntityTypeConfiguration<Child>
    {
        public void Configure(EntityTypeBuilder<Child> builder)
        {
            builder.Property(p => p.FirstName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام");

            builder.Property(p => p.LastName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام خانوادگی");

            builder.Property(p => p.NationalCode)
            .HasColumnType("nvarchar(11)")
            .IsRequired()
            .HasComment("کد ملی");

            builder.Property(p => p.Class)
            .HasConversion<int>();

            builder.HasMany(d => d.DriverChilds).WithOne(p => p.ChildNavigation)
                .HasForeignKey(f => f.ChildRef)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ParentNavigation).WithMany(p => p.Children)
                .HasForeignKey(fk => fk.ParentRef)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p=>p.LocationPairs).WithOne(d=>d.ChildNavigation)
                .HasForeignKey(fk=>fk.ChildRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class DriverConfig : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.Property(p => p.Name)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام");

            builder.Property(p => p.LastName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام خانوادگی");

            builder.Property(p => p.NationCode)
            .HasColumnType("nvarchar(11)")
            .IsRequired()
            .HasComment("کد ملی");

            builder.Property(p => p.FatherName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام پدر");

            builder.HasMany(d => d.Passanger).WithOne(p => p.DriverNavigation)
                .HasForeignKey(f => f.DriverRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class DriverContractConfig : IEntityTypeConfiguration<DriverContract>
    {
        public void Configure(EntityTypeBuilder<DriverContract> builder)
        {
            builder.HasMany(d => d.DriverContractCheques).WithOne(p => p.DriverContractNavigation)
                .HasForeignKey(f => f.DriverContractRef)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.DriverNavigation).WithMany(p => p.DriverContracts)
                .HasForeignKey(f => f.DriverRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class DriverContractChequeConfig : IEntityTypeConfiguration<DriverContractCheque>
    {
        public void Configure(EntityTypeBuilder<DriverContractCheque> builder)
        {
            builder.HasOne(d=>d.ChequeNavigation).WithMany(p=>p.DriverContractCheques)
                .HasForeignKey(fk=>fk.DriverContractRef)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class LocationDataConfig : IEntityTypeConfiguration<LocationData>
    {
        public void Configure(EntityTypeBuilder<LocationData> builder)
        {
            builder.Property(p => p.LocationType)
            .HasConversion<int>();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class LocationPairConfig : IEntityTypeConfiguration<LocationPair>
    {
        public void Configure(EntityTypeBuilder<LocationPair> builder)
        {
            //builder.HasOne(d => d.ChildNavigation).WithOne(p => p.PathNavigation)
            //.HasForeignKey<Child>(f => f.LocationPairRef)
            //.OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p=>p.Locations).WithOne(d=>d.LocationPairNavigation)
                .HasForeignKey(fk=>fk.LocationPairRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
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
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class ParentConfig : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.Property(p => p.FirstName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام");

            builder.Property(p => p.LastName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام خانوادگی");

            builder.Property(p => p.NationalCode)
            .HasColumnType("nvarchar(11)")
            .IsRequired()
            .HasComment("کد ملی");
            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
    public class PayConfig : IEntityTypeConfiguration<Pay>
    {
        public void Configure(EntityTypeBuilder<Pay> builder)
        {
            builder.HasMany(b => b.PayBills).WithOne(p => p.PayNavigation)
                .HasForeignKey(fk => fk.PayRef)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.PayType)
            .HasConversion<int>();
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class PayBillConfig : IEntityTypeConfiguration<PayBill>
    {
        public void Configure(EntityTypeBuilder<PayBill> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);
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
            builder.HasQueryFilter(x => !x.IsDeleted);

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
    public class SchoolConfig : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(p => p.Name)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام مدرسه");

            builder.Property(p => p.ManagerName)
            .HasColumnType("nvarchar(128)")
            .IsRequired()
            .HasComment("نام مدیر مدرسه");

            builder.HasMany(b=>b.Childs)
                .WithOne(p=>p.SchoolNavigation)
                .HasForeignKey(p=>p.SchoolRef)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(p => p.AddressNavigation).WithMany(b=>b.)

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class ServiceContractConfig : IEntityTypeConfiguration<ServiceContract>
    {
        public void Configure(EntityTypeBuilder<ServiceContract> builder)
        {
            builder.HasMany(p => p.ServiceContractCheques).WithOne(b => b.ServiceContractNavigation)
                .HasForeignKey(fk => fk.ServiceContractRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(p => p.ChildNavigation).WithMany(b => b.ServiceContracts)
                .HasForeignKey(fk => fk.ChildRef)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
    public class ServiceContractChequeConfig : IEntityTypeConfiguration<ServiceContractCheque>
    {
        public void Configure(EntityTypeBuilder<ServiceContractCheque> builder)
        {
            builder.HasOne(d => d.ChequeNavigation).WithMany(p => p.ServiceContractCheques)
                .HasForeignKey(fk => fk.ServiceContractRef)
                .OnDelete(DeleteBehavior.Restrict);
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


            builder.Property(p => p.FirstName)
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasComment("نام");

            builder.Property(p => p.LastName)
                .HasColumnType("nvarchar(50)")
                .IsRequired()
                .HasComment("نام خانوادگی");

            builder.HasMany(p => p.Parents).WithOne(b => b.UserNavigation)
                .HasForeignKey(fk => fk.UserRef)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p=>p.Drivers).WithOne(b => b.UserNavigation)
                .HasForeignKey(fk => fk.UserRef)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted);
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
}
