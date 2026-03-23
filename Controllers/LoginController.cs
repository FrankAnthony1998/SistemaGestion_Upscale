using Microsoft.AspNetCore.Mvc;
using MiWebApp.Data;
using MiWebApp.Models;
using System.Linq;
using System.Security.Claims; 
using Microsoft.AspNetCore.Authentication; 

namespace MiWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDataContext _context;
        public LoginController(AppDataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index() => View();
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario == null)
            {
                ViewBag.Mensaje = "Usuario no encontrado.";
                return View();
            }
            if (usuario.EstaBloqueado)
            {
                if (usuario.FechaBloqueo.HasValue && DateTime.Now < usuario.FechaBloqueo.Value.AddMinutes(1))
                {
                    ViewBag.Mensaje = "Cuenta bloqueada temporalmente. Intente más tarde.";
                    return View();
                }
                else
                {
                    usuario.EstaBloqueado = false;
                    usuario.IntentosFallidos = 0;
                }
            }
            if (usuario.Password == password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Email),
                        new Claim("UsuarioId", usuario.Id.ToString())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
                    usuario.IntentosFallidos = 0;
                    usuario.EstaBloqueado = false;
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Dashboard");
                }
            else
            {
                usuario.IntentosFallidos++;
                if (usuario.IntentosFallidos >= 5)
                {
                    usuario.EstaBloqueado = true;
                    usuario.FechaBloqueo = DateTime.Now;
                    ViewBag.Mensaje = "Has superado los 5 intentos cuenta bloqueada.";
                }
                else
                {
                    ViewBag.Mensaje = $"Contraseña incorrecta. Intento {usuario.IntentosFallidos} de 5.";
                }
                _context.SaveChanges();
                return View();
            }
        }
    }
}