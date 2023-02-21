using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Labirinth
{
    public struct constants
    {
    public const string way = "way";
    public const string wall = "wall";
    public const string character = "character";
    public const string win = "win";
    }

    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users\burla\OneDrive\Рабочий стол\Lab.txt";

            Dictionary<string, char> symbolDictionary = new Dictionary<string, char>();

            GetKeySymbols(path, symbolDictionary);

            char wall = symbolDictionary["wall"];
            char way = symbolDictionary["way"];
            char character = symbolDictionary["character"];
            char win = symbolDictionary["win"];

            int descriptionSize = symbolDictionary.Count + 1;
            char[][] labirinth = GetLabirinthFromFile(path, descriptionSize, wall);

            int x = descriptionSize;
            int y = 0;

            PrintArr(labirinth, way, wall, character, win, descriptionSize);
            FindPlayerPosition(ref x, ref y, labirinth, character);

            do
            {
                Win(labirinth, win, descriptionSize);
                Wasd(ref x, ref y, labirinth, way, wall, character, win, descriptionSize);

            } while (true);

        }
        static void GetKeySymbols(string path, Dictionary<string, char> symbolDictionary)
        {

            string[] symbols = File.ReadAllText(path).Replace("\r\n", " ").Split(' ', '\n');

            for (int p = 0; p < symbols.Length; p++)
            {
                if (symbols[p] == "")
                {
                    Array.Resize(ref symbols, p);
                }
            }

            for (int y = 0; y < symbols.Length; y += 2)
            {
                if (!symbolDictionary.ContainsValue(symbols[y + 1][0]))
                    symbolDictionary.Add(symbols[y], symbols[y + 1][0]);
            }

            //foreach (KeyValuePair<string, char> item in symbolDictionary)
            //Console.WriteLine(item.Key + " " + item.Value);
        }

        static char[][] GetLabirinthFromFile(string path, int descriptionSize, char wall)
        {
            string[] textlabirinth = File.ReadAllLines(path);
            char[][] labirinth = new char[textlabirinth.Length][];

            for (int a = descriptionSize; a < textlabirinth.Length; a++)
            {
                char[] eachline = textlabirinth[a].ToCharArray();
                labirinth[a] = new char[eachline.Length];

                for (int b = 0; b < eachline.Length; b++)
                {
                    labirinth[a][b] = eachline[b];

                }
            }

            return labirinth;
        }

        static void PrintArr(char[][] labirinth, char way, char wall, char character, char win, int descriptionSize)
        {
            int a;
            int b;

            for (a = descriptionSize; a < labirinth.Length; a++)
            {

                for (b = 0; b < labirinth[a].Length; b++)
                {
                    //if (labirinth[a][0] != wall || labirinth[a][^1] != wall || labirinth[descriptionSize][b] != wall || labirinth[^1][b] != wall)

                    if (labirinth[a][b] == wall)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(labirinth[a][b]);
                        Console.ResetColor();
                    }

                    else if (labirinth[a][b] == character)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(labirinth[a][b]);
                    }

                    else if (labirinth[a][b] == win)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(labirinth[a][b]);
                    }

                    else if (labirinth[a][b] == way)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(labirinth[a][b]);
                        Console.ResetColor();
                    }

                    else { }

                }

                Console.WriteLine();
            }

        }

        static void FindPlayerPosition(ref int x, ref int y, char[][] labirinth, char character)
        {
            for (; x < labirinth.Length; x++)
            {
                bool stop_loop = false;

                for (y = 0; y < labirinth[x].Length; y++)
                {
                    if (labirinth[x][y] == character)
                    {
                        stop_loop = true;
                        break;
                    }
                }
                if (stop_loop == true)
                    break;
            }
        }

        static int DollarCount(char[][] labirinth, int descriptionSize, char win)
        {
            int dollar = 0;

            for (int x = descriptionSize; x < labirinth.Length; x++)
            {
                for (int y = 0; y < labirinth[x].Length; y++)
                {
                    if (labirinth[x][y] == win)
                    {
                        dollar += 1;
                    }
                }
            }
            return dollar;
        }

        static void Win(char[][] labirinth, char win, int descriptionSize)
        {
            int dollarcount = DollarCount(labirinth, descriptionSize, win);
            //Console.WriteLine(dollarcount);
            if (dollarcount == 0)
                Console.WriteLine("Ты выиграл!");
        }

        static void Wasd(ref int x, ref int y, char[][] labirinth, char way, char wall, char character, char win, int descriptionSize)
        {
            char wasd = Console.ReadKey().KeyChar;

            switch (wasd)
            {
                case 'w':
                case 'W':
                case 'ц':
                case 'Ц':

                    if (labirinth[x - 1][y] == win)
                    {
                        x -= 1;
                        labirinth[x][y] = character;
                        labirinth[x + 1][y] = way;
                    }

                    else if (labirinth[x - 1][y] == way)
                    {
                        x -= 1;
                        labirinth[x][y] = character;
                        labirinth[x + 1][y] = way;
                    }

                    else { }

                    break;

                case 'a':
                case 'A':
                case 'ф':
                case 'Ф':

                    if (labirinth[x][y - 1] == win)
                    {
                        y -= 1;
                        labirinth[x][y] = character;
                        labirinth[x][y + 1] = way;
                    }

                    else if (labirinth[x][y - 1] == way)
                    {
                        y -= 1;
                        labirinth[x][y] = character;
                        labirinth[x][y + 1] = way;
                    }

                    else { }

                    break;

                case 's':
                case 'S':
                case 'ы':
                case 'Ы':

                    if (labirinth[x + 1][y] == win)
                    {
                        x += 1;
                        labirinth[x][y] = character;
                        labirinth[x - 1][y] = way;
                    }

                    else if (labirinth[x + 1][y] == way)
                    {
                        x += 1;
                        labirinth[x][y] = character;
                        labirinth[x - 1][y] = way;
                    }

                    else { }

                    break;

                case 'd':
                case 'D':
                case 'в':
                case 'В':

                    if (labirinth[x][y + 1] == win)
                    {
                        y += 1;
                        labirinth[x][y] = character;
                        labirinth[x][y - 1] = way;
                    }

                    else if (labirinth[x][y + 1] == way)
                    {
                        y += 1;
                        labirinth[x][y] = character;
                        labirinth[x][y - 1] = way;
                    }

                    else { }

                    break;

                default:

                    Console.WriteLine("");
                    Console.Write("Введите корректный символ");
                    Console.ReadKey();

                    break;
            }
            Restart(labirinth, way, wall, character, win, descriptionSize);
        }

        static void Restart(char[][] labirinth, char way, char wall, char character, char win, int descriptionSize)
        {
            Console.Clear();
            PrintArr(labirinth, way, wall, character, win, descriptionSize);
        }

    }
}



/*static char DefineWallSymbol(int[][] labirinth, int a, int b, int wall)
  {
      if (((a - 1 >= 0 && labirinth[a - 1][b] == wall) || (a + 1 < labirinth.Length && labirinth[a + 1][b] == wall))
      && ((b - 1 >= 0 && labirinth[a][b - 1] == wall) || (b + 1 < labirinth[a].Length && labirinth[a][b + 1] == wall)))
      {
          return '+';
      }

      else if ((a - 1 >= 0 && labirinth[a - 1][b] == wall) || (a + 1 < labirinth.Length && labirinth[a + 1][b] == wall))
      {
          return '|';
      }

      else if ((b - 1 >= 0 && labirinth[a][b - 1] == wall) || (b + 1 < labirinth[a].Length && labirinth[a][b + 1] == wall))
      {
          return '-';
      }

      else
      {
          return '*';
      }
  }

  static char DefineCharacterSymbol()
  {
      return '@';
  }

  static char DefineWinSymbol()
  {
      return '$';
  }

  static char DefineWaySymbol()
  {
      return ' ';
} */

