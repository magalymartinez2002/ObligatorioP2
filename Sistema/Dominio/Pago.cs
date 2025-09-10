using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pago
    {

        private int _id;
        private string _descripcion;
        private MetodoDePago _metodoDePago;
        private TipoDeGasto _tipoDeGasto;
        private Usuario _usuario;


        public Pago( string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario)
        {
            
            _descripcion = descripcion;
            _metodoDePago = metodoDePago;
            _tipoDeGasto = tipoDeGasto;
            _usuario = usuario;
        }
    }
}
