using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {

        private string _noombre;
        private string _apellido;
        private string _email;
        private Equipo _equipo;
        private DateTime _fechaIngreso;

        public Usuario(string noombre, string apellido, string email, Equipo equipo, DateTime fechaIngreso)
        {
            _noombre = noombre;
            _apellido = apellido;
            _email = email;
            _equipo = equipo;
            _fechaIngreso = fechaIngreso;
        }
    }
}
