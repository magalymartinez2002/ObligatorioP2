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
        private List<TipoDeGasto> _tiposDeGasto = new List<TipoDeGasto>();


        public Sistema()
        {
            PrecargarEquipos();
            PrecargarUsuarios();
            PrecargarTiposDeGasto();
            PrecargarPagos();
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
        }

        public List<Pago> Pagos
        {
            get { return _pagos; }
        }

        public List<Equipo> Equipos
        {
            get { return _equipos; }
        }   

        public List<TipoDeGasto> TiposDeGasto
        {
            get { return _tiposDeGasto; }
        }

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

        public void CrearTipoDeGasto(TipoDeGasto t)
        {
            if (t == null) throw new Exception("El objeto tipo de gasto no puede ser nulo");
            t.Validar();
            _tiposDeGasto.Add(t);
        }
        //Listados
        public List<Pago> ListarPagosPorMes(DateTime fecha)
        {
            List<Pago> pagosDelMes = new List<Pago>();
            foreach (Pago p in _pagos)
            {
                if (p.PagoEsteMes(fecha)) pagosDelMes.Add(p);
            }
            return pagosDelMes;
        }

        public List<Pago> ListarPagosPorUsuario(string email)
        {
            Usuario usuario= BuscarUsuarioPorEmail(email);
            if (usuario == null) throw new Exception("No se encontro el usuario");
            List<Pago> pagosDelUsuario = new List<Pago>();
            foreach (Pago p in _pagos)
            {
                if (p.Usuario.Email == usuario.Email) pagosDelUsuario.Add(p);
            }
            return pagosDelUsuario;
        }

        public List<Usuario> ListarUsuariosPorEquipo(string nombreEquipo)
        {
            Equipo equipo= BuscarEquipoPorNombre(nombreEquipo);
            if (equipo == null) throw new Exception("No se encontro el equipo");
            List<Usuario> usuariosDelEquipo = new List<Usuario>();
            foreach (Usuario u in _usuarios)
            {
                if (u.Equipo.Nombre == equipo.Nombre) usuariosDelEquipo.Add(u);
            }
            return usuariosDelEquipo;
        }


        public string CrearEmailUsuario(string nombre, string apellido)
        {
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            if (string.IsNullOrEmpty(apellido)) throw new Exception("El apellido no puede ser nulo o estar vacio");

            nombre= nombre.Length>3 ? nombre.Substring(0, 3).ToLower(): nombre.ToLower();
            apellido= apellido.Length>3 ? apellido.Substring(0, 3).ToLower(): apellido.ToLower();
          
            
            string email= $"{nombre}{apellido}@laEmpresa.com";

            int numero=1;
            while (BuscarUsuarioPorEmail(email)!=null)
            {
                email=$"{nombre}{apellido}{numero}@laEmpresa.com";
                numero++;
            }

            return email;

        }

/*
        public bool ExisteUsuarioConEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new Exception("El email no puede ser nulo o estar vacio");
            foreach (Usuario u in _usuarios)
            {
                if (u.Email.Contains(email)) return true;
            }
            return false;
        }
*/
        public Usuario BuscarUsuarioPorEmail(string email)
        {
            Usuario usuarioBuscado = null;
            if (string.IsNullOrEmpty(email)) throw new Exception("El email no puede ser nulo o estar vacio");
            foreach (Usuario u in _usuarios)
            {
                if (u.Email.Contains(email)) usuarioBuscado= u;
            }
            return usuarioBuscado;
        }

        public Equipo BuscarEquipoPorNombre(string nombre)
        {
            Equipo equipoBuscado = null;
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            foreach (Equipo e in _equipos)
            {
                if (e.Nombre.Contains(nombre)) equipoBuscado= e;
            }
            return equipoBuscado;
        }

        public TipoDeGasto BuscarTipoDeGasto(string nombre)
        {
            TipoDeGasto tipoBuscado = null;
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            foreach (TipoDeGasto t in _tiposDeGasto)
            {
                if (t.Nombre.Contains(nombre)) tipoBuscado= t;
            }
            return tipoBuscado;
        }



        #region Precargas


        private void PrecargarEquipos()
        {
            
            CrearEquipo(new Equipo("Sistemas"));
            CrearEquipo(new Equipo("Contabilidad"));
            CrearEquipo(new Equipo("Marketing"));
            CrearEquipo(new Equipo("Recursos Humanos"));

        }
        private void PrecargarUsuarios()
        {
           
            // Sistemas
            CrearUsuario(new Usuario("Magaly", "Martinez", "a123456.", CrearEmailUsuario("Magaly", "Martinez"), BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario("Juan", "Perez", "b123456.", CrearEmailUsuario("Juan", "Perez"), BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario("Lucia", "Fernandez", "c123456.", CrearEmailUsuario("Lucia", "Fernandez"), BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario("Martin", "Lopez", "d123456.", CrearEmailUsuario("Martin", "Lopez"), BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario("Sofia", "Garcia", "e123456.", CrearEmailUsuario("Sofia", "Garcia"), BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario("Diego", "Rodriguez", "f123456.", CrearEmailUsuario("Diego", "Rodriguez"), BuscarEquipoPorNombre("Sistemas"), DateTime.Today));

            // Contabilidad
            CrearUsuario(new Usuario("Carolina", "Suarez", "g123456.", CrearEmailUsuario("Carolina", "Suarez"), BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario("Andres", "Mendez", "h123456.", CrearEmailUsuario("Andres", "Mendez"), BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario("Paola", "Silva", "i123456.", CrearEmailUsuario("Paola", "Silva"), BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario("Fernando", "Alvarez", "j123456.", CrearEmailUsuario("Fernando", "Alvarez"), BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario("Laura", "Dominguez", "k123456.", CrearEmailUsuario("Laura", "Dominguez"), BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));

            // Marketing
            CrearUsuario(new Usuario("Gonzalo", "Ramos", "l123456.", CrearEmailUsuario("Gonzalo", "Ramos"), BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario("Valentina", "Cabrera", "m123456.", CrearEmailUsuario("Valentina", "Cabrera"), BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario("Rodrigo", "Vazquez", "n123456.", CrearEmailUsuario("Rodrigo", "Vazquez"), BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario("Florencia", "Mora", "o123456.", CrearEmailUsuario("Florencia", "Mora"), BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario("Sebastian", "Castro", "p123456.", CrearEmailUsuario("Sebastian", "Castro"), BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario("Julieta", "Rios", "q123456.", CrearEmailUsuario("Julieta", "Rios"), BuscarEquipoPorNombre("Marketing"), DateTime.Today));

            // Recursos Humanos
            CrearUsuario(new Usuario("Matias", "Ortiz", "r123456.", CrearEmailUsuario("Matias", "Ortiz"), BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario("Camila", "Torres", "s123456.", CrearEmailUsuario("Camila", "Torres"), BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario("Nicolas", "Gomez", "t123456.", CrearEmailUsuario("Nicolas", "Gomez"), BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario("Agustina", "Morales", "u123456.", CrearEmailUsuario("Agustina", "Morales"), BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario("Bruno", "Sanchez", "v123456.", CrearEmailUsuario("Bruno", "Sanchez"), BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario("Daniela", "Prieto", "w123456.", CrearEmailUsuario("Daniela", "Prieto"), BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));

        }

        private void PrecargarTiposDeGasto()
        {

            CrearTipoDeGasto(new TipoDeGasto("Gastos Personales", "Alimentacion"));
            CrearTipoDeGasto(new TipoDeGasto("Variables", "Consumos"));
            CrearTipoDeGasto(new TipoDeGasto("Gym", "Entrenamiento"));
            
            CrearTipoDeGasto(new TipoDeGasto("Netflix", "Plataforma digital"));
            CrearTipoDeGasto(new TipoDeGasto("Spotify", "Plataforma digital"));
            CrearTipoDeGasto(new TipoDeGasto("Luz", "Servicios básicos"));
            CrearTipoDeGasto(new TipoDeGasto("Agua", "Servicios básicos"));
            CrearTipoDeGasto(new TipoDeGasto("Internet", "Servicios básicos"));
            CrearTipoDeGasto(new TipoDeGasto("Supermercado", "Alimentación"));
            CrearTipoDeGasto(new TipoDeGasto("Restaurante", "Alimentación"));
            CrearTipoDeGasto(new TipoDeGasto("Transporte", "Movilidad"));
            CrearTipoDeGasto(new TipoDeGasto("Gasolina", "Movilidad"));
            CrearTipoDeGasto(new TipoDeGasto("Ropa", "Compras personales"));
        }

        private void PrecargarPagos()
        {

            CrearPago(new Unico(DateTime.Today, 1234, 500, "Heladeria", MetodoDePago.DEBITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("magmar@laEmpresa.com")));



            // Pagos de Magaly (Sistemas)
            CrearPago(new Unico(DateTime.Today, 1235, 1200, "Disco", MetodoDePago.DEBITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("magmar@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-2), 1236, 800, "La Pasiva", MetodoDePago.CREDITO, BuscarTipoDeGasto("Restaurante"), BuscarUsuarioPorEmail("magmar@laEmpresa.com")));

            // Pagos de Juan (Sistemas)
            CrearPago(new Unico(DateTime.Today.AddDays(-3), 1237, 1500, "Movistar", MetodoDePago.CREDITO, BuscarTipoDeGasto("Internet"), BuscarUsuarioPorEmail("juaper@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today, 1238, 4000, "Zara", MetodoDePago.DEBITO, BuscarTipoDeGasto("Ropa"), BuscarUsuarioPorEmail("juaper@laEmpresa.com")));

            // Pagos de Lucia (Sistemas)
            CrearPago(new Unico(DateTime.Today.AddDays(-7), 1239, 950, "Antel", MetodoDePago.DEBITO, BuscarTipoDeGasto("Luz"), BuscarUsuarioPorEmail("lucfer@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today, 1240, 500, "Uber", MetodoDePago.CREDITO, BuscarTipoDeGasto("Transporte"), BuscarUsuarioPorEmail("lucfer@laEmpresa.com")));

            // Pagos de Carolina (Contabilidad)
            CrearPago(new Unico(DateTime.Today.AddDays(-1), 1241, 300, "Netflix", MetodoDePago.DEBITO, BuscarTipoDeGasto("Netflix"), BuscarUsuarioPorEmail("carsua@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today, 1242, 250, "Spotify", MetodoDePago.CREDITO, BuscarTipoDeGasto("Spotify"), BuscarUsuarioPorEmail("carsua@laEmpresa.com")));

            // Pagos de Gonzalo (Marketing)
            CrearPago(new Unico(DateTime.Today.AddDays(-5), 1243, 2000, "Ta-Ta", MetodoDePago.DEBITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("gonram@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today, 1244, 750, "Petrobras", MetodoDePago.DEBITO, BuscarTipoDeGasto("Gasolina"), BuscarUsuarioPorEmail("gonram@laEmpresa.com")));

            // Pagos de Camila (Recursos Humanos)
            CrearPago(new Unico(DateTime.Today, 1245, 1000, "Macri Gym", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Gym"), BuscarUsuarioPorEmail("camtor@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-10), 1246, 1300, "Devoto", MetodoDePago.CREDITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("camtor@laEmpresa.com")));

        }

        #endregion
    }
}