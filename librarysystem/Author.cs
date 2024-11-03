using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarysystem
{
    public class Author
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Author(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
