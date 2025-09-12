using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Unico : Pago
    {
        private DateTime _fecha;
        private int _numRecibo;
        private double _monto;
        private int _descuento;


        public Unico(DateTime fecha, int numRecibo, double monto, string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario) : base(descripcion, metodoDePago, tipoDeGasto, usuario)
        {
            _fecha = fecha;
            _numRecibo = numRecibo;
            _monto = monto;
            _descuento = CalcularDescuento();
        }


        public int CalcularDescuento()
        {
            int descuentoTotal = 10;
            if (MetodoDePago == MetodoDePago.EFECTIVO) descuentoTotal = 20;

            return descuentoTotal;
        }

        public double CalcularMontoTotal()
        {
            double montoTotal = _monto - (_monto * _descuento / 100);
            return montoTotal;
        }
    }
}
