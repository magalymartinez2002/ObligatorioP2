using Dominio;

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
                   
                    break;
                case "2":
                  
                    break;
                case "3":
                   
                    break;
                case "4":
                   
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
        Console.WriteLine("1 - Crear una marca");
        Console.WriteLine("2 - Listar todas las marcas");
        Console.WriteLine("3 - Crear un auto");
        Console.WriteLine("4 - Listar todos los autos");
        Console.WriteLine("5 - Listar autos posteriores a un año");
        Console.WriteLine("6 - Listar autos por marca");
        Console.WriteLine("0 - Salir");
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