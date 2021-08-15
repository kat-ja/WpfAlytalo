using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAlytalo
{
    class Thermostat
    {
        public int Temperature { get; set; }

        public void SetTemperature(int tempe)
        {
            Temperature = tempe;
        }
    }
}
