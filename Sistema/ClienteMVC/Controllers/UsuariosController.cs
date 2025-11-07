using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace ClienteMVC.Controllers
{
    public class UsuariosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string pass)
        {
            try
            {
                Usuario usuario= miSistema.Login(email, pass);
                if(usuario == null) throw new Exception("Email o contraseña incorrectos");

                HttpContext.Session.SetString("usuario", usuario.Email);
                HttpContext.Session.SetString("rol", usuario.Rol.ToString());

                return RedirectToAction("Index", "Home");

            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
        }

        public IActionResult Perfil()
        { 
            Usuario usuario = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));
            ViewBag.Usuario= usuario;
               
            ViewBag.Equipo = miSistema.ListarUsuariosPorEquipo(usuario.Equipo);
            return View();
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
