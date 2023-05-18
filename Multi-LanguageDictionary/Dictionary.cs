using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_LanguageDictionary
{
    /// <summary>
    /// Represents a word and its translations.
    /// </summary>
    class WordTranslation
    {
        /// <summary>
        /// Gets or sets the Id of the word translation.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the type of the word translation.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the entries of the word translation.
        /// The keys are the words, and the values are lists of translation variants.
        /// </summary>
        public Dictionary<string, List<string>> Entries { get; set; }
        /// <summary>
        /// Returns a string representation of the word translation.
        /// </summary>
        /// <returns>A string representation of the word translation.</returns>
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
    /// <summary>
    /// Represents a dictionary of word translations.
    /// </summary>
    class Dictionary : IEnumerable
    {
        private WordTranslation[] wordTranslations;
        /// <summary>
        /// Initializes a new instance of the <see cref="Dictionary"/> class.
        /// </summary>
        public Dictionary()
        {
            // test
            wordTranslations = new WordTranslation[2];
            wordTranslations[0] = new WordTranslation
            {
                Id = 0,
                Type = "eng-rus",
                Entries = new Dictionary<string, List<string>>()
            };
            wordTranslations[0].Entries.Add("hello", new List<string> { "привет", "здравствуй" });
            wordTranslations[0].Entries.Add("light", new List<string> { "свет", "лёгкий" });

            // test 2
            wordTranslations[1] = new WordTranslation
            {
                Id = 1,
                Type = "tr-eng",
                Entries = new Dictionary<string, List<string>>()
            };
            wordTranslations[1].Entries.Add("merhaba", new List<string> { "привет", "здравствуй" });
            wordTranslations[1].Entries.Add("gule gule", new List<string> { "прощай", "пока пока" });
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Dictionary"/> class with the specified size.
        /// </summary>
        /// <param name="size">The size of the dictionary.</param>
        public Dictionary(int size) => wordTranslations = new WordTranslation[size];
        /// <summary>
        /// Gets the length of the dictionary.
        /// </summary>
        public int Length { get { return wordTranslations.Length; } }

        //----------------------------------------------------------------        
        /// <summary>
        /// Checks user input, prompting for a value until a non-empty line is entered.
        /// </summary>
        /// <param name="inputDesc">The description of the input.</param>
        /// <returns>The non-empty input value.</returns>
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
        /// <summary>
        /// Retrieves the WordTranslation at the specified index from the given dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to retrieve from.</param>
        /// <param name="index">The index of the WordTranslation.</param>
        /// <returns>The WordTranslation at the specified index, or null if index is out of range.</returns>
        public WordTranslation GetWordTranslation(Dictionary dic, int index)
        {
            if (index >= 0 && index < dic.Length)
            {
                return dic.wordTranslations[index];
            }

            return null;
        }
        /// <summary>
        /// Adds a new WordTranslation to the dictionary.
        /// </summary>
        /// <returns>The newly added WordTranslation.</returns>
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
        /// <summary>
        /// Retrieves the word translations entered by the user.
        /// </summary>
        /// <returns>A dictionary containing word translations.</returns>
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
        /// <summary>
        /// Finds the index of the WordTranslation with the specified type.
        /// </summary>
        /// <param name="type">The type of WordTranslation to search for.</param>
        /// <returns>The index of the WordTranslation if found, or -1 if not found.</returns>
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
        /// <summary>
        /// Gets or sets the WordTranslation at the specified index.
        /// </summary>
        /// <param name="index">The index of the WordTranslation.</param>
        /// <returns>The WordTranslation at the specified index, or null if the index is out of range.</returns>
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
        /// <summary>
        /// Gets or sets the WordTranslation with the specified type.
        /// </summary>
        /// <param name="type">The type of the WordTranslation.</param>
        /// <returns>The WordTranslation with the specified type, or null if the type is not found.</returns>
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
        /// <summary>
        /// Changes the Id property of each WordTranslation in the dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to change.</param>
        /// <returns>The modified dictionary.</returns>
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
        /// <summary>
        /// Adds a WordTranslation to the dictionary.
        /// </summary>
        /// <param name="dic">The dictionary.</param>
        /// <param name="wordTranslation">The WordTranslation to add.</param>
        /// <returns>The modified dictionary with the added WordTranslation, or the original dictionary if the WordTranslation's type already exists.</returns>
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
        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return wordTranslations.GetEnumerator();
        }

    }
}