using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Autenticacion.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Autenticacion.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        public void OnGet()
        {   

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, User.Email)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
            return RedirectToPage("/Index");
        }
    }
}

// https://github.com/SebastianDB616/Autent.git