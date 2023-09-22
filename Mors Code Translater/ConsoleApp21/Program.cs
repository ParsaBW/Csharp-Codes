using System;
using System.Collections.Generic;
using System.Threading;

namespace MorseCodeTranslator
{
    class Program
    {
        private static readonly Dictionary<char, string> MorseDictionary = new Dictionary<char, string>
        {
            { 'a', ".-" }, { 'b', "-..." }, { 'c', "-.-." }, { 'd', "-.." }, { 'e', "." },
            { 'f', "..-." }, { 'g', "--." }, { 'h', "...." }, { 'i', ".." }, { 'j', ".---" },
            { 'k', "-.-" }, { 'l', ".-.." }, { 'm', "--" }, { 'n', "-." }, { 'o', "---" },
            { 'p', ".--." }, { 'q', "--.-" }, { 'r', ".-." }, { 's', "..." }, { 't', "-" },
            { 'u', "..-" }, { 'v', "...-" }, { 'w', ".--" }, { 'x', "-..-" }, { 'y', "-.--" },
            { 'z', "--.." }, { '1', ".----" }, { '2', "..---" }, { '3', "...--" }, { '4', "....-" },
            { '5', "....." }, { '6', "-...." }, { '7', "--..." }, { '8', "---.." }, { '9', "----." },
            { '0', "-----" }, { ' ', "/" } // Space is represented as "/"
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Text to Morse Code");
                Console.WriteLine("2. Morse Code to Text");
                Console.WriteLine("3. Quit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TextToMorse();
                        break;
                    case "2":
                        MorseToText();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void TextToMorse()
        {
            Console.Write("Enter text to translate to Morse code: ");
            string text = Console.ReadLine().ToLower();

            foreach (char c in text)
            {
                if (MorseDictionary.TryGetValue(c, out string morse))
                {
                    Console.Write(morse + " ");
                    PlayMorseSound(morse);
                }
                else
                {
                    Console.Write($"({c}) "); // Display unknown characters in parentheses
                }
            }

            Console.WriteLine();
        }

        static void MorseToText()
        {
            Console.Write("Enter Morse code to translate to text (use '.' for dots, '-' for dashes, and '/' for spaces): ");
            string morseCode = Console.ReadLine();

            string[] morseWords = morseCode.Split(new[] { " / " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in morseWords)
            {
                string[] morseCharacters = word.Split(' ');

                foreach (string morseCharacter in morseCharacters)
                {
                    if (MorseDictionary.ContainsValue(morseCharacter))
                    {
                        char character = GetCharacterByMorse(morseCharacter);
                        Console.Write(character);
                        PlayMorseSound(morseCharacter);
                    }
                    else
                    {
                        Console.Write("?");
                    }
                }

                Console.Write(" "); // Space between words
            }

            Console.WriteLine();
        }

        static char GetCharacterByMorse(string morse)
        {
            foreach (var kvp in MorseDictionary)
            {
                if (kvp.Value == morse)
                {
                    return kvp.Key;
                }
            }
            return '?'; // Return '?' for unknown morse code
        }

        static void PlayMorseSound(string morse)
        {
            foreach (char symbol in morse)
            {
                if (symbol == '.')
                {
                    Console.Beep(1000, 200); // Beep for a dot
                }
                else if (symbol == '-')
                {
                    Console.Beep(1000, 400); // Beep for a dash
                }
                else if (symbol == '/')
                {
                    Thread.Sleep(400); // Pause for a space
                }
            }
            Thread.Sleep(200); // Pause between symbols
        }
    }
}
