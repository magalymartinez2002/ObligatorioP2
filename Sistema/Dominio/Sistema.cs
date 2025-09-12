using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Pago> _pagos = new List<Pago>();
        private List<Equipo> _equipos = new List<Equipo>();


        public void CrearUsuario(Usuario u)
        {
            if (u == null) throw new Exception("El objeto usuario no puede ser nulo");
            u.Validar();
            _usuarios.Add(u);
        }

        public void CrearPago(Pago p)
        {
            if (p == null) throw new Exception("El objeto pago no puede ser nulo");
            p.Validar();
            _pagos.Add(p);
        }

        public void CrearEquipo(Equipo e)
        {
            if (e == null) throw new Exception("El objeto equipo no puede ser nulo");
            e.Validar();
            _equipos.Add(e);
        }



    }
}
