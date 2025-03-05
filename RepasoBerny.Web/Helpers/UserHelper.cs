using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepasoBerny.Shared.DTO;
using RepasoBerny.Shared.Entities;
using RepasoBerny.Web.Data;

namespace RepasoBerny.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext dataContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<User> signInManager;

        public UserHelper(
            DataContext dataContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(
                    new IdentityRole
                    {
                        Name = roleName,
                    });
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await dataContext.Users
                .Include(u => u.City!)
                .FirstOrDefaultAsync(x => x.Email == email);
            return user!;
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO login)
        {
            return await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
