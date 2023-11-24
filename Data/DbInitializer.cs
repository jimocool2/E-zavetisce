using E_zavetisce.Data;
using E_zavetisce.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace E_zavetisce.Data
{
    public static class DbInitializer
    {
        private static Pet MakePet(string name, string type)
        {
            return new Pet { Name = name, Type = type, DateAdded = DateTime.Parse("2023-10-01") };
        }

        private static Client MakeClient(string fmname, string lname)
        {
            return new Client { FirstMidName = fmname, LastName = lname, DateJoined = DateTime.Parse("2023-09-01") };
        }
        private static Employee MakeEmployee(string fmname, string lname)
        {
            return new Employee { FirstMidName = fmname, LastName = lname, HireDate = DateTime.Parse("2021-09-01") };
        }
        public static void Initialize(ZavetisceContext context)
        {
            context.Database.EnsureCreated();

            // Look for any pets.
            if (context.Pets.Any())
            {
                return;   // DB has been seeded
            }

            var pets = new Pet[]
            {
                MakePet("Jaka", "Ptič"),
                MakePet("Sky", "Pes"),
                MakePet("Muc", "Maček")
            };
            context.Pets.AddRange(pets);
            context.SaveChanges();

            var clients = new Client[]
            {
                MakeClient("Karen", "Smith"),
                MakeClient("Li", "Wohn")
            };
            context.Clients.AddRange(clients);
            context.SaveChanges();

            var employees = new Employee[]
            {
                MakeEmployee("Sam", "Jonson"),
                MakeEmployee("Anna", "Bell")
            };
            context.Employees.AddRange(employees);
            context.SaveChanges();

            var roles = new IdentityRole[]{
                new IdentityRole{Id="1", Name="Employee"},
                new IdentityRole{Id="2", Name="Client"}
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            var user = new ApplicationUser
            {
                FirstName = "Bob",
                LastName = "Dilon",
                Email = "bob@example.com",
                NormalizedEmail = "XXXX@EXAMPLE:COM",
                UserName = "bob@example.com",
                NormalizedUserName = "bob@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Testni123!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
            };

            var emp = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "jhon@example.com",
                NormalizedEmail = "XXXX@EXAMPLE:COM",
                UserName = "jhon@example.com",
                NormalizedUserName = "jhon@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == emp.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(emp, "Example123!");
                emp.PasswordHash = hashed;
                context.Users.Add(emp);
            };

            context.SaveChanges();

            var UserRoles = new IdentityUserRole<string>[] {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=emp.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id}
            };

            context.UserRoles.AddRange(UserRoles);
            context.SaveChanges();
        }
    }
}