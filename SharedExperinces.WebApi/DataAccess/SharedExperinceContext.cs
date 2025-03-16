﻿using Microsoft.EntityFrameworkCore;
using SharedExperinces.WebApi.Models;
using System.Reflection;

namespace SharedExperinces.WebApi.DataAccess
{
	public class SharedExperinceContext : DbContext
	{
        public SharedExperinceContext(DbContextOptions<SharedExperinceContext> options) 
			: base(options) { }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SharedExperience> SharedExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public static void Seed(SharedExperinceContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Providers.Any())
            {
                context.Providers.AddRange(
                    new Provider { CVR = "11111114", Name = "Noah's Hotel", Address = "Finlandsgade 17, 8200 Aarhus N", PhoneNumber = "+45 71555080" },
                    new Provider { CVR = "22222222", Name = "Grand Ocean Resort", Address = "Beach Road 42, 8000 Aarhus C", PhoneNumber = "+45 71717171" },
                    new Provider { CVR = "33333333", Name = "Skyline Adventures", Address = "Mountain View 99, 9000 Aalborg", PhoneNumber = "+45 70707070" },
                    new Provider { CVR = "44444444", Name = "Sunset Bistro", Address = "Harbor Street 12, 5000 Odense", PhoneNumber = "+45 72727272" },
                    new Provider { CVR = "55555555", Name = "City Tour Guides", Address = "Old Town Square 3, 1000 Copenhagen", PhoneNumber = "+45 73737373" }
                );
                context.SaveChanges();
            }


			var service1 = new Service { Name = "Night at Noah's Hotel Single Room", Description = "A cozy single room at Noah's Hotel.", Price = 730.50M, ServiceDate = new DateTime(2024, 6, 15), CVR = "11111114" };
			var service2 = new Service { Name = "Night at Noah's Hotel Double Room", Description = "A spacious double room at Noah's Hotel.", Price = 910.99M, ServiceDate = new DateTime(2024, 6, 15), CVR = "11111114" };
			var service3 = new Service { Name = "Flight AAR – VIE", Description = "One-way flight from Aarhus (AAR) to Vienna (VIE).", Price = 1000.70M, ServiceDate = new DateTime(2024, 7, 1), CVR = "33333333" };
			var service4 = new Service { Name = "Vienna Historic Center Walking Tour", Description = "Guided walking tour of Vienna's historic center.", Price = 100.00M, ServiceDate = new DateTime(2024, 7, 2), CVR = "55555555" };


			var sharedExperince1 = new SharedExperience { Name = "Trip to Austria", Description = "A group trip exploring Vienna, including flights, hotel stays, and guided tours." };

			var sharedExperience2 = new SharedExperience { Name = "Dinner Downtown", Description = "A fine dining experience at a highly-rated restaurant in the city center." };


            sharedExperince1.Services.Add(service1);
			sharedExperince1.Services.Add(service3);
			sharedExperince1.Services.Add(service4);

            service1.SharedExperiences.Add(sharedExperince1);
			service3.SharedExperiences.Add(sharedExperince1);
			service4.SharedExperiences.Add(sharedExperince1);

			if (!context.Services.Any())
            {


				context.Services.AddRange(
                  service1,
                  service2,
				  service3,
				  service4
				);
                context.SaveChanges();
            }

			

			if (!context.SharedExperiences.Any())
            {


				context.SharedExperiences.AddRange(
                    sharedExperince1,
                    sharedExperience2
                );
                context.SaveChanges();
            }

            if (!context.Guests.Any())
            {
                context.Guests.AddRange(
                    new Guest {  Name = "Joan Eriksen", Number = "+45 11113333", Age = 27 },
                    new Guest { Name = "Suzanne Mortensen", Number = "+45 22224444", Age = 29 },
                    new Guest { Name = "Patrick Larsen", Number = "+45 33335555", Age = 32 },
                    new Guest { Name = "Anne Christensen", Number = "+45 44446666", Age = 26 }
                );
                context.SaveChanges();
            }

            if (!context.Registrations.Any())
            {
                context.Registrations.AddRange(
                    new Registration { GuestId = 1, ServiceId = 4 },
                    new Registration {  GuestId = 2, ServiceId = 4 },
                    new Registration {  GuestId = 1, ServiceId = 1 },
                    new Registration {  GuestId = 2, ServiceId = 1 }
                );
                context.SaveChanges();
            }

            if (!context.Discounts.Any())
            {
                context.Discounts.AddRange(
                    new Discount { ServiceId = 1, GuestCount = 10, Percentage = 10.00 },
                    new Discount { ServiceId = 1, GuestCount = 50, Percentage = 20.00 },
                    new Discount { ServiceId = 2, GuestCount = 10, Percentage = 10.00 },
                    new Discount { ServiceId = 2, GuestCount = 50, Percentage = 20.00 }
                );
                context.SaveChanges();
            }
        }
    }
}

