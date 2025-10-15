using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Clase Usuario

namespace Dominio
{
    public class Usuario : IValidable
    {

        private string _nombre;
        private string _apellido;
        private string _contrasenia;
        private string _email;
        private Equipo _equipo;
        private DateTime _fechaIngreso;

        public Usuario(string nombre, string apellido, string contrasenia, Equipo equipo, DateTime fechaIngreso)
        {
            _nombre = nombre;
            _apellido = apellido;
            _contrasenia = contrasenia;
            _equipo = equipo;
            _fechaIngreso = fechaIngreso;
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Nombre
        {
            get { return _nombre;}
        }

        public string Apellido
        {
            get { return _apellido;}
        }
        public Equipo Equipo
        {
            get { return _equipo; }
        }



        public void Validar()
        {

            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");

            if (string.IsNullOrEmpty(_apellido)) throw new Exception("El apellido no puede ser nulo o estar vacio");

            if (string.IsNullOrEmpty(_contrasenia)) throw new Exception("La contrasenia no puede ser nula o estar vacia");
            else if (_contrasenia.Length < 8) throw new Exception("La contrasenia debe tener al menos 8 caracteres");

            if (string.IsNullOrEmpty(_email)) throw new Exception("El email no puede ser nulo o estar vacio");

            if (_equipo == null) throw new Exception("El equipo no puede ser nulo");

            if (_fechaIngreso > DateTime.Today) throw new Exception("La fecha de ingreso no puede ser mayor a la fecha actual");


        }


        public override string ToString()
        {
            return $"{_nombre} {_apellido} - {_email} - {_equipo.Nombre} ";
        }
    }
}
