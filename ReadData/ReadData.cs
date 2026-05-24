using System;
using System.IO;
using System.Text.Json;
using CampusFranceProject.Models;

namespace CampusFranceProject.ReadData
{
    public class JsonDataReader
    {
        private readonly RegisterDataRoot _data;

        public JsonDataReader()
        {
            string chemin = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "TestData", "RegisterData.json"
            );

            string json = File.ReadAllText(chemin);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _data = JsonSerializer.Deserialize<RegisterDataRoot>(json, options);
        }

        public RegisterModel GetProfil(string profil)
        {
            return profil switch
            {
                "etudiant" => _data.Etudiant,
                "chercheur" => _data.Chercheur,
                "institutionnel" => _data.Institutionnel,
                _ => throw new ArgumentException($"Profil inconnu dans le JSON : {profil}")
            };
        }
    }
}