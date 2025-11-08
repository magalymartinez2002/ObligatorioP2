using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace ClienteMVC.Controllers
{
    public class TiposDeGastosController : Controller
    {
        Sistema miSistema=Sistema.Instancia;
        [HttpGet]
        public IActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alta(TipoDeGasto t)
        {
            try
            {
                if (string.IsNullOrEmpty(t.Nombre)) throw new Exception("El noombre no puede ser nulo o estar vacio");
                if (string.IsNullOrEmpty(t.Descripcion)) throw new Exception("La descripcion no puede ser nula o estar vacia");

                miSistema.CrearTipoDeGasto(t);
                ViewBag.Exito = "Tipo de gasto creado con exito";


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

            }
            return View();
        }

        [HttpGet]
        public IActionResult Baja() 
        {
            ViewBag.Listado = miSistema.TiposDeGasto;
            return View();
        }

        [HttpPost]
        public IActionResult Baja(string nombre)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre es nulo");
                TipoDeGasto t = miSistema.BuscarTipoDeGasto(nombre);
                if(miSistema.ExisteTipoDeGasto(nombre)) throw new Exception("No se puede eliminar ya que esta siendo utilizado");
                miSistema.TiposDeGasto.Remove(t);

                ViewBag.Exito = "Tipo de gasto eliminado con exito";
            }
            catch (Exception ex) 
            { 
                ViewBag.Error=ex.Message;
            }

            return View();
        }
    }
}
