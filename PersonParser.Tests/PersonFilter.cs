using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonParser.Tests
{
    public class PersonFilter
    {
        private readonly IPersonParser _personParser;

        public PersonFilter(IPersonParser personParser)
        {
            _personParser = personParser;
        }

        public IEnumerable<Person> GetAdults()
        {
            return _personParser
                    .GetPeople()
                    .Where(p=>p.Age>=18);
        }
    }
}
