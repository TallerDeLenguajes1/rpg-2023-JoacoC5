using EspacioPersonajes;

internal class Program
{
    private static void Main(string[] args)
    {
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        List<Personaje> competidores = GenerarCompetidores(fabrica);
        MostrarCompetidores(competidores);



    }

    public static List<Personaje> GenerarCompetidores(FabricaDePersonajes fabrica)
    {
        List<Personaje> carga = new List<Personaje>();
        Personaje aux;
        for (int i = 0; i < 10; i++)
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

    public static void MostrarCompetidores(List<Personaje> competidores)
    {
        for (int i = 0; i < competidores.Count(); i++)
        {
            Console.WriteLine((i + 1) + ": " + competidores[i].Nombre);
        }
    }
}