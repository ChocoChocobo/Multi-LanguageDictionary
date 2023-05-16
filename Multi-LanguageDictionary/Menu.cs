﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_LanguageDictionary
{
    internal class Menu
    {
        Requests req = new Requests();
        Dictionary dic = new Dictionary();
        WordTranslation wordTranslation = new WordTranslation();
        public delegate void Actions(ref Dictionary dic);

        //This code uses a nested loop to handle working with an existing dictionary. The outer loop displays the main menu, and the inner loop displays the submeny for the selected dictionary. The user can select options to add, replace, delete, search, or export data for the selected dictionary. The user can also select an option to return to the main menu, which sets the 'workingWithDictionary' flag to false and exits the inner loop.      
        public void StartMenu()
        {
            Console.Clear();
            //Actions
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create a dictionary.");
                Console.WriteLine("2. Work with existing dictionary.");
                Console.WriteLine("0. Exit.");
                int option = Int32.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        //code to create a dictionary
                        req.CreateDictionary(ref dic);
                        break;
                    case 2:
                        bool workingWithDictionary = true;
                        while (workingWithDictionary)
                        {
                            Console.Clear();
                            Console.WriteLine("Here is a list of existing dictionaries:\n");
                            req.ShowDictionary(ref dic);

                            Console.WriteLine("Select an option:");
                            Console.WriteLine("1. Add a word and its translation to an existing dictionary.");
                            Console.WriteLine("2. Replace a word or its translation in the dictionary.");
                            Console.WriteLine("3. Delete a word or translation from the dictionary.");
                            Console.WriteLine("4. Search for the translation of a word.");
                            Console.WriteLine("5. Export dictionary data to a file.");
                            Console.WriteLine("0. Exit");
                            int subOption = Int32.Parse(Console.ReadLine());

                            switch (subOption)
                            {
                                case 1:
                                    //code for adding a word and its translation
                                    req.AddWordTranslation(ref dic, ref wordTranslation);
                                    break;
                                case 2:
                                    //code for replacing a word or its translation
                                    break;
                                case 3:
                                    //code for deleting a word or translation
                                    break;
                                case 4:
                                    //code for searching for the translation of a word
                                    break;
                                case 5:
                                    //code for exporting dictionary data to a file
                                    break;
                                case 0:
                                    workingWithDictionary = false;
                                    break;
                                default: Console.WriteLine("Invalid choice!"); break;
                            }
                            Console.WriteLine();
                        }
                        break;
                    case 0:
                        running = false;                        
                        break;
                    default:
                        Console.WriteLine("Invalid choice!"); break;
                }
                Console.WriteLine();
            }
        }
    }
}