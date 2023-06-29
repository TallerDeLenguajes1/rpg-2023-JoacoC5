using System.Text.Json;
using System.IO;

namespace EspacioPersonajes;

public class Personaje
{
    //DATOS PERSONALES
    //private string tipo = String.Empty;
    private string nombre = String.Empty;
    //private string apodo = String.Empty;
    //private DateTime fechaNacimineto;
    //private int edad;
    //CARACTERISTICAS INDIVIDUALES
    private int velocidad;
    private int destreza;
    private int fuerza;
    private int nivel;
    private int armadura;
    private int salud;
    private int indice;

    //public string Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    //public string Apodo { get => apodo; set => apodo = value; }
    //public DateTime FechaNacimineto { get => fechaNacimineto; set => fechaNacimineto = value; }
    //public int Edad { get => edad; set => edad = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Indice { get => indice; set => indice = value; }

    public int Ataque(int destreza, int fuerza, int nivel)
    {
        return (destreza * fuerza * nivel);
    }

    public int Defensa(int armadura, int velocidad)
    {
        return (armadura * velocidad);
    }
}

public class FabricaDePersonajes
{
    public Personaje crearPersonaje()
    {
        var personaje = new Personaje();
        Random random = new Random();

        string[] opcionesNombres = {"Iron Man", "Capitan America", "Hulk", "Thor", "Ojo de Halcon", "Viuda Negra", "Bruja Escarlata", "Dr. Strange", "Spider-Man", "Vision",
                              "Batman", "Superman", "Aquaman", "Mujer Maravilla", "Linterna Verde", "Flash", "Cyborg", "Shazam", "Flecha Verde", "Detective Marciano",
                              "Profesor X", "Wolverine", "Ciclope", "Phoenix", "Bestia", "Mystique", "Magneto", "Tormenta", "Coloso", "Angelo",
                              "La cosa", "Hombre Antorcha", "Hombre Elastico", "Mujer Invisible", "Ant Man", "Capitana Marvel", "Falcon", "Winter Soldier", "Black Panther", "Quick Silver",
                              "Frost", "Firestorm", "Canario Negro", "Canario Blanco", "Capitan Frio", "Vibe", "Arsenal", "Spartan", "Atomo", "Wild Dog"};

        personaje.Indice = random.Next(0, 50);
        personaje.Nombre = opcionesNombres[personaje.Indice];
        personaje.Velocidad = random.Next(1, 11);
        personaje.Destreza = random.Next(1, 6);
        personaje.Fuerza = random.Next(1, 11);
        personaje.Nivel = random.Next(1, 11);
        personaje.Armadura = random.Next(1, 11);
        personaje.Salud = 100;

        return personaje;
    }


}


public class PersonajesJson
{
    public void GuardarPersonajes(List<Personaje> lista, string archivo)
    {
        string personajesJson = JsonSerializer.Serialize(lista);
        File.WriteAllText(archivo, personajesJson);
    }

    public List<Personaje> LeerPersonajes(string archivo)
    {
        string jsonString = File.ReadAllText(archivo);
        List<Personaje> personajesdes = JsonSerializer.Deserialize<List<Personaje>>(jsonString);
        return personajesdes;
    }

    public bool ExisteArchivo(string archivo)
    {
        return File.Exists(archivo);
    }
}