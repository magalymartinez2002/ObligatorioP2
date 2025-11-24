using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Clase TipoDeGasto

namespace Dominio
{
    public class TipoDeGasto : IValidable
    {
        private string _nombre;
        private string _descripcion;


        public TipoDeGasto() { }
        public TipoDeGasto(string nombre, string descripcion)
        {
            _nombre = nombre;
            _descripcion = descripcion;
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser nula o estar vacia");
        }

      

        public override bool Equals(object? obj)
        {

            return obj is TipoDeGasto t && t.Nombre == _nombre;
        }
    }
}
