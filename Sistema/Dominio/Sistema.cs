using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Clase Sistema 

namespace Dominio
{
    public class Sistema
    {
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Pago> _pagos = new List<Pago>();
        private List<Equipo> _equipos = new List<Equipo>();
        private List<TipoDeGasto> _tiposDeGasto = new List<TipoDeGasto>();

        private static Sistema s_instancia;

    public static Sistema Instancia
        {
            get
            {
                if (s_instancia == null)
                {
                    s_instancia = new Sistema();
                }
                return s_instancia;
            }
        }

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
            u.Email = CrearEmailUsuario(u.Nombre, u.Apellido);
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

        public Usuario Login(string email, string pass)
        {
            Usuario buscado = null;
            Usuario usuario = BuscarUsuarioPorEmail(email);
            if (usuario == null) throw new Exception("Usuario no encontrado");
            if (usuario != null && usuario.Email == email && usuario.Contrasenia == pass) buscado = usuario;
            else throw new Exception("Email o contraseña incorrecto");
            return usuario;
        }

        //Listados

        public List<Pago> ListarPagosPorUsuario(Usuario usuario)
        {
            
            if (usuario == null) throw new Exception("No se encontro el usuario");
            List<Pago> pagosDelUsuario = new List<Pago>();
            foreach (Pago p in _pagos)
            {
                if (p.Usuario.Email == usuario.Email) pagosDelUsuario.Add(p);
            }
            return pagosDelUsuario;
        }

        public List<Pago> ListarPagosPorUsuarioDelMes(Usuario usuario, DateTime fecha)
        {

            if (usuario == null) throw new Exception("No se encontro el usuario");
            List<Pago> pagosDelMes = new List<Pago>();
            List<Pago> pagosDelUsuario = ListarPagosPorUsuario(usuario);

            foreach (Pago p in pagosDelUsuario)
            {
                if (p.PagoEsteMes(fecha)) pagosDelMes.Add(p);
            }
            pagosDelMes.Sort();
            return pagosDelMes;
        }

        public List<Usuario> ListarUsuariosPorEquipo(Equipo equipo)
        {
            if (equipo == null) throw new Exception("El equipo no puede ser nulo");    
            List<Usuario> usuariosDelEquipo = new List<Usuario>();
            foreach (Usuario u in _usuarios)
            {
                if (equipo.Equals(u.Equipo)) usuariosDelEquipo.Add(u);
            }

            usuariosDelEquipo.Sort();
            return usuariosDelEquipo;
        }

        public List<double> ListarMontoDeUsuarios(Equipo equipo)
        {
            if (equipo == null) throw new Exception("El equipo no puede ser nulo");

            List<Usuario> usuariosDelEquipo = ListarUsuariosPorEquipo(equipo);

            List<double> result = new List<double>();
            foreach (Usuario u in usuariosDelEquipo)
            {
                result.Add(MontoTotalPorUsuario(u, DateTime.Today));
            }
            
            return result;
        }

        public List<Pago> ListarPagosPorEquipo(Equipo equipo, DateTime fecha)
        {
            
            List<Pago> pagosDelMes = new List<Pago>();
            List<Usuario> usuarios = ListarUsuariosPorEquipo(equipo);

            foreach (Usuario u in usuarios)
            {
                List<Pago> pagosDelUsuario = ListarPagosPorUsuario(u);
                foreach (Pago p in pagosDelUsuario)
                {
                    if (p.PagoEsteMes(fecha)) pagosDelMes.Add(p);
                }

            }

            pagosDelMes.Sort();
            return pagosDelMes;
        }

        public double MontoTotalPorUsuario(Usuario u, DateTime fecha)
        {
            List<Pago> pagos = ListarPagosPorUsuarioDelMes(u, fecha);
            double total = 0;

            foreach (Pago p in pagos)
            {
                total=+ p.CalcularMontoTotal();

            }
            return total;
        }
        
        public bool ExisteTipoDeGasto(string nombre)
        {
           
            int i = 0;
            bool buscado= false;

            while (!buscado && i < _pagos.Count)
            {
                if (_pagos[i].TipoDeGasto.Nombre == nombre) buscado= true;
                i++;
            }
            return buscado;
        }

     /*   public double MontoTotalPorEquipo(Equipo e, DateTime fecha)
        {
            List<Pago> pagos= ListarPagosPorEquipo(e, fecha);

            double total = 0;
            foreach (Pago p in pagos)
            {
                total = +p.CalcularMontoTotal();

            }
            return total;
        }*/


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

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            Usuario usuarioBuscado = null;
            if (string.IsNullOrEmpty(email)) throw new Exception("El email no puede ser nulo o estar vacio");
            int i = 0;

            while(i< _usuarios.Count && usuarioBuscado==null)
            {
                if (_usuarios[i].Email == email) usuarioBuscado = _usuarios[i];
                i++;
            }

            return usuarioBuscado;
        }

        public Equipo BuscarEquipoPorNombre(string nombre)
        {
            Equipo equipoBuscado = null;
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            int i = 0;
            while (i < _equipos.Count && equipoBuscado == null)
            {
                if (_equipos[i].Nombre.ToLower() == nombre.ToLower()) equipoBuscado = _equipos[i];
                i++;
            }
            return equipoBuscado;
        }

        public TipoDeGasto BuscarTipoDeGasto(string nombre)
        {
            TipoDeGasto tipoBuscado = null;
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede ser nulo o estar vacio");
            int i = 0;
            while(i < _tiposDeGasto.Count && tipoBuscado == null)
            {
                if (_tiposDeGasto[i].Nombre.ToLower() == nombre.ToLower()) tipoBuscado = _tiposDeGasto[i];
                i++;
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
            CrearUsuario(new Usuario(Rol.GERENTE,"Magaly", "Martinez", "a123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.GERENTE,"Victoria", "Garcia", "a123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO,"Juan", "Perez", "b123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Lucia", "Fernandez", "c123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Martin", "Lopez", "d123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Sofia", "Garcia", "e123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Diego", "Rodriguez", "f123456.", BuscarEquipoPorNombre("Sistemas"), DateTime.Today));

            // Contabilidad
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Carolina", "Suarez", "g123456.", BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Andres", "Mendez", "h123456.", BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Paola", "Silva", "i123456.", BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.GERENTE,"Fernando", "Alvarez", "j123456.", BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.GERENTE,"Laura", "Dominguez", "k123456.", BuscarEquipoPorNombre("Contabilidad"), DateTime.Today));

            // Marketing
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Gonzalo", "Ramos", "l123456.", BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.GERENTE, "Valentina", "Cabrera", "m123456.", BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Rodrigo", "Vazquez", "n123456.", BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Florencia", "Mora", "o123456.", BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Sebastian", "Castro", "p123456.", BuscarEquipoPorNombre("Marketing"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.GERENTE, "Julieta", "Rios", "q123456.", BuscarEquipoPorNombre("Marketing"), DateTime.Today));

            // Recursos Humanos
            CrearUsuario(new Usuario(Rol.GERENTE, "Matias", "Ortiz", "r123456.", BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.GERENTE, "Camila", "Torres", "s123456.", BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Nicolas", "Gomez", "t123456.", BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Agustina", "Morales", "u123456.", BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Bruno", "Sanchez", "v123456.", BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));
            CrearUsuario(new Usuario(Rol.EMPLEADO, "Daniela", "Prieto", "w123456.", BuscarEquipoPorNombre("Recursos Humanos"), DateTime.Today));

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
            CrearPago(new Recurrente(new DateTime(2021, 06, 12), new DateTime(2026, 06, 12), 300, "Netflix", MetodoDePago.CREDITO, BuscarTipoDeGasto("Netflix"), BuscarUsuarioPorEmail("paosil@laEmpresa.com")));


            // ------------------ PAGOS ÚNICOS (17) ------------------
            CrearPago(new Unico(DateTime.Today, 1001, 500, "Heladeria", MetodoDePago.DEBITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("magmar@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-1), 1002, 1200, "Farmacia", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("juaper@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-2), 1003, 800, "Restaurante", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Restaurante"), BuscarUsuarioPorEmail("lucfer@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-3), 1004, 2000, "Tienda Ropa", MetodoDePago.DEBITO, BuscarTipoDeGasto("Ropa"), BuscarUsuarioPorEmail("marlop@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-4), 1005, 3500, "Electrodomésticos", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("sofgar@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-5), 1006, 1000, "Taxi", MetodoDePago.DEBITO, BuscarTipoDeGasto("Transporte"), BuscarUsuarioPorEmail("dierod@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-6), 1007, 450, "Cine", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Gastos Personales"), BuscarUsuarioPorEmail("carsua@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-7), 1008, 2500, "Supermercado", MetodoDePago.DEBITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("paosil@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-8), 1009, 1500, "Gasolina", MetodoDePago.DEBITO, BuscarTipoDeGasto("Gasolina"), BuscarUsuarioPorEmail("feralv@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-9), 1010, 600, "Comida rápida", MetodoDePago.CREDITO, BuscarTipoDeGasto("Restaurante"), BuscarUsuarioPorEmail("laudom@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-10), 1011, 900, "Cafetería", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Restaurante"), BuscarUsuarioPorEmail("gonram@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-11), 1012, 5000, "Viaje", MetodoDePago.CREDITO, BuscarTipoDeGasto("Transporte"), BuscarUsuarioPorEmail("valcab@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-12), 1013, 200, "Panadería", MetodoDePago.DEBITO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("rodvaz@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-13), 1014, 300, "Verdulería", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Supermercado"), BuscarUsuarioPorEmail("flomor@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-14), 1015, 700, "Farmacia", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("sebcas@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-15), 1016, 4000, "Celular", MetodoDePago.DEBITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("julrio@laEmpresa.com")));
            CrearPago(new Unico(DateTime.Today.AddDays(-16), 1017, 1800, "Perfumería", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Gastos Personales"), BuscarUsuarioPorEmail("matort@laEmpresa.com")));
            // ------------------ PAGOS RECURRENTES (25) ------------------
            // ---- 20 activos (FechaFin = DateTime.MinValue) ----
            CrearPago(new Recurrente(new DateTime(2023, 01, 10), DateTime.MinValue, 300, "Netflix", MetodoDePago.CREDITO, BuscarTipoDeGasto("Netflix"), BuscarUsuarioPorEmail("camtor@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 02, 15), DateTime.MinValue, 200, "Spotify", MetodoDePago.CREDITO, BuscarTipoDeGasto("Spotify"), BuscarUsuarioPorEmail("nicgom@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 03, 05), DateTime.MinValue, 1500, "Luz", MetodoDePago.DEBITO, BuscarTipoDeGasto("Luz"), BuscarUsuarioPorEmail("agumor@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 04, 01), DateTime.MinValue, 1200, "Agua", MetodoDePago.DEBITO, BuscarTipoDeGasto("Agua"), BuscarUsuarioPorEmail("brusan@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 05, 20), DateTime.MinValue, 1800, "Internet", MetodoDePago.CREDITO, BuscarTipoDeGasto("Internet"), BuscarUsuarioPorEmail("danpri@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 06, 11), DateTime.MinValue, 3500, "Alquiler", MetodoDePago.DEBITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("magmar@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 07, 08), DateTime.MinValue, 700, "Gimnasio", MetodoDePago.EFECTIVO, BuscarTipoDeGasto("Gym"), BuscarUsuarioPorEmail("juaper@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 08, 17), DateTime.MinValue, 250, "Disney+", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("lucfer@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 09, 09), DateTime.MinValue, 400, "Amazon Prime", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("marlop@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 10, 02), DateTime.MinValue, 1000, "Seguro Auto", MetodoDePago.DEBITO, BuscarTipoDeGasto("Transporte"), BuscarUsuarioPorEmail("sofgar@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 10, 18), DateTime.MinValue, 2200, "Seguro Salud", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("dierod@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 11, 04), DateTime.MinValue, 600, "HBO Max", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("carsua@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 11, 20), DateTime.MinValue, 3000, "Guardería", MetodoDePago.DEBITO, BuscarTipoDeGasto("Gastos Personales"), BuscarUsuarioPorEmail("paosil@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2023, 12, 01), DateTime.MinValue, 900, "Clases Inglés", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("feralv@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2024, 01, 10), DateTime.MinValue, 2500, "Teléfono", MetodoDePago.DEBITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("laudom@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2024, 02, 15), DateTime.MinValue, 500, "Paramount+", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("gonram@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2024, 03, 05), DateTime.MinValue, 1600, "Gas", MetodoDePago.DEBITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("valcab@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2024, 04, 01), DateTime.MinValue, 2000, "Hogar", MetodoDePago.DEBITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("rodvaz@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2024, 05, 20), DateTime.MinValue, 800, "Revista Online", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("flomor@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2024, 06, 10), DateTime.MinValue, 450, "Dropbox", MetodoDePago.CREDITO, BuscarTipoDeGasto("Variables"), BuscarUsuarioPorEmail("sebcas@laEmpresa.com")));
            // ---- 5 finalizados (con FechaFin distinta de DateTime.MinValue) ----
            CrearPago(new Recurrente(new DateTime(2021, 06, 12), new DateTime(2023, 06, 12), 300, "Netflix", MetodoDePago.CREDITO, BuscarTipoDeGasto("Netflix"), BuscarUsuarioPorEmail("julrio@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2021, 07, 01), new DateTime(2022, 07, 01), 200, "Spotify", MetodoDePago.CREDITO, BuscarTipoDeGasto("Spotify"), BuscarUsuarioPorEmail("matort@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2021, 08, 05), new DateTime(2023, 08, 05), 1500, "Luz", MetodoDePago.DEBITO, BuscarTipoDeGasto("Luz"), BuscarUsuarioPorEmail("camtor@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2021, 09, 10), new DateTime(2022, 09, 10), 1800, "Internet", MetodoDePago.CREDITO, BuscarTipoDeGasto("Internet"), BuscarUsuarioPorEmail("nicgom@laEmpresa.com")));
            CrearPago(new Recurrente(new DateTime(2021, 10, 15), new DateTime(2022, 10, 15), 1200, "Agua", MetodoDePago.DEBITO, BuscarTipoDeGasto("Agua"), BuscarUsuarioPorEmail("agumor@laEmpresa.com")));
        }

        #endregion
    }
}