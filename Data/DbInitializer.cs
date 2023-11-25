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
            return new Pet { Name = name, Type = type, DateAdded = DateTime.Now };
        }

        private static Notification MakeNotification(string title, string body, string empID)
        {
            return new Notification { Title = title, Body = body, EmployeeID = empID, DateCreated = DateTime.Now };
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
                MakePet("Skobčevka Jaka", "Skobčevka, star je 5 let. Lepo poje. Je čudovit in zabaven hišni ljubljenček. Ne boji se rok in rad sedi na rami. Ne grize rad se tudi igra z igračami."),
                MakePet("Pes Sky", "Pes, star 10 let. Je zelo živahen in aktiven kuža. Obožuje igrače, hkrati pa je zelo ubogljiv in učljiv. Je res priden, prijazen in simpatičen kuža, ki se na žalost z mucki ne razume najbolje, saj jih, vsaj zaenkrat, preganja. Prav tako se ne razume z drugimi psi. Sky trenutno tehta približno 25 kilogramov."),
                MakePet("Ronaldo the Great", "Grška želva, star je 15 let. Rad sedi v svojem kotu kletke. Tam sedi že kr nekaj dni. Ko pa se premika pa takoj slišiš razne zvoke. Če hoče je tudi hiter kot pes."),
                MakePet("Hrček Dorthy", "Je mlada, stara približno 6 mesecev. Je živahna in radovedna. Zelo rada raziskuje svoj kletkarski svet in obožuje labirinte ter male igrače. Dorothy je pridna, čeprav včasih malce nagajiva. Je prijazna in se dobro razume z drugimi hrčki. Trenutno tehta približno 50 gramov."),
                MakePet("Kakadu Leno", "Živahen in zabaven kakadu. Je radoveden, poln energije ter ima izjemno zabaven in prikupen karakter. Leno rad raziskuje svoje okolje in se rad igra z različnimi igračami. Je pameten in se hitro uči novih trikov. Zaradi svojega živahnega naravnega nagona je včasih glasen, a obenem prinaša ogromno veselja in zabave s svojim edinstvenim ptičjim temperamentom."),
                MakePet("Jež Tini", "Jež Tina je prikupen in sramežljiv nočni lovec. Ima ščitasto in bodičasto zunanjost ter je izjemno previdna žival. Tina obožuje hrano, kot so črvi, polži in majhni insekti, ter se večinoma zadržuje v grmovju ali pod drevesi. Je mirna in radovedna, vendar raje ohranja svoj prostor in se umakne, če se počuti ogrožena."),
                MakePet("Ribica Riki", "Rika je majhna zlata ribica, ki plava po svojem akvariju. Je živahna in se rada giblje med rastlinami ter se igra z drobnimi kamni na dnu. Rika obožuje čisto vodo in živi v urejenem akvariju s pravilno temperaturo vode. Je prijazna in radovedna ter se veseli obrokov hrane ob isti uri vsak dan."),
                MakePet("Želvi Tim in Tara", "Tim in Tara sta dve prikupni želvi, ki živita v svojem terariju. Sta radovedni in se radi prepuščata sončnim žarkom pod svojo UV-lučjo. Tim je nekoliko počasnejši in rad se stiska pod svojo skalo, medtem ko je Tara bolj drzna in rada raziskuje vsak kotiček terarija. Oba obožujeta zeleno listnato hrano in se rada prepuščata mirnemu življenju."),
                MakePet("Čivava Lojz", "Je majhen in živahen kuža, poln energije. Ima dolgo dlako in značilno majhno postavo. Lojz je izjemno navezan na svojega lastnika in rad preživlja čas v njegovem naročju. Kljub majhnosti je pogumen in zelo zaščitniški do svojega okolja. Rad se igra z majhnimi igračami in obožuje pozornost ter crkljanje. Lojz je inteligenten in se hitro uči novih trikov.")
            };
            context.Pets.AddRange(pets);
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
                context.Clients.Add(new Client { ClientID = user.Id, FirstMidName = user.FirstName, LastName = user.LastName, DateJoined = DateTime.Now });
            };

            var user1 = new ApplicationUser
            {
                FirstName = "Anica",
                LastName = "Baa",
                Email = "baa@example.com",
                NormalizedEmail = "XXXX@EXAMPLE:COM",
                UserName = "baa@example.com",
                NormalizedUserName = "baa@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user1, "Testni123!");
                user1.PasswordHash = hashed;
                context.Users.Add(user1);
                context.Clients.Add(new Client { ClientID = user1.Id, FirstMidName = user1.FirstName, LastName = user1.LastName, DateJoined = DateTime.Now });
            };

            var emp = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "john@example.com",
                NormalizedEmail = "XXXX@EXAMPLE:COM",
                UserName = "john@example.com",
                NormalizedUserName = "john@example.com",
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
                context.Employees.Add(new Employee { EmployeeID = emp.Id, FirstMidName = emp.FirstName, LastName = emp.LastName, HireDate = DateTime.Now });
            };

            context.SaveChanges();

            var UserRoles = new IdentityUserRole<string>[] {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=emp.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user1.Id}
            };

            context.UserRoles.AddRange(UserRoles);
            context.SaveChanges();

            var Notifications = new Notification[]{
                MakeNotification("Odprtje zavetišča E_Zavetišče", "Z veseljem sporočamo odprtje našega novega zavetišča E_Zavetišče! Zavetišče, ki ga navdihuje ljubezen do živali, bo nudilo topel in skrben dom za tiste, ki potrebujejo našo pomoč. \r\nE_Zavetišče si prizadeva zagotavljati varno in udobno okolje za naše kosmate prijatelje, kjer bodo deležni oskrbe, nege in ljubezni. Naša ekipa strastnih in izkušenih prostovoljcev je pripravljena, da sprejme in poskrbi za vsako žival, ki jo bomo gostili. Veselimo se, da bomo skupaj z vami ustvarjali varno zavetje za živali v stiski.", emp.Id),
                MakeNotification("Prvi kosmatinec Sky", "Super novica! Pravkar smo sprejeli našo prvo žival v E_Zavetišče - Sky. Naš novi kosmatinec že uživa v udobju in skrbi, ki mu jo nudimo. Pridite ga spoznat in spremljajte naše dogodivščine skupaj!", emp.Id),
                MakeNotification("Resnično hvala", "Pozdravljeni vsi ljubitelji živali! Spomladanski čas je tu in pri E_Zavetišču smo navdušeni nad vašo podporo. Zahvaljujoč vaši pomoči in donacijam smo lahko nudili zdravstveno oskrbo, topel dom in ljubezen mnogim živalim v stiski. Hvala, ker ste del naše zgodbe in omogočate, da lahko spreminjamo življenja teh kosmatincev!", emp.Id),
                MakeNotification("Pomagajmo živalim", "Pri E_Zavetišču verjamemo, da ima vsaka žival pravico do dostojnega življenja. Naša ekipa se vsak dan trudi, da bi zagotovili tople postelje, kvalitetno hrano in ljubečo oskrbo vsem živalim, ki so se znašle v naši oskrbi. Vaša podpora nam omogoča, da lahko še naprej sledimo temu poslanstvu in spreminjamo življenja na bolje!", emp.Id),
                MakeNotification("Živali iščejo nov dom", "Novice iz E_Zavetišča! Naša vrata so odprta za mnoge nove šape in repke, ki iščejo svoj dom. Veliko novih kosmatincev je pripravljenih deliti ljubezen in sprejemati nežnost. Pridite jih spoznat in morda najdete svojega popolnega sopotnika!", emp.Id)
            };

            context.Notifications.AddRange(Notifications);
            context.SaveChanges();
        }
    }
}