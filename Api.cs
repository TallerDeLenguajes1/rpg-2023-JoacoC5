using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using Pokemon;
namespace Api;

public class ConsumoApi
{
    private string nombre;

    public string Nombre { get => nombre; set => nombre = value; }


    public void Consulta(int id)
    {
        var url = "https://pokeapi.co/api/v2/pokemon?limit=151";
        var request = (HttpWebRequest)WebRequest.Create(url + id);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;

                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        ListaPokemon ListPersonaje = JsonSerializer.Deserialize<ListaPokemon>(responseBody);

                        Nombre = ListPersonaje.listaPokemones[id].nombre;

                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("No se pudo acceder a la API");
            throw;
        }
    }
}

