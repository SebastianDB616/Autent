using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Autenticacion.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Autenticacion.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Verificar las credenciales del usuario en la base de datos
                using (var schoolContext = new SchoolContext())
                {
                    var user = await schoolContext.User.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
                    if (user != null)
                    {
                        // Realizar la autenticación del usuario
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                        var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                        return RedirectToPage("/Index");
                    }
                }
            }

            // Las credenciales son inválidas o no se pudo autenticar al usuario
            ModelState.AddModelError(string.Empty, "Las credenciales son inválidas.");
            return Page();
        }
    }
}

// https://github.com/SebastianDB616/Autent.git