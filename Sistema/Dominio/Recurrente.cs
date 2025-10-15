using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Clase Recurrente 

namespace Dominio
{
    public class Recurrente :Pago
    {
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private int _recargo;


        public Recurrente(DateTime fechaInicio, DateTime fechaFin, double monto, string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario) : base(descripcion, metodoDePago, tipoDeGasto, usuario, monto)
        {
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
         
            _recargo = CalcularRecargo();
        }



        public override double CalcularMontoTotal()
        {
            if (_fechaFin == DateTime.MinValue) _fechaFin = DateTime.Today;

            int meses = CalcularMeses(_fechaFin, _fechaInicio);
            double montoTotal = (_monto * _recargo / 100) * meses;

            return montoTotal;

        }

        public int CalcularPagosPendientes()
        {
            int pagosPendientes = 0;
            DateTime fechaActual = DateTime.Today;

            
            if (_fechaFin > fechaActual)
            {
                pagosPendientes= CalcularMeses(_fechaFin, fechaActual);
            }
            return pagosPendientes;
        }


        private int CalcularMeses(DateTime fechaFin, DateTime fechaInicio)
        {
            int  meses = (fechaFin.Year - fechaInicio.Year) * 12 + (fechaFin.Month - fechaInicio.Month);
            
            return meses;
        }

       public override void Validar()
        {
            base.Validar();
            if (_fechaInicio > DateTime.Today) throw new Exception("La fecha de inicio no puede ser mayor a la fecha actual");
            if (_fechaFin != DateTime.MinValue && _fechaFin < _fechaInicio) throw new Exception("La fecha de fin no puede ser menor a la fecha de inicio");
            
        }


        private int CalcularRecargo()
        {
           
            int recargoTotal = 3;
            DateTime fecha = _fechaFin;

           if(_fechaFin == DateTime.MinValue) fecha=DateTime.Today;
            int meses = CalcularMeses(fecha,_fechaInicio);


            if (meses >= 10) recargoTotal = 10;
            else if (meses >= 6) recargoTotal = 5;


            return recargoTotal;
        }

        public override string ToString()
        {
            string s = $" Pago: {_id} - Metodo de Pago: - {_metodoDePago} - Monto Total: {CalcularMontoTotal()}";
            if (_fechaFin == DateTime.MinValue) s=s+ $" - Recurrente";
            else s=s+ $" - Pagos pendientes: {CalcularPagosPendientes()}";

            return s;
        }
    }
}
