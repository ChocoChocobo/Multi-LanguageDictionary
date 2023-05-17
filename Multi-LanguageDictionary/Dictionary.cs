using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_LanguageDictionary
{
    //class that represents Words and Translations
    class WordTranslation
    {
        public int Id { get; set; }
        public string Type { get; set; }

        //Entries property is a dictionary where the keys are the words, and the values are lists of translation variants.
        public Dictionary<string, List<string>> Entries { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, List<string>> entry in Entries)
            {
                string word = entry.Key;
                List<string> translations = entry.Value;

                sb.AppendLine($"\n\tWord: \t\t\t{word}");
                foreach (string translation in translations)
                {
                    sb.AppendLine($"\tTranslations:\t\t{translation}");
                }
            }
            return $"Id: {Id}\nType: {Type}\nWord-translation:\n {sb.ToString()}";
        }
    }

    class Dictionary : IEnumerable
    {
        WordTranslation[] wordTranslations;
        public Dictionary()
        {
            //test
            wordTranslations = new WordTranslation[2];
            wordTranslations[0] = new WordTranslation
            {
                Id = 0,
                Type = "eng-rus",
                Entries = new Dictionary<string, List<string>>()
            };
            wordTranslations[0].Entries.Add("hello", new List<string> { "привет", "здравствуй" });
            wordTranslations[0].Entries.Add("light", new List<string> { "свет", "лёгкий" });

            //test 2
            wordTranslations[1] = new WordTranslation
            {
                Id = 1,
                Type = "tr-eng",
                Entries = new Dictionary<string, List<string>>()
            };
            wordTranslations[1].Entries.Add("merhaba", new List<string> { "привет", "здравствуй" });
            wordTranslations[1].Entries.Add("gule gule", new List<string> { "прощай", "пока пока" });
        }

        //creating a new empty array to add Words Translations in it
        public Dictionary(int size) => wordTranslations = new WordTranslation[size];
        public int Length { get { return wordTranslations.Length; } }

        //----------------------------------------------------------------
        //simple CheckInput for not letting a user enter incorrect values
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

        //------------------------------------------------------------------
        //adding words and their translations to a cell in Dictionary array

        //this thing checks for the cell
        public WordTranslation GetWordTranslation(Dictionary dic, int index)
        {
            if (index >= 0 && index < dic.Length)
            {
                return dic.wordTranslations[index];
            }

            return null;
        }

        public WordTranslation AddWordTranslation()
        {
            Console.Clear();
            WordTranslation wordTranslation = new WordTranslation();
            wordTranslation.Id = Length;
            wordTranslation.Type = CheckInput("Enter dictionary type: ");
            wordTranslation.Entries = GetWordTranslations();
            if(FindWordTranslation(wordTranslation.Type) >= 0)
            {
                Console.WriteLine("Cannot add an existing translation!");
            }
            else
            {
                Console.WriteLine("Success!");
                Array.Resize(ref wordTranslations, wordTranslations.Length + 1);
                wordTranslations[Length - 1] = wordTranslation;
            }
            Console.ReadKey();
            Console.Clear();
            return wordTranslation;
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
        private int FindWordTranslation(string type)
        {
            type = type.ToLower();
            for(int i = 0; i < wordTranslations.Length; i++)
            {
                if (wordTranslations[i].Type.ToLower() == type)
                {
                    return i;
                }
            }
            return -1;
        }

        //------------------------------------------------------------------
        //int index indeksator
        public WordTranslation this[int index]
        {
            get
            {
                if (index >= 0 && index < wordTranslations.Length)
                {
                    return wordTranslations[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                wordTranslations[index] = value;
            }
        }

        //------------------------------------------------------------------
        //string type indeksator
        public WordTranslation this[string type]
        {
            get
            {
                if(FindWordTranslation(type) >= 0)
                {
                    return this[FindWordTranslation(type)];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if(FindWordTranslation(type) >= 0)
                {
                    wordTranslations[FindWordTranslation(type)] = value;
                }
            }
        }

        //------------------------------------------------------------------
        //Dictionary operators
        private static Dictionary ChangeId(Dictionary dic)
        {
            if (dic != null)
            {
                for (int i = 0; i < dic.Length; i++)
                {
                    dic.wordTranslations[i].Id = i + 1;
                }
            }
            return dic;
        }

        public static Dictionary operator +(Dictionary dic, WordTranslation wordTranslation)
        {
            Dictionary dict = new Dictionary();
            var idxType = dict.FindWordTranslation(wordTranslation.Type);
            if(!(idxType >= 0))
            {
                Array.Resize(ref dict.wordTranslations, dict.wordTranslations.Length + 1);
                dict.wordTranslations[dict.wordTranslations.Length - 1] = wordTranslation;
                dict = ChangeId(dict);
                return dict;
            }
            else
            {
                return dict;
            }
        }

        //------------------------------------------------------------------
        //GetEnumerator()
        IEnumerator IEnumerable.GetEnumerator()
        {
            return wordTranslations.GetEnumerator();
        }

    }
}