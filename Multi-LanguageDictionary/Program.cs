using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_LanguageDictionary
{
    /// <summary>
    /// The main entry point for the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method that gets executed when the program starts.
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.StartMenu();
        }
    }
}
