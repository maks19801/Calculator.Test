using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PersonParser
{
    public class CsvPersonParser:IPersonParser
    {
        private string _fileName;

        public CsvPersonParser(string fileName)
        {
            this._fileName = fileName;
        }

        public IEnumerable<Person> GetPeople()
        { 
            using var fs = new FileStream(_fileName, FileMode.OpenOrCreate);
            using var sr = new StreamReader(fs);

            var header = sr.ReadLine();

            var line = string.Empty;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var personInfo = line.Split(',');
                yield return new Person
                {
                    Id = int.Parse(personInfo[0]),
                    Name = personInfo[1],
                    Age = int.Parse(personInfo[2])
                };
            }

        }
    }
}
