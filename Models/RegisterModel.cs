namespace CampusFranceProject.Models
{
    public class RegisterModel
    {
        // ── Informations de connexion ──────────────────────
        public string Email { get; set; }
        public string MotDePasse { get; set; }

        // ── Informations personnelles ──────────────────────
        public string Civilite { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string PaysResidence { get; set; }
        public string PaysNationalite { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }

        // ── Profil ─────────────────────────────────────────
        public string Profil { get; set; }

        // ── Complémentaires Étudiant / Chercheur ───────────
        public string DomaineEtudes { get; set; }
        public string NiveauEtude { get; set; }

        // ── Complémentaires Institutionnel ─────────────────
        public string Fonction { get; set; }
        public string TypeOrganisme { get; set; }
        public string NomOrganisme { get; set; }
    }

    public class RegisterDataRoot
    {
        public RegisterModel Etudiant { get; set; }
        public RegisterModel Chercheur { get; set; }
        public RegisterModel Institutionnel { get; set; }
    }
}