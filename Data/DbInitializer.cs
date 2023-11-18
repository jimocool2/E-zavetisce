using E_zavetisce.Data;
using E_zavetisce.Models;
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
        }
    }
}