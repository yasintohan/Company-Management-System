using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Management
{

    class DepItem
    {
        private string name;
        private string description;

        public DepItem(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }

    }
}
