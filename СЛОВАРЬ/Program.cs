using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Labirinth
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users\burla\OneDrive\Рабочий стол\Lab.txt";
            Dictionary<string, char> keyy = new Dictionary<string, char>();

            Key(path, keyy);
            PrintDictionary(keyy);

        }

        static void Key(string path, Dictionary<string, char> keyy)
        {

            string[] words = File.ReadAllText(path).Replace("\r\n", " ").Split(' ', '\n');
            
            for (int p = 0; p < words.Length; p++)
            {
                if (words[p] == "")
                    Array.Resize(ref words, p);
            }

            for (int i = 0; i < words.Length; i += 2)
                keyy.Add(words[i], words[i + 1][0]);

        }

        static void PrintDictionary(Dictionary<string, char> keyy)
        {
            foreach (KeyValuePair<string, char> keyandvalue in keyy)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    keyandvalue.Key, keyandvalue.Value);
            }
        }
    }
}
