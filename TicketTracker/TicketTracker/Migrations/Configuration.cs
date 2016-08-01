namespace TicketTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TicketTracker.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketTracker.Models.ApplicationDbContext context)
        {
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
