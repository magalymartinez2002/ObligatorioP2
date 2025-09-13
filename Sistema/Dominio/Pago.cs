using Dominio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pago : IValidable
    {

        private int _id;
        private static int s_ultiID = 1;
        private string _descripcion;
        private MetodoDePago _metodoDePago;
        private TipoDeGasto _tipoDeGasto;
        private Usuario _usuario;


        public Pago( string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario)
        {
            _id = s_ultiID++;
            _descripcion = descripcion;
            _metodoDePago = metodoDePago;
            _tipoDeGasto = tipoDeGasto;
            _usuario = usuario;
        }

        public MetodoDePago MetodoDePago { get { return _metodoDePago; } }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("La descripcion no puede ser nula o estar vacia");
           
            if (_tipoDeGasto == null) throw new Exception("El tipo de gasto no puede ser nulo");
            if (_usuario == null) throw new Exception("El usuario no puede ser nulo");
        } 

        public override string ToString()
        {
            return $"{_descripcion} - {_metodoDePago} - {_tipoDeGasto} - {_usuario}";
        }

    }


}
