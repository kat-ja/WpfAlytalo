using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlytalo
{
    class Sauna
    {
        public Boolean Switched { get; set; }
        public int SaunaTempe { get; set; }

        public Boolean SaunaOn()
        {
            Switched = true;
            return Switched;
        }

        public Boolean SaunaOff()
        {
            Switched = false;
            return Switched;
        }
    }
}
