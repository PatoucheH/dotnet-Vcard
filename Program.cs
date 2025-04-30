
namespace Vcard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // initialize the file name and a variable for the user's choice
            string fileName = "contacts.vcf";
            string choice;

            Console.WriteLine("Welcome to the Vcard application!");
            Console.WriteLine("This application allows you to manage your contacts.");
            Console.WriteLine("Please choose an option from the menu below:"); 
            // display Menu and ask the user to make a choice of what he wants to do 
            Utils.DisplayMenu();
            choice = Utils.AskUserForAnNoEmptyString();
            // create a List to enter the contacts
            List<Contact> contactsList = Utils.LoadAllFile(fileName);

            // loop on the user's choice each number made a diffenrent action 
            while (choice != "6")
            {
                switch (choice)
                {
                    case "1":
                        Vcard.DiplayAllContacts(contactsList);
                        break;
                    case "2":
                        Vcard.AddAContact(contactsList, fileName);
                        break;
                    case "3":
                        Vcard.DisplayContact(contactsList);
                        break;
                    case "4":
                        Vcard.DeleteContact(contactsList, fileName);
                        break;
                    case "5":
                        Vcard.ExportContact(contactsList, fileName);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Utils.DisplayMenu();
                choice = Utils.AskUserForAnNoEmptyString();
            }
        }
    }
}
