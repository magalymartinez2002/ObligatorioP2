using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario : IValidable
    {

        private string _noombre;
        private string _apellido;
        private string _contrasenia;
        private string _email;
        private Equipo _equipo;
        private DateTime _fechaIngreso;

        public Usuario(string noombre, string apellido, string contrasenia, string email, Equipo equipo, DateTime fechaIngreso)
        {
            _noombre = noombre;
            _apellido = apellido;
            _contrasenia = contrasenia;
            _email = email;
            _equipo = equipo;
            _fechaIngreso = fechaIngreso;
        }

        public string Email
        {
            get { return _email; }
        }

        public Equipo Equipo
        {
            get { return _equipo; }
        }



        public void Validar()
        {

            if (string.IsNullOrEmpty(_noombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");

            if (string.IsNullOrEmpty(_apellido)) throw new Exception("El apellido no puede ser nulo o estar vacio");

            if (string.IsNullOrEmpty(_contrasenia)) throw new Exception("La contrasenia no puede ser nula o estar vacia");
            else if (_contrasenia.Length < 8) throw new Exception("La contrasenia debe tener al menos 8 caracteres");

            if (string.IsNullOrEmpty(_email)) throw new Exception("El email no puede ser nulo o estar vacio");

            else if (!_email.Contains("@") || !_email.Contains(".")) throw new Exception("El email debe contener '@' y '.'");

            if (_equipo == null) throw new Exception("El equipo no puede ser nulo");

            if (_fechaIngreso > DateTime.Now) throw new Exception("La fecha de ingreso no puede ser mayor a la fecha actual");


        }


        public override string ToString()
        {
            return $"{_noombre} {_apellido} - {_email} - {_equipo.Nombre} ";
        }
    }
}
