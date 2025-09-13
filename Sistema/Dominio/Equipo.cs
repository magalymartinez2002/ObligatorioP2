using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Equipo : IValidable
    {
        private int _id;
        private static int s_ultId = 1;
        private string _nombre;


        public Equipo(string nombre)
        {
            _id = s_ultId++;
            _nombre = nombre;
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
        }

        public override string ToString()
        {
            return $"{_nombre}";
        }
    }
}
