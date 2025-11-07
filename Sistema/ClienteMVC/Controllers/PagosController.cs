using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ClienteMVC.Controllers
{
    public class PagosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        public IActionResult Listado()
        {
            
            Usuario u = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));
            ViewBag.Listado = miSistema.ListarPagosPorUsuarioDelMes(u, DateTime.Today);
            return View();
        }


        [HttpGet]
        public IActionResult ListadoPorEquipo()
        {
            DateTime fecha = DateTime.Today;
            if (TempData["Fecha"] != null) fecha = (DateTime)(TempData["Fecha"]);

            if (TempData["Mensaje"] != null) ViewBag.Mensaje = TempData["Mensaje"].ToString();

            Usuario u = miSistema.BuscarUsuarioPorEmail(HttpContext.Session.GetString("usuario"));

            ViewBag.Listado = miSistema.ListarPagosPorEquipo(u.Equipo, fecha);
           
            return View();
        }

        [HttpPost]
        public IActionResult ListadoPorEquipo(DateTime fecha)
        {
            TempData["Mensaje"] = $"Pagos de la fecha {fecha.ToString()}";
            TempData["Fecha"] = fecha;
            return RedirectToAction("ListadoPorEquipo");


        }

        [HttpGet]
        public IActionResult OpcionAlta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OpcionAlta(string tipoPago)
        {
           
           return tipoPago == "unico"? RedirectToAction("AltaUnico") : RedirectToAction("AltaRecurrente");
        }

        [HttpGet]
        public IActionResult AltaUnico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AltaUnico(Unico p, string nomTipoGasto)
        {
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
            return View();
        }

        [HttpPost]
        public IActionResult AltaRecurrente(Recurrente p,string nomTipoGasto)
        {
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
