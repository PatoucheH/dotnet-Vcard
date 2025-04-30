using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Vcard
{
    internal class Vcard
    {
        /// <summary>
        /// Method to display all the contacts
        /// </summary>
        /// <param name="contactsList">The list with all the contacts</param>
        public static void DiplayAllContacts(List<Contact> contactsList)
        {
            var table = new Table();
            table.Centered();
            table.ShowRowSeparators();
            table.Border = TableBorder.MinimalHeavyHead;
            table.AddColumn("[blue]Full name[/]");
            table.AddColumn("[blue]Phone number[/]");
            table.AddColumn("[blue]Email[/]");
            table.Columns[0].Centered();
            table.Columns[1].Centered();
            table.Columns[2].Centered();
            
            foreach(var contact in contactsList)
            {
                table.AddRow(contact.FullName, contact.PhoneNumber, contact.Email);
            }
            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Add a contact to the list and the file .vcf
        /// </summary>
        /// <param name="contactsList">List with all the contacts</param>
        /// <param name="fileName">Path of the file</param>
        public static void AddAContact(List<Contact> contactsList, string fileName)
        {
            Contact newContact = new Contact();
            string fullName, phoneNumber, email;

            AnsiConsole.Write(new Rule("Full name").RuleStyle("green"));
            AnsiConsole.Markup("Enter the [blue]full name[/] of the contact : ");
            fullName = Utils.AskUserForAnNoEmptyString();

            AnsiConsole.Write(new Rule("Phone number").RuleStyle("green"));
            AnsiConsole.Markup("Enter the [blue]phone number[/] of the contact : ");
            do
            {
                phoneNumber = Utils.AskUserForAnNoEmptyString();
                if(!Utils.IsValidInternationalPhoneNumber(phoneNumber))
                    AnsiConsole.MarkupLine("[red]Please enter a valid phone number : [/]");
            } while (!Utils.IsValidInternationalPhoneNumber(phoneNumber));

            AnsiConsole.Write(new Rule("Email").RuleStyle("green"));
            AnsiConsole.Markup("Enter the [blue]email[/] of the contact : ");
            do
            {
                email = Utils.AskUserForAnNoEmptyString();
                if (!Utils.IsValidEmail(email))
                    AnsiConsole.Markup("[red]Please enter a valid email : [/]");
            } while (!Utils.IsValidEmail(email));

            newContact.FullName = fullName;
            newContact.PhoneNumber = phoneNumber;
            newContact.Email = email;
            contactsList.Add(newContact);

            using (StreamWriter writer = new StreamWriter(fileName, append: true))
            {
                writer.WriteLine("BEGIN:VCARD");
                writer.WriteLine($"FN:{newContact.FullName}");
                writer.WriteLine($"TEL:{newContact.PhoneNumber}");
                writer.WriteLine($"EMAIL:{newContact.Email}");
                writer.WriteLine("END:VCARD");
            }
        }

        /// <summary>
        /// display just one contact choose by his name
        /// </summary>
        /// <param name="contactsList">List of all the contacts</param>
        public static void DisplayContact(List<Contact> contactsList)
        {
            AnsiConsole.MarkupLine("Enter the [blue]name of the contact[/] you want to search for : ");
            string name = Utils.AskUserForAnNoEmptyString();
            var contactToDisplay = contactsList.FirstOrDefault(contact => contact.FullName.ToLower() == name.ToLower());
            if (contactToDisplay != null)
            {
                var panelText = $"[blue]Contact[/]\n" +
                $"Full name : [blue]{contactToDisplay.FullName}[/]\n" +
                $"Phone number : [blue]{contactToDisplay.PhoneNumber}[/]\n" +
                $"Email : [blue]{contactToDisplay.Email}[/]";
                var panel = new Panel(panelText);
                panel.Border = BoxBorder.Rounded;
                AnsiConsole.Write(panel);
            }
            else
            {
                AnsiConsole.MarkupLine("[red3]Contact not found.[/]");
            }
        }

        /// <summary>
        /// Delete a contact of the list and the .vcf file
        /// </summary>
        /// <param name="contactsList">List of all the contacts</param>
        /// <param name="fileName">Path of the file .vcf</param>
        public static void DeleteContact(List<Contact> contactsList, string fileName)
        {
            AnsiConsole.MarkupLine("Enter the name of the [blue]contact[/] you want to delete : ");
            string nameToDelete = Utils.AskUserForAnNoEmptyString();
            var contact = contactsList.FirstOrDefault(contact => contact.FullName.ToLower() == nameToDelete.ToLower());
            if (contact != null)
            {
                contactsList.Remove(contact);
                string content = "";
                    foreach (var contactToKeep in contactsList)
                    {
                        content += $"BEGIN:VCARD\nFN:{contactToKeep.FullName}\nTEL:{contactToKeep.PhoneNumber}\nEMAIL:{contactToKeep.Email}\nEND:VCARD\n\n";
                    }
                File.WriteAllText(fileName, content);

                AnsiConsole.MarkupLine($"[green]{nameToDelete} successfully deleted.[/]");
            }               
            else            
            {               
                AnsiConsole.MarkupLine($"[red3]{nameToDelete} doesn't exist.[/]");
            }
        }

        /// <summary>
        /// Export a contact to another .vcf file with his name
        /// </summary>
        /// <param name="contactsList">List of all the contacts</param>
        /// <param name="fileName">Path of the origin file</param>
        public static void ExportContact(List<Contact> contactsList, string fileName)
        {
            AnsiConsole.MarkupLine("Enter the [blue]name of the contact[/] you want to export : ");
            string nameToExport = Utils.AskUserForAnNoEmptyString();
            var contactToExport = contactsList.FirstOrDefault(contact => contact.FullName.ToLower() == nameToExport.ToLower());
            if(contactToExport != null)
            {
                contactsList.Remove(contactToExport);
                string content = "";
                foreach (var contactToKeep in contactsList)
                {
                    content += $"BEGIN:VCARD\nFN:{contactToKeep.FullName}\nTEL:{contactToKeep.PhoneNumber}" +
                        $"\nEMAIL:{contactToKeep.Email}\nEND:VCARD";
                }
                File.WriteAllText(fileName, content);

                string contentToExport = $"BEGIN:VCARD\nFN:{contactToExport.FullName}\nTEl:{contactToExport.PhoneNumber}" +
                    $"\nEMAIL:{contactToExport.Email}\nEND:VCARD\n";
                File.WriteAllText($"{nameToExport}.vcf", contentToExport);

                AnsiConsole.MarkupLine($"[green]{nameToExport} successfully exported.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red3]{nameToExport} doesn't exist.[/]");
            }
        }
    }
}
