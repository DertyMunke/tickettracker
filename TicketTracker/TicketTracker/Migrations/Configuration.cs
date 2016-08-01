namespace TicketTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TicketTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Adds users with roles to login to the web app
        /// </summary>
        bool AddUserAndRole(TicketTracker.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("admin"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "admin1@email.com",
            };
            ir = um.Create(user, "admin1pswrd");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "admin");
            return ir.Succeeded;
        }

        /// <summary>
        /// Creates default data on building new versions
        /// </summary>
        protected override void Seed(TicketTracker.Models.ApplicationDbContext context)
        {
            // Adds an admin user to the web app
            AddUserAndRole(context);

            //  This method will be called after migrating to the latest version.
            context.Tickets.AddOrUpdate(p => p.Title,
                new Ticket
                {
                    Title = "Broken",
                    Description = "The whole thing broke.",
                    TicketStatus = TicketTypes.active,
                    CreatorEmail = "a@email.com",
                    ResolverEmail = "",
                },
                new Ticket
                {
                    Title = "The thing",
                    Description = "It did stuff.",
                    TicketStatus = TicketTypes.resolved,
                    CreatorEmail = "b@email.com",
                    ResolverEmail = "c@email.com",
                },
                new Ticket
                {
                    Title = "Gears",
                    Description = "There is no more grease on the gears.",
                    TicketStatus = TicketTypes.active,
                    CreatorEmail = "d@email.com",
                    ResolverEmail = "",
                },
                new Ticket
                {
                    Title = "Crashing",
                    Description = "The login keeps crashing.",
                    TicketStatus = TicketTypes.resolved,
                    CreatorEmail = "e@email.com",
                    ResolverEmail = "b@email.com",
                }

                );
        }


    }
}
