using Autenticacion.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Autenticacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Guardar el usuario en la base de datos utilizando Entity Framework Core
                // Aquí asumimos que tienes un contexto de base de datos llamado "DbContext" y una tabla llamada "Users"
                using (var dbContext = new SchoolContext())
                {
                    dbContext.User.Add(User);
                    await dbContext.SaveChangesAsync();
                }

                // Realizar la autenticación del usuario recién registrado
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, User.Email),  // Utilizar User.Email como ClaimTypes.NameIdentifier
                new Claim(ClaimTypes.Email, User.Email),
                new Claim(ClaimTypes.Name, User.Name),
                new Claim(ClaimTypes.Surname, User.LastName)
            };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
