using Microsoft.AspNetCore.Mvc;
using Dominio;

//Controlador Usuarios
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
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            try
            {
                Usuario usuario = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));
                ViewBag.Usuario = usuario;
                ViewBag.MontoTotal = miSistema.MontoTotalPorUsuario(usuario, DateTime.Today);
                ViewBag.Equipo = miSistema.ListarUsuariosPorEquipo(usuario.Equipo);
               
            }
            catch (Exception ex) 
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        
        public IActionResult Logout()
        {
         
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
