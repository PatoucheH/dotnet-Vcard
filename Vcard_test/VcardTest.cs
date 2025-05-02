using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcard;

namespace Vcard_test
{
    public class VcardTest
    {

        [Fact]
        public static void Test_AddAContact()
        {
            // Arrange
            var contactsList = new List<Contact>();
            string fileName = "../../../contactsTest.vcf";

            if (File.Exists(fileName))
                File.Delete(fileName);
            var input = new StringReader("Pierre Jacques\n123456789012\nPierre@jacques.com\n");
            Console.SetIn(input);

            // Act

            Vcard.Vcard.AddAContact(contactsList, fileName);

            // Assert
            Assert.Single(contactsList);
            Assert.Equal("Pierre Jacques", contactsList[0].FullName);
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public static void Test_DisplayContact()
        {
            // Arrange
            var contactsList = new List<Contact>
            {
                new Contact
                {
                    FullName = "Pierre Jacques",
                    PhoneNumber = "123456789012",
                    Email = "Pierre@Jacques.com"
                }
            };
            var input = new StringReader("Pierre Jacques");
            Console.SetIn(input);

            var output = new StringWriter();
            var originalConsole = AnsiConsole.Console;
            var testConsole = AnsiConsole.Create(new AnsiConsoleSettings
            {
                Out = new AnsiConsoleOutput(output)
            });
            AnsiConsole.Console = testConsole;

            try
            {

            // Act
            Vcard.Vcard.DisplayContact(contactsList);
            }
            finally
            {
                AnsiConsole.Console = originalConsole;
            }
            // Assrt
            string consoleOutput = output.ToString();
            Assert.Contains("Pierre Jacques", consoleOutput);
            Assert.Contains("123456789012", consoleOutput);
            Assert.Contains("Pierre@Jacques.com", consoleOutput);

        }

        [Fact]
        public void Test_DeleteContact()
        {
            // Arrange
            var contactsList = new List<Contact>
            {
                new Contact { FullName = "Pierre Jacques", PhoneNumber = "1234567890", Email = "pierre@jacques.com" },
                new Contact { FullName = "Marie Dupont", PhoneNumber = "0987654321", Email = "marie@dupont.com" }
            };

            string fileName = "../../../contactsDeleteTest.vcf";

            File.WriteAllText(fileName,
                "BEGIN:VCARD\nFN:Pierre Jacques\nTEL:1234567890\nEMAIL:pierre@jacques.com\nEND:VCARD\n\n" +
                "BEGIN:VCARD\nFN:Marie Dupont\nTEL:0987654321\nEMAIL:marie@dupont.com\nEND:VCARD\n\n");

            var input = new StringReader("Pierre Jacques\n");
            Console.SetIn(input);

            // Act
            Vcard.Vcard.DeleteContact(contactsList, fileName);

            // Assert
            Assert.Single(contactsList); 
            Assert.Equal("Marie Dupont", contactsList[0].FullName); 

            string updatedContent = File.ReadAllText(fileName);
            Assert.DoesNotContain("Pierre Jacques", updatedContent);
            Assert.Contains("Marie Dupont", updatedContent);
        }

    }
}
