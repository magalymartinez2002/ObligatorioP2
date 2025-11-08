using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Clase Pago

namespace Dominio
{
    public abstract class Pago : IValidable, IComparable<Pago>
    {

        protected int _id;
        protected static int s_ultiID = 1;
        protected string _descripcion;
        protected MetodoDePago _metodoDePago;
        protected TipoDeGasto _tipoDeGasto;
        protected Usuario _usuario;
        protected double _monto;

        public Pago()
        {
            _id = s_ultiID++;
        }

        public Pago(string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario, double monto)
        {
            _id = s_ultiID++;
            _descripcion = descripcion;
            _metodoDePago = metodoDePago;
            _tipoDeGasto = tipoDeGasto;
            _usuario = usuario;
            _monto = monto;
        }

        public Usuario Usuario
        {  
            get {return _usuario;} 
            set { _usuario = value;}
        }

        public double Monto
        {
            get { return _monto; }
            set { _monto = value;}
        }
        public TipoDeGasto TipoDeGasto
        {
            get { return _tipoDeGasto; }
            set { _tipoDeGasto = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        public int Id
        {
            get { return _id; }
        }
        public MetodoDePago MetodoDePago
        {
            get { return _metodoDePago; }
            set { _metodoDePago = value; }
        }


        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser nula o estar vacia");
            if (_monto <= 0) throw new Exception("El monto debe ser mayor a 0");
            if (_tipoDeGasto == null) throw new Exception("El tipo de gasto no puede ser nulo");
            if (_usuario == null) throw new Exception("El usuario no puede ser nulo");
        }

       

        public abstract double CalcularMontoTotal();

        public abstract bool PagoEsteMes(DateTime fecha);

        public int CompareTo(Pago? other)
        {
            return _monto.CompareTo(other._monto) * -1;
        }
    }


}
