using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Pago : IValidable
    {

        protected int _id;
        protected static int s_ultiID = 1;
        protected string _descripcion;
        protected MetodoDePago _metodoDePago;
        protected TipoDeGasto _tipoDeGasto;
        protected Usuario _usuario;


        public Pago(string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario)
        {
            _id = s_ultiID++;
            _descripcion = descripcion;
            _metodoDePago = metodoDePago;
            _tipoDeGasto = tipoDeGasto;
            _usuario = usuario;
        }

        public Usuario Usuario{  get{return _usuario;} }


        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser nula o estar vacia");

            if (_tipoDeGasto == null) throw new Exception("El tipo de gasto no puede ser nulo");
            if (_usuario == null) throw new Exception("El usuario no puede ser nulo");
        }

        public abstract double CalcularMontoTotal();

        public abstract bool PagoEsteMes(DateTime fecha);
    }


}
