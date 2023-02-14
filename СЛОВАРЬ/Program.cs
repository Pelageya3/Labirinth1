using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Authentication;

namespace Labirinth
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users\burla\OneDrive\Рабочий стол\Lab.txt";
            Dictionary<string, string> keyy = new Dictionary<string, string>();

            Key(path, keyy);
            VivestySlovar(keyy);

        }

        static void Key(string path, Dictionary<string, string> keyy)
        {

            string text = File.ReadAllText(path);
            string text2 = text.Replace("\r\n", " ");
            string[] words = text2.Split(' ', '\n');

            for (int p = 0; p < words.Length; p++)
            {
                if (words[p] == "")
                {
                    Array.Resize(ref words, p);
                }
            }

            string[] word = new string[words.Length / 2];
            string[] key = new string[words.Length / 2];

            int i = 0;
            int o = 1;

            int u = 0;
            int y = 0;

            while (i < words.Length && u < words.Length / 2)
            {
                word[u] = words[i];

                while (o < words.Length && y < words.Length / 2)
                {

                    key[y] = words[o];

                    keyy.Add(word[u], key[y]);
                    y++;
                    o += 2;

                    break;

                }

                u++;
                i += 2;

            }
        }

        static void VivestySlovar(Dictionary<string, string> keyy)
        {
            foreach (KeyValuePair<string, string> keyandvalue in keyy)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    keyandvalue.Key, keyandvalue.Value);
            }
        }
    }
}
