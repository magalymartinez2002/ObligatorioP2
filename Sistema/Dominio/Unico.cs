using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Clase Unico

namespace Dominio
{
    public class Unico : Pago
    {
        private DateTime _fecha;
        private int _numRecibo;
        private int _descuento;

        public Unico() : base()
        {
            _descuento = CalcularDescuento();
        }
        public Unico(DateTime fecha, int numRecibo, double monto, string descripcion, MetodoDePago metodoDePago, TipoDeGasto tipoDeGasto, Usuario usuario) : base(descripcion, metodoDePago, tipoDeGasto, usuario, monto)
        {
            _fecha = fecha;
            _numRecibo = numRecibo;
            _descuento = CalcularDescuento();
        }

        public DateTime Fecha {
            get { return _fecha; } 
            set { _fecha = value; }
        }
        public int NumRecibo {
            get { return _numRecibo; } 
            set { _numRecibo = value; }
        }



        private int CalcularDescuento()
        {
            int descuentoTotal = 10;
            if (_metodoDePago == MetodoDePago.EFECTIVO) descuentoTotal = 20;

            return descuentoTotal;
        }

       

        public override double CalcularMontoTotal()
        {
            double montoTotal = _monto - (_monto * _descuento / 100);
            return montoTotal;
        }

        public override bool PagoEsteMes(DateTime fecha)
        {
            return (_fecha.Year == fecha.Year && _fecha.Month == fecha.Month);
        }

        public override void Validar()
        {
            base.Validar();
            if (_fecha > DateTime.Today) throw new Exception("La fecha no puede ser mayor a la fecha actual");
            if (_numRecibo <= 0) throw new Exception("El numero de recibo debe ser mayor a 0");
            
        
        }
       

        public override string ToString()
        {
            return $" Pago: {_id} - Metodo de Pago: {_metodoDePago} -  Monto Total: {CalcularMontoTotal()}";
        }
    }

}
