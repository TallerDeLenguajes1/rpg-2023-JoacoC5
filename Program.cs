using EspacioPersonajes;
using Api;
using Pokemon;

internal class Program
{
    private static void Main(string[] args)
    {
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        List<Personaje> competidores = GenerarCompetidores(fabrica);
        List<Personaje> semifinalistas = new List<Personaje>();
        List<Personaje> finalistas = new List<Personaje>();
        Console.WriteLine("Ingrese el archivo de los competidores: ");
        string? archivo = Console.ReadLine();
        PersonajesJson auxjson = new PersonajesJson();
        int comp1, comp2, auxArchivo;

        if (archivo != null)
        {
            if (!auxjson.ExisteArchivo(archivo))
            {
                auxjson.GuardarPersonajes(competidores, archivo);
                competidores = auxjson.LeerPersonajes(archivo);
            }
            else
            {
                MostrarCompetidores(auxjson.LeerPersonajes(archivo));
                Console.WriteLine("\nDesea utilizar estos concursantes? (1=si - 0=no): ");
                int.TryParse(Console.ReadLine(), out auxArchivo);
                if (auxArchivo == 1)
                {
                    competidores = auxjson.LeerPersonajes(archivo);
                }
            }

            Console.WriteLine("\n-----PRIMERA FASE-----\n");
            while (competidores.Count() > 1)
            {
                MostrarCompetidores(competidores);
                Console.WriteLine("\nIngrese un competidor: ");
                int.TryParse(Console.ReadLine(), out comp1);
                Console.WriteLine("Ingrese otro competidor: ");
                int.TryParse(Console.ReadLine(), out comp2);

                competidores = Batalla(competidores, comp1, comp2, semifinalistas);
            }

            Console.WriteLine("\n\n\n-----SEMIFINALES-----\n");
            while (semifinalistas.Count() > 1)
            {
                MostrarCompetidores(semifinalistas);
                Console.WriteLine("\nIngrese un competidor: ");
                int.TryParse(Console.ReadLine(), out comp1);
                Console.WriteLine("Ingrese otro competidor: ");
                int.TryParse(Console.ReadLine(), out comp2);

                semifinalistas = Batalla(semifinalistas, comp1, comp2, finalistas);
            }

            Console.WriteLine("\n\n\n-----FINAL-----\n");
            MostrarCompetidores(finalistas);
            Console.WriteLine("\nIngrese un competidor: ");
            int.TryParse(Console.ReadLine(), out comp1);
            Console.WriteLine("Ingrese otro competidor: ");
            int.TryParse(Console.ReadLine(), out comp2);

            finalistas = Batalla(finalistas, comp1, comp2, finalistas);
        }
        else
        {
            Console.WriteLine("No se ingreso un nombre de archivo valido");
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

    private static List<Personaje> Batalla(List<Personaje> competidores, int comp1, int comp2, List<Personaje> semifinalistas)
    {
        Personaje aux1 = competidores[comp1 - 1], aux2 = competidores[comp2 - 1];
        Random random = new Random();

        Console.WriteLine("\n------BATALLA------\n");
        Console.WriteLine(aux1.Nombre + " VS " + aux2.Nombre + "\n");

        while ((aux1.Salud > 0) && (aux2.Salud > 0))
        {
            int efectividad = random.Next(1, 101);
            int danio1 = ((aux1.Ataque(aux1.Destreza, aux1.Fuerza, aux1.Nivel) * efectividad)
                            - (aux1.Defensa(aux2.Armadura, aux2.Velocidad))) / 500;
            aux2.Salud -= danio1;
            int danio2 = ((aux2.Ataque(aux2.Destreza, aux2.Fuerza, aux2.Nivel) * efectividad)
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