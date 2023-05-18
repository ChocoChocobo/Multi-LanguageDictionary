using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Multi_LanguageDictionary
{
    /// <summary>
    /// Represents requests which are used in menu
    /// </summary>
    internal class Requests
    {
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
        /// <summary>
        /// Checks if the index is valid for a given dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to check.</param>
        /// <param name="index">The index to check.</param>
        /// <param name="trueM">The message to display if the index is valid.</param>
        /// <param name="falseM">The message to display if the index is invalid.</param>
        /// <returns>True if the index is valid, false otherwise.</returns>
        private bool CheckWordTranslationArray(Dictionary dic, int index, string trueM, string falseM)
        {
            if (index == dic.Length)
            {
                Console.WriteLine(trueM);
                return true;
            }
            else
            {
                Console.WriteLine(falseM);
                return false;
            }
            //Console.WriteLine("Press Enter...");
        }
        /// <summary>
        /// Prompts the user to enter word and translation pairs.
        /// </summary>
        /// <returns>A dictionary of word and translation pairs</returns>
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
        /// Displays the contents of a dictionary.
        /// </summary>
        /// <param name="dic">Ref to the dictionary to display</param>
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
        /// <summary>
        /// Creates a new dictionary.
        /// </summary>
        /// <param name="dic">Ref to the dictionary.</param>
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
        /// <summary>
        /// Adds a word and its translation to an existing dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to add the word and translations to.</param>
        public void AddWordTranslation(ref Dictionary dic)
        {
            var len = dic.Length;
            bool isExistingDictionary = CheckWordTranslationArray(dic, len, "Intializing successful!", "Cannot access a dictionary!");
            if(isExistingDictionary)
            {
                if(dic.Length > 0)
                {
                    Console.Write("Enter the desired dictionary id:\n");
                    int arrayIndex = Convert.ToInt32(Console.ReadLine()); 
                    
                    WordTranslation wordTranslation = dic.GetWordTranslation(dic, arrayIndex);
                    if(wordTranslation != null)
                    {
                        Dictionary<string, List<string>> newEntries = GetWordTranslations();
                        foreach(var entry in newEntries )
                        {
                            if(wordTranslation.Entries.ContainsKey(entry.Key))
                            {
                                wordTranslation.Entries[entry.Key].AddRange(entry.Value);
                            }
                            else
                            {
                                wordTranslation.Entries[entry.Key] = entry.Value;
                            }
                        }
                    }                    
                }
            }
        }
        /// <summary>
        /// Replaces the translation of a word in an existing dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to modify.</param>
        public void ReplaceWordTranslation(ref Dictionary dic)
        {
            var len = dic.Length;
            bool isExistingDictionary = CheckWordTranslationArray(dic, len, "Intializing successful!", "Cannot access a dictionary!");

            if(isExistingDictionary)
            {
                if(dic.Length > 0 )
                {
                    Console.Write("Enter the desired dictionary id:\n");
                    int arrayIndex = Convert.ToInt32(Console.ReadLine());

                    WordTranslation wordTranslation = dic.GetWordTranslation(dic, arrayIndex);
                    if(wordTranslation != null)
                    {
                        string wordToReplace = CheckInput("Enter the word which translations you want to replace:");

                        if(wordTranslation.Entries.ContainsKey(wordToReplace))
                        {
                            string newTranslation = CheckInput("Enter the new translation:");
                            List<string> translations = wordTranslation.Entries[wordToReplace];
                            translations.Clear();           //Можно добавить, чтобы не через запятую
                            translations.Add(newTranslation);
                            Console.WriteLine("Word translation replaced!");
                        }
                        else
                        {
                            Console.WriteLine("Word not found!");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Deletes a word and its translation from an existing dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to modify.</param>
        public void DeleteWordTranslation(ref Dictionary dic)
        {
            var len = dic.Length;
            bool isExistingDictionary = CheckWordTranslationArray(dic, len, "Intializing successful!", "Cannot access a dictionary!");

            if(isExistingDictionary)
            {
                if(dic.Length > 0)
                {
                    Console.Write("Enter the desired dictionary id:\n");
                    int arrayIndex = Convert.ToInt32(Console.ReadLine());

                    WordTranslation wordTranslation = dic.GetWordTranslation(dic, arrayIndex);
                    if(wordTranslation != null)
                    {
                        string wordToDelete = CheckInput("Enter the word to delete:");

                        if(wordTranslation.Entries.ContainsKey(wordToDelete))
                        {
                            wordTranslation.Entries.Remove(wordToDelete);
                            Console.WriteLine("Word deleted from the dictionary!");
                        }
                        else
                        {
                            Console.WriteLine("Word not found!");
                        }
                    }
                }
            }
            else { Console.WriteLine($"No dictionary with this id!"); }
        }
        /// <summary>
        /// Searches for the translation of a word in an existing dictionary.
        /// </summary>
        /// <param name="dic">The dictionary to search.</param>
        public void SearchWordTranslation(ref Dictionary dic)
        {
            var len = dic.Length;
            bool isExistingDictionary = CheckWordTranslationArray(dic, len, "Intializing successful!", "Cannot access a dictionary!");

            if (isExistingDictionary)
            {
                if (dic.Length > 0)
                {
                    Console.Write("Enter the desired dictionary id:\n");
                    int arrayIndex = Convert.ToInt32(Console.ReadLine());

                    WordTranslation wordTranslation = dic.GetWordTranslation(dic, arrayIndex);
                    if(wordTranslation != null)
                    {
                        var search = CheckInput("Enter the word which you wish to search the translation for:");
                        if (wordTranslation.Entries.ContainsKey(search))
                        {
                            List<string> translations = wordTranslation.Entries[search];
                            Console.WriteLine("\nTranslations:");
                            foreach(string translation in translations)
                            {
                                Console.WriteLine(translation);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no such a word in dictionaries!");
                        }
                        Console.ReadKey();
                    }
                }
            }           
        }
        /// <summary>
        /// Exports the data of an existing dictionary to a file.
        /// </summary>
        /// <param name="dic">The dictionary to export.</param>
        public void ExportDictionaryToFile(Dictionary dic)
        {
            var len = dic.Length;
            bool isExistingDictionary = CheckWordTranslationArray(dic, len, "Intializing successful!", "Cannot access a dictionary!");

            if(isExistingDictionary)
            {
                if(dic.Length > 0)
                {
                    Console.Write("Enter the desired dictionary id:\n");
                    int arrayIndex = Convert.ToInt32(Console.ReadLine());

                    WordTranslation wordTranslation = dic.GetWordTranslation(dic, arrayIndex);
                    if(wordTranslation != null)
                    {
                        //string folderPath = "C:\\";
                        string fileName = CheckInput("Enter the file name (without the exstension) to save the dictionary data: ");
                        fileName += ".txt";
                        //string filePath = Path.Combine(folderPath, fileName);
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(fileName))
                            {
                                sw.WriteLine($"Id of the dictionary: {wordTranslation.Id}");
                                sw.WriteLine($"Dictionary type: {wordTranslation.Type}");

                                foreach (KeyValuePair<string, List<string>> entry in wordTranslation.Entries)
                                {
                                    sw.WriteLine($"\nWord: {entry.Key}");
                                    foreach (string translation in entry.Value)
                                    {
                                        sw.WriteLine($"Translation: {translation}");
                                    }
                                }
                            }
                            Console.WriteLine("Dictionary data exported successfully!");
                            string fullPath = Path.GetFullPath(fileName);
                            Console.WriteLine($"Saved file: {fullPath}");
                            Console.WriteLine("Press Enter...");
                            Console.ReadKey();
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Invalid file name.");
                        }
                        catch (PathTooLongException)
                        {
                            Console.WriteLine("File path exceeds the system`s maximum length.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while exporting the dictionary data: {ex.Message}");
                        }
                    }
                }
            }            
        }
    }
}
