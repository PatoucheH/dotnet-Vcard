# 📇 VCard Manager – Application de gestion de contacts `.vcf`

Une application console en **C# (.NET)** qui permet de lire, afficher, ajouter, supprimer, exporter et trier des contacts au format vCard (`.vcf`).  
Elle utilise **Spectre.Console** pour enrichir l’interface en ligne de commande et **xUnit** pour les tests unitaires.

---

## 🧾 Fonctionnalités

- 📂 Lecture de fichiers `.vcf` (vCard)
- 👥 Affichage de tous les contacts
- ➕ Ajout d’un nouveau contact
- ❌ Suppression d’un contact
- 🔍 Affichage d’un contact spécifique
- 📤 Export d’un contact au format `.vcf`
- 🔠 Tri des contacts par nom
- 💄 Interface console stylisée avec Spectre.Console
- 🧪 Tests unitaires avec xUnit

---

## 🗂️ Structure du projet

```plaintext
VCardManager/
├── Vcard/
│   ├── Contact.cs            -> Modèle de données pour les contacts
│   ├── Program.cs            -> Point d'entrée de l'application
│   ├── Utils.cs              -> Méthodes utilitaires (affichage, chargement, etc.)
│   ├── Vcard.cs              -> Logique métier de gestion des contacts
│   ├── Vcard.csproj          -> Fichier projet .NET
│   ├── contacts.vcf          -> Exemple de fichier vCard
│   └── zazi zzz.vcf          -> Autre fichier de test
├── Tests/
│   ├── VcardTests.cs         -> Fichier de tests xUnit
│   ├── contactsDeleteTest.vcf -> Fichier contacts de tests
│   ├──contactsTest.vcf       -> Fichier contacts de tests
│   └── Tests.csproj          -> Projet de test
````

## 🧰 Technologies utilisées

- [.NET 8+](https://dotnet.microsoft.com/)
- [Spectre.Console](https://spectreconsole.net/)
- [xUnit](https://xunit.net/)

---

## ▶️ Lancer l’application

Dans le dossier racine du projet, exécute :

```bash
dotnet run
````
## 💻 Exemple d'affichage console du menu
````plaintext
Welcome to the Vcard application!
This application allows you to manage your contacts.
Please choose an option from the menu below:

1. Display all contacts
2. Add a new contact
3. Display a specific contact
4. Delete a contact
5. Export contact
6. Sort contacts by name
7. Exit
````