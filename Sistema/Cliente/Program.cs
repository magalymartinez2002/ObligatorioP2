using Dominio;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

internal class Program
{

    private static Sistema miSistema;

    private static void Main(string[] args)
    {

        miSistema = new Sistema();

        string opcion = "";
        while (opcion != "0")
        {
            MostrarMenu();
            opcion = LeerTexto("Ingrese una opcion -> ");

            switch (opcion)
            {
                case "1":
                    ListarUsuarios();
                    break;
                case "2":
                    string email=LeerTexto("Ingrese el email del usuario: ");
                    ListarPagosPorUsuario(email);

                    break;
                case "3":
                    CrearUsuario();

                    break;
                case "4":
                    MostrarMenuEquipo();
                    opcion = LeerTexto("Ingrese una opcion -> ");

                    switch (opcion)
                    {
                        case "1":
                            ListarEquipos();
                            
                            break;
                        case "2":
                            string equipo=LeerTexto("Ingrese el nombre del equipo: ");
                            ListarUsuariosPorEquipo(equipo);

                            break;
                     
                        case "0":
                            Console.WriteLine("Saliendo...");
                            break;
                        default:
                            MostrarError("ERROR: Opcion inválida");
                            PressToContinue();
                            break;
                    }

                    break;
               
                case "0":
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    MostrarError("ERROR: Opcion inválida");
                    PressToContinue();
                    break;
            }
        }
    }

    static void MostrarMenu()
    {
        Console.Clear();
       // MostrarMensajeColor(ConsoleColor.Cyan, "****************");
      //  MostrarMensajeColor(ConsoleColor.Cyan, "      MENU      ");
      //  MostrarMensajeColor(ConsoleColor.Cyan, "****************");
        Console.WriteLine();
        Console.WriteLine("1 - Listar Usuarios");
        Console.WriteLine("2 - Listar Pagos por un usuario");
        Console.WriteLine("3 - Crear un Usuario");
        Console.WriteLine("4 - Equipos");
        Console.WriteLine("0 - Salir");
    }

    static void MostrarMenuEquipo()
    {
        Console.Clear();
        // MostrarMensajeColor(ConsoleColor.Cyan, "************************");
        //  MostrarMensajeColor(ConsoleColor.Cyan, "      MENU  EQUIPO    ");
        //  MostrarMensajeColor(ConsoleColor.Cyan, "***********************");
        Console.WriteLine();
        Console.WriteLine("1 - Listar todos los Equipos");
        Console.WriteLine("2 - Listar Usuarios por Equipos");
        Console.WriteLine("0 - Salir");
    }


    static void CrearUsuario()
    {
        Console.Clear();
        MostrarMensajeColor(ConsoleColor.Yellow, "Creacion de un nuevo Usuario");
        try
        {

            string nombre = LeerTexto("Ingrese el nombre: ");
            string apellido = LeerTexto("Ingrese el apellido: ");
            string contrasenia = LeerTexto("Ingrese la contrasenia: ");
            string equipoNombre = LeerTexto("Ingrese el nombre del equipo: ");
            DateTime fechaIngreso= LeerFecha("Ingrese la fecha de ingreso");

            Equipo equipo= miSistema.BuscarEquipoPorNombre(equipoNombre);
            string email = miSistema.CrearEmailUsuario(nombre, apellido);

            miSistema.CrearUsuario(new Usuario(nombre, apellido, contrasenia, email,equipo,fechaIngreso));

            MostrarExito("Se ha creado el usuario correctamente. El email generado es: "+email);

        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);



        }
        PressToContinue();
    }

    #region Listados

    static void ListarUsuarios()
    {
        Console.Clear();
        MostrarMensajeColor(ConsoleColor.Yellow, "Listado de todos los Usuarios");
        Console.WriteLine();

        try
        {
            List<Usuario> usuarios= miSistema.Usuarios;
            if (usuarios.Count == 0) throw new Exception("No se encontraron usuarios en el sistema");

            foreach (Usuario u in usuarios)
            {
                Console.WriteLine(u);
            }
        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);
        }
        PressToContinue();
    }

    static void ListarPagosPorUsuario(string email)
    {
        Console.Clear();
        MostrarMensajeColor(ConsoleColor.Yellow, "Listado de todos los Pagos");
        Console.WriteLine();

        try
        {
            
            List<Pago> pagos = miSistema.ListarPagosPorUsuario(email);
            if (pagos.Count == 0) throw new Exception("No se encontraron pagos en el sistema");

            foreach (Pago p in pagos)
            {
                Console.WriteLine(p);
            }
        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);
        }
        PressToContinue();
    }

    static void ListarEquipos()
    {
        Console.Clear();
        MostrarMensajeColor(ConsoleColor.Yellow, "Listado de todos los Equipos");
        Console.WriteLine();

        try
        {
            List<Equipo> equipos = miSistema.Equipos;
            if (equipos.Count == 0) throw new Exception("No se encontraron equipos en el sistema");

            foreach (Equipo e in equipos)
            {
                Console.WriteLine(e);
            }
        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);
        }
        PressToContinue();
    }

    static void ListarUsuariosPorEquipo(string equipo)
    {
        Console.Clear();
        MostrarMensajeColor(ConsoleColor.Yellow, $"Listado del equipo: {equipo}");
        Console.WriteLine();

        try
        {
            List<Usuario> usuarios = miSistema.ListarUsuariosPorEquipo(equipo);
            if (usuarios.Count == 0) throw new Exception("No se encontraron usuarios en ese equipo en el sistema");

            foreach (Usuario u in usuarios)
            {
                Console.WriteLine(u);
            }
        }
        catch (Exception ex)
        {
            MostrarError(ex.Message);
        }
        PressToContinue();
    }

    #endregion


    #region Métodos de lectura de datos por consola
    static string LeerTexto(string mensaje)
    {
        Console.Write(mensaje);
        string datos = Console.ReadLine();
        return datos;
    }

    static int LeerEntero(string mensaje)
    {
        int numeroEntero = 0;
        Console.Write(mensaje);
        string numeroString = Console.ReadLine();

        while (!int.TryParse(numeroString, out numeroEntero))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"Ingreso incorrecto. {mensaje}");
            numeroString = Console.ReadLine();
        }

        return numeroEntero;
    }

    static DateTime LeerFecha(string mensaje)
    {
        bool exito = false;
        DateTime fecha = new DateTime();
        while (!exito)
        {
            Console.Write(mensaje + " [DD/MM/YYYY]:");
            exito = DateTime.TryParse(Console.ReadLine(), out fecha);

            if (!exito) MostrarError("ERROR: Debe ingresar una fecha en formato DD/MM/YYYY");
        }
        return fecha;
    }

    static bool LeerBooleano(string mensaje)
    {
        bool exito = false;
        bool resultado = false;
        while (!exito)
        {
            Console.Write(mensaje + " [S/N]:");
            string booleanoString = Console.ReadLine();
            if (booleanoString.ToUpper() == "S")
            {
                resultado = true;
                exito = true;
            }
            else if (booleanoString.ToUpper() == "N")
            {
                resultado = false;
                exito = true;
            }

            if (!exito) MostrarError("ERROR: Debe ingresar solo S o N");
        }

        return resultado;
    }

   

    static void MostrarMensajeColor(ConsoleColor color1, string mensaje)
    {
        Console.ForegroundColor = color1;
        Console.WriteLine(mensaje);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    static void MostrarError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(mensaje);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    static void MostrarExito(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(mensaje);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    static void PressToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Presione cualquier tecla para volver al menu...");
        Console.ReadKey();
    }

#endregion
}