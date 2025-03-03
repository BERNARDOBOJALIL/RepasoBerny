using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepasoBerny.Shared.Entities;
using RepasoBerny.Web.Helpers;

namespace RepasoBerny.Web.Data
{
    public class Seeder
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public Seeder(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();
            await CheckCitiesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Brad", "Pitt", "brad.pitt@gmail.com", "2221111111", "brad.png", UserType.Provider);
            await CheckUserAsync("Angelina", "Jolie", "angelina.jolie@gmail.com", "2222222222", "angelina.png", UserType.Client);
            await CheckUserAsync("Jennifer", "López", "jlo@gmail.com", "2223333333", "jlo.png", UserType.User);
            await CheckUserAsync("Dwayne", "Johnson", "laroca@gmail.com", "2224444444", "laroca.png", UserType.Admin);
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phoneNumber, string photo, UserType userType)
        {
            var user = await userHelper.GetUserAsync(email);
            if (user == null) 
            {
                var city = await dataContext.Cities.FirstOrDefaultAsync(x => x.Name == "Veracruz");
                if (city == null)
                {
                    city = await dataContext.Cities.FirstOrDefaultAsync();
                }
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    UserName = email,
                    UserType = userType,
                    City = city,
                    PhotoUrl = photo
                };
                IdentityResult x = await userHelper.AddUserAsync(user, "123456");
                await userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
            return user;
        }

        private async Task CheckRolesAsync()
        {
            await userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await userHelper.CheckRoleAsync(UserType.Provider.ToString());
            await userHelper.CheckRoleAsync(UserType.Client.ToString());
            await userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCitiesAsync()
        {
            if(!dataContext.Cities.Any())
            {
                dataContext.Cities.Add(new City { Name = "San Andres Cholula" });
                dataContext.Cities.Add(new City { Name = "Puebla" });
                dataContext.Cities.Add(new City { Name = "Veracruz" });
                dataContext.Cities.Add(new City { Name = "Oaxaca" });
                dataContext.Cities.Add(new City { Name = "Chipilo" });
                dataContext.Cities.Add(new City { Name = "Atlixcos" });
                dataContext.Cities.Add(new City { Name = "Las Vegas" });
                await dataContext.SaveChangesAsync();
            }
        }
    }
}
