using System.Collections.Generic;

namespace PersonParser
{
    public interface IPersonParser
    {
        public IEnumerable<Person> GetPeople();
    }
}
