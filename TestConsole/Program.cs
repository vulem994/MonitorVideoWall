using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("String1 value: " + Properties.Resources.String1);
            Console.WriteLine("Press enter to change String1 value");
            Console.ReadLine();

            Properties.Resources.Culture = new System.Globalization.CultureInfo("Serbian")
            {

            };
            Console.WriteLine("String1 new value: " + Properties.Resources.String1);
        }
    }
}
