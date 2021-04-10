using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace PersonParser.Tests
{
    public class PersonParserTests
    {
        [Fact]
        public void Get_From_Empty_File()
        {
            var fileName = "testEmpty.csv";
            EnsureFileDeleted(fileName);
            var parser = new CsvPersonParser(fileName);
            Assert.Empty(parser.GetPeople());
        }

       
        [Fact]
        public void Get_One_Line()
        {
            var fileName = "testOneLine.csv";
            EnsureFileDeleted(fileName);
            var person = new Person
            {
                Id = 315,
                Name = "Alex",
                Age = 18
            };
            var line = "header\n"+
                $"{person.Id},{person.Name},{person.Age}";
            File.WriteAllText(fileName, line, Encoding.UTF8);
            var parser = new CsvPersonParser(fileName);
            var parsedPerson = parser.GetPeople().FirstOrDefault();
            Assert.NotNull(parsedPerson);
            Assert.True(person.Id == parsedPerson.Id &&
                        person.Name == parsedPerson.Name &&
                        person.Age == parsedPerson.Age);
        }
        private void EnsureFileDeleted(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }


    }
}
