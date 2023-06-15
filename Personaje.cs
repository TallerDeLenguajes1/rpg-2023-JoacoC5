namespace EspacioPersonajes;

public class Personaje
{
    //DATOS PERSONALES
    private string tipo = String.Empty;
    private string nombre = String.Empty;
    private string apodo = String.Empty;
    private DateTime fechaNacimineto;
    private int edad;
    //CARACTERISTICAS INDIVIDUALES
    private int velocidad;
    private int destreza;
    private int fuerza;
    private int nivel;
    private int armadura;
    private int salud;

    public string Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public DateTime FechaNacimineto { get => fechaNacimineto; set => fechaNacimineto = value; }
    public int Edad { get => edad; set => edad = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Salud { get => salud; set => salud = value; }
}