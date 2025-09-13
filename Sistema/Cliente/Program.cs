using Dominio;
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
                  
                    break;
                case "3":
                   
                    break;
                case "4": MostrarMenuEquipo();
                   
                    break;
                case "5":
                  
                    break;
                case "6":
                  
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
        Console.WriteLine("2 - Listar mis Pagos");
        Console.WriteLine("3 - Crear un Usuario");
        Console.WriteLine("4 - Equipos");
        Console.WriteLine("5 - ");
        Console.WriteLine("6 - ");
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
        Console.WriteLine("3 - ");
        Console.WriteLine("4 - ");
        Console.WriteLine("5 - ");
        Console.WriteLine("6 - ");
        Console.WriteLine("0 - Salir");
    }

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