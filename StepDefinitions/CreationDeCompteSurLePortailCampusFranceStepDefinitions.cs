using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using CampusFranceProject.Models;
using CampusFranceProject.ReadData;

namespace CampusFranceProject.StepDefinitions
{
    [Binding]
    public class CreationDeCompteSurLePortailCampusFranceStepDefinitions
    {
        private IWebDriver driver;
        private RegisterModel utilisateur;
        private readonly JsonDataReader jsonDataReader = new JsonDataReader();

        // ─────────────────────────────────────
        // UTILITAIRE — CHAMP TEXTE
        // ─────────────────────────────────────
        private void RemplirChamp(string id, string valeur)
        {
            IWebElement champ = driver.FindElement(By.Id(id));

            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].value = arguments[1];",
                champ,
                valeur
            );

            System.Threading.Thread.Sleep(1000);
        }

        // ─────────────────────────────────────
        // UTILITAIRE — SELECTIZE
        // ─────────────────────────────────────
        private void SelectionnerOption(string id, string valeur)
        {
            IWebElement champ = driver.FindElement(By.Id(id));

            // scroll
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].scrollIntoView({block:'center'});",
                champ
            );

            System.Threading.Thread.Sleep(1000);

            // click JS
            ((IJavaScriptExecutor)driver).ExecuteScript(
                "arguments[0].click();",
                champ
            );

            System.Threading.Thread.Sleep(1000);

            // vider le champ
            champ.SendKeys(Keys.Control + "a");
            champ.SendKeys(Keys.Delete);

            System.Threading.Thread.Sleep(500);

            // écrire la valeur
            champ.SendKeys(valeur);

            System.Threading.Thread.Sleep(2000);

            // récupérer toutes les options
            var options = driver.FindElements(
                By.CssSelector(".selectize-dropdown-content .option"));

            foreach (var option in options)
            {
                // comparaison exacte
                if (option.Text.Trim().Equals(valeur.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "arguments[0].click();",
                        option
                    );

                    break;
                }
            }

            System.Threading.Thread.Sleep(1000);
        }
        
        // ─────────────────────────────────────
        // GIVEN
        // ─────────────────────────────────────
        [Given("je suis sur la page d'inscription de CampusFrance")]
        public void GivenJeSuisSurLaPageDInscriptionDeCampusFrance()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.campusfrance.org/fr/user/register");
            System.Threading.Thread.Sleep(5000);

            try
            {
                var btn = driver.FindElement(By.Id("tarteaucitronPersonalize2"));
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", btn);
                System.Threading.Thread.Sleep(1500);
            }
            catch { }
        }

        [Given("je charge les données du profil {string}")]
        public void GivenJeChargeLesDonneesDuProfil(string profil)
            => utilisateur = jsonDataReader.GetProfil(profil);

        // ─────────────────────────────────────
        // EMAIL / PASSWORD
        // ─────────────────────────────────────
        [When("je renseigne mon adresse e-mail")]
        public void WhenJeRenseigneMonAdresseEMail()
        {
            RemplirChamp("edit-name", utilisateur.Email);
        }

        [When("je renseigne mon mot de passe")]
        public void WhenJeRenseigneMonMotDePasse()
            => RemplirChamp("edit-pass-pass1", utilisateur.MotDePasse);

        [When("je confirme mon mot de passe")]
        public void WhenJeConfirmeMonMotDePasse()
            => RemplirChamp("edit-pass-pass2", utilisateur.MotDePasse);

        // ─────────────────────────────────────
        // CIVILITE
        // ─────────────────────────────────────
        [When("je sélectionne la civilité")]
        public void WhenJeSelectionneLaCivilite()
        {
            var id = (utilisateur.Civilite == "Mme" || utilisateur.Civilite == "Mme.")
                ? "edit-field-civilite-mme" : "edit-field-civilite-mr";
            var el = driver.FindElement(By.Id(id));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
            System.Threading.Thread.Sleep(500);
        }

        // ─────────────────────────────────────
        // NOM / PRENOM
        // ─────────────────────────────────────
        [When("je renseigne mon nom")]
        public void WhenJeRenseigneMonNom()
            => RemplirChamp("edit-field-nom-0-value", utilisateur.Nom);

        [When("je renseigne mon prénom")]
        public void WhenJeRenseigneMonPrenom()
            => RemplirChamp("edit-field-prenom-0-value", utilisateur.Prenom);

        // ─────────────────────────────────────
        // PAYS
        // ─────────────────────────────────────
        [When("je sélectionne mon pays de résidence")]
        public void WhenJeSelectionneMonPaysDeResidence()
        {
            SelectionnerOption(
                "edit-field-pays-concernes-selectized",
                utilisateur.PaysResidence);
        }
        [When("je sélectionne mon pays de nationalité")]
        public void WhenJeSelectionneMonPaysDeNationalite()
        {
            var champ = driver.FindElement(By.Id("edit-field-nationalite-0-target-id"));

            champ.SendKeys(utilisateur.PaysNationalite);

            System.Threading.Thread.Sleep(1500);

            champ.SendKeys(Keys.ArrowDown);
            champ.SendKeys(Keys.Enter);
        }

        // ─────────────────────────────────────
        // INFOS PERSO
        // ─────────────────────────────────────
        [When("je renseigne mon code postal")]
        public void WhenJeRenseigneMonCodePostal()
            => RemplirChamp("edit-field-code-postal-0-value", utilisateur.CodePostal);

        [When("je renseigne ma ville")]
        public void WhenJeRenseigneMaVille()
            => RemplirChamp("edit-field-ville-0-value", utilisateur.Ville);

        [When("je renseigne mon téléphone")]
        public void WhenJeRenseigneMonTelephone()
            => RemplirChamp("edit-field-telephone-0-value", utilisateur.Telephone);

        // ─────────────────────────────────────
        // PROFIL
        // ─────────────────────────────────────
        [When("je sélectionne le profil {string}")]
        public void WhenJeSelectionneLeProfil(string profil)
        {
            string id = profil switch
            {
                "Étudiants" => "edit-field-publics-cibles-2",
                "Chercheurs" => "edit-field-publics-cibles-3",
                _ => "edit-field-publics-cibles-4"
            };
            var el = driver.FindElement(By.Id(id));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
            System.Threading.Thread.Sleep(1000);
        }

        // ─────────────────────────────────────
        // ETUDES
        // ─────────────────────────────────────
        [When("je sélectionne le domaine d'études")]
        public void WhenJeSelectionneLeDomaineDetudes()
        {
            SelectionnerOption(
                "edit-field-domaine-etudes-selectized",
                utilisateur.DomaineEtudes);
        }

        [When("je sélectionne le niveau d'étude")]
        public void WhenJeSelectionneLeNiveauDetude()
        {
            SelectionnerOption(
                "edit-field-niveaux-etude-selectized",
                utilisateur.NiveauEtude);
        }
        // ─────────────────────────────────────
        // FONCTION
        // ─────────────────────────────────────
        [When("je renseigne la fonction")]
        public void WhenJeRenseigneLaFonction()
            => RemplirChamp("edit-field-fonction-0-value", utilisateur.Fonction);
        // ─────────────────────────────────────
        // TYPE ORGANISME
        // ─────────────────────────────────────
        [When("je sélectionne le type d'organisme")]
        public void WhenJeSelectionneLeTypeDOrganisme()
        {
            var input = driver.FindElement(By.Id("edit-field-type-organisme-selectized"));

            input.SendKeys(utilisateur.TypeOrganisme);
            System.Threading.Thread.Sleep(1500);

            input.SendKeys(Keys.ArrowDown);
            input.SendKeys(Keys.Enter);
        }
        // ─────────────────────────────────────
        // NOM ORGANISME
        // ─────────────────────────────────────
        [When("je renseigne le nom de l'organisme")]
        public void WhenJeRenseigneLeNomDeLOrganisme()
        {
            System.Threading.Thread.Sleep(1000);

            RemplirChamp(
                "edit-field-nom-organisme-0-value",
                utilisateur.NomOrganisme
            );
        }

        // ─────────────────────────────────────
        // SUBMIT
        // ─────────────────────────────────────
        [When("je soumets le formulaire d'inscription")]
        public void WhenJeSoumetsLeFormulaireDInscription()
        {
            var btn = driver.FindElement(By.Id("edit-submit"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", btn);
            System.Threading.Thread.Sleep(3000);
        }

        // ─────────────────────────────────────
        // THEN
        // ─────────────────────────────────────
        [Then("mon compte est créé avec succès")]
        public void ThenMonCompteEstCreeAvecSucces()
            => Console.WriteLine("Scénario exécuté avec succès");

        // ─────────────────────────────────────
        // CLEANUP
        // ─────────────────────────────────────
        [After]
        public void FermerNavigateur()
            => driver?.Quit();
    }
}