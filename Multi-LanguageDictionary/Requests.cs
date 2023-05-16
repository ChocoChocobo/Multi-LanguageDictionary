using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Multi_LanguageDictionary
{
    internal class Requests
    {
        private string CheckInput(string inputDesc)
        {
            string input;
            do
            {
                Console.Write(inputDesc);
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine("\tEmpty line!");
                }
            } while (input == "");
            return input;
        }
        private void CheckWordTranslationArray(Dictionary dic, int index, string trueM, string falseM)
        {
            if (index != dic.Length)
            {
                Console.WriteLine(trueM);
            }
            else
            {
                Console.WriteLine(falseM);
            }
            Console.WriteLine("Press Enter...");
        }
        private Dictionary<string, List<string>> GetWordTranslations()
        {
            Dictionary<string, List<string>> entries = new Dictionary<string, List<string>>();

            Console.WriteLine("Enter word and its translations (separated by commas): ");
            Console.WriteLine("Press Enter without entering anything to finish.");

            string input = Console.ReadLine();
            while (!string.IsNullOrEmpty(input))
            {
                string[] inputParts = input.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputParts.Length == 2)
                {
                    string word = inputParts[0].Trim();
                    string[] translations = inputParts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    if (!entries.ContainsKey(word))
                    {
                        entries[word] = new List<string>();
                    }

                    entries[word].AddRange(translations.Select(x => x.Trim()));
                }
                else
                {
                    Console.WriteLine("Invalid input format. Use 'word: translation1, translation2, ... ' format.");
                }

                input = Console.ReadLine();
            }
            return entries;
        }



        public void ShowDictionary(ref Dictionary dic)
        {
            //Console.Clear();
            foreach(var wordTranslation in dic)
            {
                Console.Write(wordTranslation + "\n");
            }
            Console.WriteLine("Press Enter...");
            Console.ReadKey();
            //Console.Clear();
        }

        public void CreateDictionary(ref Dictionary dic)
        {
            WordTranslation wordTranslation = new WordTranslation();
            wordTranslation.Id = dic.Length;
            wordTranslation.Type = CheckInput("Enter the desired dictionary type:");
            wordTranslation.Entries = new Dictionary<string, List<string>>();
            var len = dic.Length;
            dic = dic + wordTranslation;
            CheckWordTranslationArray(dic, len, "New dictionary created!", "Trying to add an existing dictionary. Error!");
            Console.ReadKey();
        }

        public void AddWordTranslation(ref Dictionary dic)
        {   
            WordTranslation wordTranslation = WordTranslationSearch();
            SearchDictionaryByType(ref dic);
            wordTranslation.Entries = GetWordTranslations();
            Console.WriteLine("Done!");
        }

        //Переработать вернуть wordTranslation
        private Dictionary SearchDictionaryByType(ref Dictionary dic)
        {
            var search = CheckInput("Enter the desired dictionary type to find: ");
            if (dic[search] ==  null)
            {
                Console.WriteLine("No existing dictionary type!");
                return dic;
            }
            else
            {
                return dic;
            }
        }
    }
}
