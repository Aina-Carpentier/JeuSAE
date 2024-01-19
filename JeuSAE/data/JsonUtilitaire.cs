using JeuSAE.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JeuSAE.data
{
    internal class JsonUtilitaire
    {
        public static void Ecriture(object obj, string fileName)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);
            File.WriteAllText(fileName, jsonString);
        }

        public static BaseDeDonnee LireFichier(string fileName)
        {
            var jsoncontenu = File.ReadAllText(fileName);
            var donnee = JsonSerializer.Deserialize<BaseDeDonnee>(jsoncontenu);
            return donnee;
        }
    }
}
