using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario: IValidable
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



        public void Validar()
        {
           
            if (string.IsNullOrEmpty(_noombre))  throw new Exception("El nombre no puede ser nulo o estar vacio");
           
            if (string.IsNullOrEmpty(_apellido)) throw new Exception("El apellido no puede ser nulo o estar vacio");

            if (string.IsNullOrEmpty(_email)) throw new Exception("El email no puede ser nulo o estar vacio");

            else if (!_email.Contains("@") || !_email.Contains(".")) throw new Exception("El email debe contener '@' y '.'");

            if (_equipo == null) throw new Exception("El equipo no puede ser nulo");
           
            if (_fechaIngreso > DateTime.Now) throw new Exception("La fecha de ingreso no puede ser mayor a la fecha actual");

         
        }
    }
}
