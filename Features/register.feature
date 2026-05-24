Feature: Création de compte sur le portail CampusFrance

  # US1 — Étudiant
  # En tant qu'Étudiant,
  # je souhaite créer un compte Campus France
  # afin d'accéder aux services et informations liés à mes études.

  Scenario: Création de compte en tant qu'Étudiant

    Given je suis sur la page d'inscription de CampusFrance
    And je charge les données du profil "etudiant"

    When je renseigne mon adresse e-mail
    And je renseigne mon mot de passe
    And je confirme mon mot de passe

    And je sélectionne la civilité
    And je renseigne mon nom
    And je renseigne mon prénom

    And je sélectionne mon pays de résidence
    And je sélectionne mon pays de nationalité
    And je renseigne mon code postal
    And je renseigne ma ville
    And je renseigne mon téléphone

    And je sélectionne le profil "Étudiants"

    And je sélectionne le domaine d'études
    And je sélectionne le niveau d'étude

    And je soumets le formulaire d'inscription

    Then mon compte est créé avec succès


  # US2 — Chercheur
  # En tant que Chercheur,
  # je souhaite créer un compte Campus France
  # afin d'accéder aux ressources et programmes de recherche.

  Scenario: Création de compte en tant que Chercheur

    Given je suis sur la page d'inscription de CampusFrance
    And je charge les données du profil "chercheur"

    When je renseigne mon adresse e-mail
    And je renseigne mon mot de passe
    And je confirme mon mot de passe

    And je sélectionne la civilité
    And je renseigne mon nom
    And je renseigne mon prénom

    And je sélectionne mon pays de résidence
    And je sélectionne mon pays de nationalité
    And je renseigne mon code postal
    And je renseigne ma ville
    And je renseigne mon téléphone

    And je sélectionne le profil "Chercheurs"

    And je sélectionne le domaine d'études
    And je sélectionne le niveau d'étude

    And je soumets le formulaire d'inscription

    Then mon compte est créé avec succès


  # US3 — Institutionnel
  # En tant qu'Institutionnel,
  # je souhaite créer un compte Campus France
  # afin de représenter mon organisme sur la plateforme.

  Scenario: Création de compte en tant qu'Institutionnel

    Given je suis sur la page d'inscription de CampusFrance
    And je charge les données du profil "institutionnel"

    When je renseigne mon adresse e-mail
    And je renseigne mon mot de passe
    And je confirme mon mot de passe

    And je sélectionne la civilité
    And je renseigne mon nom
    And je renseigne mon prénom

    And je sélectionne mon pays de résidence
    And je sélectionne mon pays de nationalité
    And je renseigne mon code postal
    And je renseigne ma ville
    And je renseigne mon téléphone

    And je sélectionne le profil "Institutionnel"

    And je renseigne la fonction
    And je sélectionne le type d'organisme
    And je renseigne le nom de l'organisme

    And je soumets le formulaire d'inscription

    Then mon compte est créé avec succès
