using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Unico
    {
        private DateTime _fecha;
        private int _numRecibo;
        private double _monto;
        private int _descuento;


        public Unico(DateTime fecha, int numRecibo, double monto, int descuento)
        {
            _fecha = fecha;
            _numRecibo = numRecibo;
            _monto = monto;
            _descuento = descuento;
        }
    }
}
