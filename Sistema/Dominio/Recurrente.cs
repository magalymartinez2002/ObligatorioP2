using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Recurrente :Pago
    {
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private double _montoDelPago;
        private int _recargo;


        public Recurrente(DateTime fechaInicio, DateTime fechaFin, double montoDelPago, int recargo, string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario) : base(descripcion, metodoDePago, tipoDeGasto, usuario)
        {
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _montoDelPago = montoDelPago;
            _recargo = CalcularRecargo();
        }



        public override double CalcularMontoTotal()
        {
            // if (_fechaFin.) _fechaFin=DateTime.Today; si no tiene fecha fin, toma la fecha actual
            int meses = ((_fechaFin.Year - _fechaInicio.Year) * 12) + _fechaFin.Month - _fechaInicio.Month + 1;
            double montoTotal = (_montoDelPago * _recargo / 100) * meses;



            return montoTotal;

        }


        public int CalcularRecargo()
        {
            int recargoTotal = 3;
            int meses = ((_fechaFin.Year - _fechaInicio.Year) * 12) + _fechaFin.Month - _fechaInicio.Month + 1;

            if (meses>=10) recargoTotal = 10;
            else if (meses>=6) recargoTotal = 5;


            return recargoTotal;
        }

        public override string ToString()
        {
            return $"{_fechaInicio} - {_fechaFin} - Monto del Pago: {_montoDelPago} - Recargo: {_recargo}% - Monto Total: {CalcularMontoTotal()}";
        }
    }
}
