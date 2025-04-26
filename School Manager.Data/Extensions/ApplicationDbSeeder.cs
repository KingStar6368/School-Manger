using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School_Manager.Data.Context;
using School_Manager.Domain.Entities.Catalog.App;
using School_Manager.Domain.Entities.Catalog.Identity;
using School_Manager.Domain.Entities.Catalog.Operation;

namespace School_Manager.Data.Extensions
{
    public class ApplicationDbSeeder
    {
        private readonly SchMSDBContext _dbContext;

        public ApplicationDbSeeder()
        {
            _dbContext = new SchMSDBContext();
        }
        public ApplicationDbSeeder(SchMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task SeedDatabase()
        {
            // Seed Admin User
            SeedAdminUser();
            SeedWareHouse();
            SeedSupplier();
            SeedPurchaseRequest();
            SeedFiscalYear();
            return Task.CompletedTask;
        }

        private void SeedAdminUser()
        {
            var users = _dbContext.Users.IgnoreQueryFilters().FirstOrDefault();
            if (users == null)
            {
                var _users = new List<User>();
                _users.Add(new User() { UserName = "402321",PasswordHash = EntityHelper.Encrypt("123!@#qwe"), FirstName = "Admin", Email = "admin@gmail.com", LastName = "Admin" });
                _dbContext.Users.AddRange(_users);
                _dbContext.SaveChanges();
            }
        }
        private void SeedWareHouse()
        {
            //var warehouse = _dbContext.Warehouses.IgnoreQueryFilters().FirstOrDefault();
            //if (warehouse == null)
            //{
            //    var _wareHouse = new List<Warehouse>();
            //    _wareHouse.Add(new Warehouse() { PartyRef = 1,Name = "انبار مواد اولیه",Location = "شرکت" });
            //    _dbContext.Warehouses.AddRange(_wareHouse);
            //    _dbContext.SaveChanges();
            //}
        }
        private void SeedSupplier()
        {
            //var supplier = _dbContext.Suppliers.IgnoreQueryFilters().FirstOrDefault();
            //if (supplier == null)
            //{
            //    var _supplier = new List<Supplier>();
            //    _supplier.Add(new Supplier() { 
            //        Address = "آدرس",
            //        Name = "تامین کننده",
            //        Email = " ",
            //        PhoneNumber = "123",
            //        ContactPerson = "پاسخگو" 
            //    });
            //    _dbContext.Suppliers.AddRange(_supplier);
            //    _dbContext.SaveChanges();
            //}
        }
        private void SeedPurchaseRequest()
        {
            //var purchaseRequest = _dbContext.PurchaseRequests.IgnoreQueryFilters().FirstOrDefault();
            //if (purchaseRequest == null)
            //{
            //    var _purchaseRequest = new List<PurchaseRequest>();
            //    _purchaseRequest.Add(new PurchaseRequest() { RequestDate = DateTime.Now,Status = 0,UserRef = 1 });
            //    _dbContext.PurchaseRequests.AddRange(_purchaseRequest);
            //    _dbContext.SaveChanges();
            //}
        }
        private void SeedFiscalYear()
        {
            //var fiscalYear = _dbContext.FiscalYears.IgnoreQueryFilters().FirstOrDefault();
            //if (fiscalYear == null)
            //{
            //    var _fiscalYear = new List<FiscalYear>();
            //    _fiscalYear.Add(new FiscalYear()
            //    {
            //        CreatedBy = 1,
            //        CreatedOn = DateTime.Now,
            //        EndDate = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        StartDate = DateTime.Now,
            //        YearName = "1403"
            //    });
            //    _dbContext.FiscalYears.AddRange(_fiscalYear);
            //    _dbContext.SaveChanges();
            //}
        }
    }
}
