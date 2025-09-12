using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Recurrente
    {
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private double _montoTotal;
        private int _recargo;


        public Recurrente(DateTime fechaInicio, DateTime fechaFin, double montoTotal, int recargo)
        {
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _montoTotal = montoTotal;
            _recargo = recargo;
        }
    }
}
