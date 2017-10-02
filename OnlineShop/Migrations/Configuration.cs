namespace OnlineShop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineShop.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #region Add Roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = ApplicationUser.AdminRoleName });
                roleManager.Create(new IdentityRole { Name = ApplicationUser.UserRoleName });
            }
            #endregion

            #region Add Admin
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser()
            {
                UserName = "admin",
                Email = "tranviettien1996@gmail.com",
                EmailConfirmed = true,
                FirstName = "Tran",
                LastName = "Viet Tien",
                JoinDate = DateTime.Now,
                Address = "776/42 Phạm Văn Bạch"
                
            };

            manager.Create(user, "Abc123456!");

            var result = manager.Find(user.UserName, "Abc123456!"); 
            manager.AddToRole(result.Id, "Admin");
            #endregion

            context.SaveChanges();
        }

        #region Add User
        public ApplicationUser AddUser(OnlineShop.Models.ApplicationDbContext context, string userName, string password, string role)
        {
            userName = string.IsNullOrEmpty(userName) ? Guid.NewGuid().ToString().Replace("-", "") : userName;

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = userName
            };

            var resultCreate = manager.Create(user, password);

            var result = manager.FindByName(userName);

            manager.AddToRoles(result.Id, new string[] { role });

            return result;
        }
        #endregion
    }
}
