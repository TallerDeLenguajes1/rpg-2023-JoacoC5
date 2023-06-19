using EspacioPersonajes;

internal class Program
{
    private static void Main(string[] args)
    {
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        List<Personaje> competidores = GenerarCompetidores(fabrica);
        while (competidores.Count() > 1)
        {
            MostrarCompetidores(competidores);
            int comp1, comp2;
            Console.WriteLine("\nIngrese un competidor: ");
            int.TryParse(Console.ReadLine(), out comp1);
            Console.WriteLine("Ingrese otro competidor: ");
            int.TryParse(Console.ReadLine(), out comp2);

            competidores = Batalla(competidores, comp1, comp2);

        }


    }

    private static List<Personaje> GenerarCompetidores(FabricaDePersonajes fabrica)
    {
        List<Personaje> carga = new List<Personaje>();
        Personaje aux;
        for (int i = 0; i < 16; i++)
        {
            aux = fabrica.crearPersonaje();
            if (!carga.Contains(aux)) //APARECE MAS DE UNA VEZ EL MISMO
            {
                carga.Add(aux);
            }
            else
            {
                i--;
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

    private static List<Personaje> Batalla(List<Personaje> competidores, int comp1, int comp2)
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
            aux2.Salud = aux2.Salud - danio1;
            int danio2 = ((aux2.Ataque(aux2.Destreza, aux2.Fuerza, aux2.Nivel) * efectividad)
                            - (aux2.Defensa(aux2.Armadura, aux2.Velocidad))) / 500;
            aux1.Salud = aux1.Salud - danio2;
            Console.WriteLine("Salud1: " + aux1.Salud + " || " + "Salud2: " + aux2.Salud);
        }

        if ((aux1.Salud <= 0) && (aux1.Salud < aux2.Salud))
        {
            competidores.RemoveAt(comp1 - 1);
            Console.WriteLine("\nGANADOR: " + aux2.Nombre + "\n");
            aux2.Salud = 100;
        }
        else
        {
            if ((aux2.Salud <= 0) && (aux2.Salud < aux1.Salud))
            {
                competidores.RemoveAt(comp2 - 1);
                Console.WriteLine("\nGANADOR: " + aux1.Nombre + "\n");
                aux1.Salud = 100;
            }
        }

        return competidores;
    }
}