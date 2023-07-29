using EspacioPersonajes;
using Api;
using Pokemon;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("\n" + @"|==========================|
|==========================|
|                          |
|       BIENVENIDO A       |
|         LA LIGA          |
|         POKEMON          |
|                          |
'--------------------------'" + "\n");

        Console.WriteLine("Preparando los Pokemon, aguarde un momento...\n");

        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        List<Personaje> competidores = GenerarCompetidores(fabrica);
        List<Personaje> semifinalistas = new List<Personaje>();
        List<Personaje> finalistas = new List<Personaje>();
        Console.WriteLine("Ingrese la Liga en la que desea competir: ");
        string? archivo = Console.ReadLine();
        archivo += ".json";
        PersonajesJson auxjson = new PersonajesJson();
        int auxArchivo;

        if (archivo != null)
        {
            if (!auxjson.ExisteArchivo(archivo))
            {
                auxjson.GuardarPersonajes(competidores, archivo);
            }
            else
            {
                if (auxjson.LeerPersonajes(archivo).Count() != 8)
                {
                    Console.WriteLine("\nEl archivo seleccionado no cumple con los requisitos necesarios\nSe generaran nuevos competidores\n");
                    auxjson.GuardarPersonajes(competidores, archivo);
                }
                else
                {
                    Console.WriteLine("");
                    MostrarCompetidores(auxjson.LeerPersonajes(archivo));
                    Console.WriteLine("\nDesea utilizar estos concursantes? (1=si - 0=no): ");
                    int.TryParse(Console.ReadLine(), out auxArchivo);
                    if (auxArchivo == 1)
                    {
                        competidores = auxjson.LeerPersonajes(archivo);
                    }
                }

            }

            Console.WriteLine("\n-----PRIMERA FASE-----\n");
            semifinalistas = Ronda(competidores);
            Console.WriteLine("\n\n\n-----SEMIFINALES-----\n");
            finalistas = Ronda(semifinalistas);
            Console.WriteLine("\n\n\n-----FINAL-----\n");
            finalistas = Ronda(finalistas);

            Console.WriteLine(@"|==========================|
|==========================|
|                          |
|         GANADOR          |
|        DE LA LIGA        |
|        " + finalistas[0].Nombre.ToUpper().PadRight(10) + @"        |
|                          |
'--------------------------'");
        }
        else
        {
            Console.WriteLine("\nNo se ingreso un nombre de archivo valido\n");
        }
    }

    private static List<Personaje> GenerarCompetidores(FabricaDePersonajes fabrica)
    {
        List<Personaje> carga = new List<Personaje>();
        Personaje auxCarga;

        for (int i = 0; i < 8; i++)
        {
            auxCarga = fabrica.crearPersonaje();
            carga.Add(auxCarga);

            for (int j = 0; j < i; j++)
            {
                if ((carga[j].Indice) == auxCarga.Indice)
                {
                    carga.RemoveAt(j);
                    i--;
                    break;
                }
            }
        }
        return carga;
    }

    private static void MostrarCompetidores(List<Personaje> competidores)
    {
        for (int i = 0; i < competidores.Count(); i++)
        {
            Console.WriteLine((i + 1) + ": " + competidores[i].Nombre);
        }
    }

    private static List<Personaje> Ronda(List<Personaje> competidores)
    {
        List<Personaje> ganadores = new List<Personaje>();
        int comp1, comp2;
        while (competidores.Count() > 1)
        {
            MostrarCompetidores(competidores);
            do
            {
                Console.WriteLine("\nIngrese un competidor: ");
                int.TryParse(Console.ReadLine(), out comp1);
            } while (comp1 >= competidores.Count());
            do
            {
                Console.WriteLine("Ingrese otro competidor: ");
                int.TryParse(Console.ReadLine(), out comp2);
            } while (comp2 > competidores.Count());

            competidores = Batalla(competidores, comp1, comp2, ganadores);
        }
        return ganadores;
    }

    private static List<Personaje> Batalla(List<Personaje> competidores, int comp1, int comp2, List<Personaje> semifinalistas)
    {
        Personaje aux1 = competidores[comp1 - 1], aux2 = competidores[comp2 - 1];
        Random random = new Random();

        Console.WriteLine("\n------BATALLA------\n");
        Console.WriteLine(aux1.Nombre + " VS " + aux2.Nombre + "\n");
        int efectividad, danio1, danio2;

        while ((aux1.Salud > 0) && (aux2.Salud > 0))
        {
            efectividad = random.Next(1, 101);
            danio1 = ((aux1.Ataque(aux1.Destreza, aux1.Fuerza, aux1.Nivel) * efectividad)
                            - (aux1.Defensa(aux2.Armadura, aux2.Velocidad))) / 500;
            efectividad = random.Next(1, 101);
            aux2.Salud -= danio1;
            danio2 = ((aux2.Ataque(aux2.Destreza, aux2.Fuerza, aux2.Nivel) * efectividad)
                            - (aux2.Defensa(aux2.Armadura, aux2.Velocidad))) / 500;
            aux1.Salud -= danio2;
            Console.WriteLine("Salud1: " + aux1.Salud + " || " + "Salud2: " + aux2.Salud);
        }

        if ((aux1.Salud <= 0) && (aux1.Salud < aux2.Salud))
        {

            Console.WriteLine("\nGANADOR: " + aux2.Nombre + "\n");
            aux2.Salud = 100;
            semifinalistas.Add(competidores[comp2 - 1]);
            if (competidores.Count() > 1)
            {
                competidores.Remove(aux2);
                competidores.Remove(aux1);
            }
            else
            {
                competidores = new List<Personaje>();
            }
        }
        else
        {
            if ((aux2.Salud <= 0) && (aux2.Salud < aux1.Salud))
            {
                Console.WriteLine("\nGANADOR: " + aux1.Nombre + "\n");
                aux1.Salud = 100;
                semifinalistas.Add(competidores[comp1 - 1]);
                if (competidores.Count() > 1)
                {
                    competidores.Remove(aux2);
                    competidores.Remove(aux1);
                }
                else
                {
                    competidores = new List<Personaje>();
                }
            }
        }

        return competidores;
    }
}