using Dominio;
using Microsoft.AspNetCore.Mvc;

//Controlador Pagos
namespace ClienteMVC.Controllers
{
    public class PagosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        public IActionResult Listado()
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            try
            {
                Usuario u = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));
                ViewBag.Listado = miSistema.ListarPagosPorUsuarioDelMes(u, DateTime.Today);
                
            }
            catch (Exception ex) 
            {
                ViewBag.Error = ex;
            }
            return View();
        }


        [HttpGet]
        public IActionResult ListadoPorEquipo()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "GERENTE") return View("NoAuth");
            try
            {
                DateTime fecha = DateTime.Today;
                if (TempData["Fecha"] != null) fecha = (DateTime)(TempData["Fecha"]);
                if (TempData["Mensaje"] != null) ViewBag.Mensaje = TempData["Mensaje"].ToString();
                Usuario u = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));

                ViewBag.Listado = miSistema.ListarPagosPorEquipo(u.Equipo, fecha);
            }
            catch (Exception ex) 
            { ViewBag.Error = ex; }


            return View();
        }

        [HttpPost]
        public IActionResult ListadoPorEquipo(DateTime fecha)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "GERENTE") return View("NoAuth");

            TempData["Mensaje"] = $"Pagos de la fecha {fecha.ToShortDateString()}";
            TempData["Fecha"] = fecha;
            return RedirectToAction("ListadoPorEquipo");


        }

        [HttpGet]
        public IActionResult OpcionAlta()
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            return View();
        }

        [HttpPost]
        public IActionResult OpcionAlta(string tipoPago)
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            return tipoPago == "unico"? RedirectToAction("AltaUnico") : RedirectToAction("AltaRecurrente");
        }

        [HttpGet]
        public IActionResult AltaUnico()
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            return View();
        }

        [HttpPost]
        public IActionResult AltaUnico(Unico p, string nomTipoGasto)
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            try
            {
                if (string.IsNullOrEmpty(p.Descripcion)) throw new Exception("La descripcion no puede ser nula o vacia");
                if (p.Monto <= 0) throw new Exception("El monto debe ser mayor a cero");
                p.Usuario = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));
                p.TipoDeGasto = miSistema.BuscarTipoDeGasto(nomTipoGasto);
                miSistema.CrearPago(p);
                ViewBag.Exito = "Pago unico creado con exito";
            }catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AltaRecurrente()
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            return View();
        }

        [HttpPost]
        public IActionResult AltaRecurrente(Recurrente p,string nomTipoGasto)
        {
            if (HttpContext.Session.GetString("rol") == null) return View("NoAuth");
            try
            {
                if (string.IsNullOrEmpty(p.Descripcion)) throw new Exception("La descripcion no puede ser nula o vacia");
                if (p.Monto <= 0) throw new Exception("El monto debe ser mayor a cero");
                p.Usuario = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));
                p.TipoDeGasto= miSistema.BuscarTipoDeGasto(nomTipoGasto);

                miSistema.CrearPago(p);
                ViewBag.Exito = "Pago recurrente creado con exito";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
          
        }
    }
}
