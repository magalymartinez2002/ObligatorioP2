using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoDeGasto : IValidable
    {
        private string _nombre;
        private string _descripcion;

        public TipoDeGasto(string nombre, string descripcion)
        {
            _nombre = nombre;
            _descripcion = descripcion;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser nula o estar vacia");
        }
    }
}
