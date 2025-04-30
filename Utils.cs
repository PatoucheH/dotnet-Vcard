using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Vcard
{
    /// <summary>
    /// Create a class utils with all the utils methods
    /// </summary>
    internal class Utils
    {
        /// <summary>
        /// Method to display the menu to the user
        /// </summary>
        public static void DisplayMenu()
        {
            AnsiConsole.MarkupLine("[italic]1. [blue]Display[/] all contacts[/]");
            AnsiConsole.MarkupLine("[italic]2. [green]Add[/] a new contact[/]");
            AnsiConsole.MarkupLine("[italic]3. Search for a contact by his name[/]");
            AnsiConsole.MarkupLine("[italic]4. [red]Delete[/] a contact[/]");
            AnsiConsole.MarkupLine("[italic]5. Export a contact in a separate file[/]");
            AnsiConsole.MarkupLine("[italic]6. [red3]Exit[/][/]");
        }

        /// <summary>
        /// Method to ask to the user to enter anything and check if he doesn't enter an invalid input
        /// </summary>
        /// <returns>return what the user enter after the check </returns>
        public static string AskUserForAnNoEmptyString()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    AnsiConsole.MarkupLine("[red]Input cannot be empty. Please try again.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"You entered: [green]{input}[/]");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        /// <summary>
        /// Methods which loads all the file .vcf and put it in a List
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns>a List with all the contacts</returns>
        public static List<Contact> LoadAllFile(string filePath)
        {
            var contacts = new List<Contact>();

            if (!File.Exists(filePath))
            {
                AnsiConsole.MarkupLine("[red]File don't exist ![/]");
                return contacts;
            }

            string[] lines = File.ReadAllLines(filePath);
            Contact actualContact = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("BEGIN:VCARD"))
                {
                    actualContact = new Contact();
                }
                else if (line.StartsWith("FN:") && actualContact != null)
                {
                    actualContact.FullName = line.Substring(3).Trim();
                }
                else if (line.StartsWith("TEL:") && actualContact != null)
                {
                    actualContact.PhoneNumber = line.Substring(4);
                }
                else if (line.StartsWith("EMAIL:") && actualContact != null)
                {
                    actualContact.Email = line.Substring(6);
                }
                else if (line.StartsWith("END:VCARD") && actualContact != null)
                {
                    contacts.Add(actualContact);
                    actualContact = null;
                }
            }
            return contacts;
        }

/// <summary>
/// Methods to check if it's a valid email
/// </summary>
/// <param name="email">email entry</param>
/// <returns>a bool if the email is valid or not</returns>
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// Methods to check if it's a valid phone number
        /// </summary>
        /// <param name="phoneNumber">phone number entry</param>
        /// <returns>a bool if the phone number is valid or not</returns>
        public static bool IsValidInternationalPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(?:\+([1-9][0-9]{0,2})\s?\d{6,12}|0[1-9](?:\s?\d{2}){4})$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

    }
}
