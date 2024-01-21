using System.IO;
using System.Text.Json;
using JeuSAE.classes;

namespace JeuSAE.data;

internal class JsonUtilitaire
{
    public static void Ecriture(object obj, string fileName)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        var jsonString = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(fileName, jsonString);
    }

    public static BaseDeDonnee LireFichier(string fileName)
    {
        string jsoncontenu = File.ReadAllText(fileName);
        var donnee = JsonSerializer.Deserialize<BaseDeDonnee>(jsoncontenu);
        return donnee;
    }
}