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

            Dictionary<char, string> symbolDictionary = new Dictionary<char, string>();
            Dictionary<string, char> entitiesDictionary = new Dictionary<string, char>();

            GetKeySymbols(path, symbolDictionary, entitiesDictionary);

            int descriptionSize = symbolDictionary.Count + 1;
            char[][] labirinth = GetLabirinthFromFile(path, descriptionSize);

            int x = descriptionSize;
            int y = 0;

            PrintArr(labirinth, symbolDictionary, descriptionSize);
            FindPlayerPosition(ref x, ref y, labirinth, symbolDictionary);

            do
            {
                Win(labirinth, descriptionSize, symbolDictionary);
                Wasd(ref x, ref y, labirinth, descriptionSize, symbolDictionary, entitiesDictionary);

            } while (true);

        }
        static void GetKeySymbols(string path, Dictionary<char, string> symbolDictionary, Dictionary<string, char> entitiesDictionary)
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
                if (!symbolDictionary.ContainsValue(symbols[y + 1]))
                    symbolDictionary.Add(symbols[y + 1][0], symbols[y]);
                if (!entitiesDictionary.ContainsValue(symbols[y][0]))
                    entitiesDictionary.Add(symbols[y], symbols[y + 1][0]);
            }
        }

        static char[][] GetLabirinthFromFile(string path, int descriptionSize)
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

        static void PrintArr(char[][] labirinth, Dictionary<char, string> symbolDictionary, int descriptionSize)
        {
            int a;
            int b;

            for (a = descriptionSize; a < labirinth.Length; a++)
            {

                for (b = 0; b < labirinth[a].Length; b++)
                {
                    //if (labirinth[a][0] != wall || labirinth[a][^1] != wall || labirinth[descriptionSize][b] != wall || labirinth[^1][b] != wall)
                    switch (symbolDictionary[labirinth[a][b]])
                    {
                        case constants.wall:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(labirinth[a][b]);
                            Console.ResetColor();
                            break;

                        case constants.character:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(labirinth[a][b]);
                            break;

                        case constants.win:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(labirinth[a][b]);
                            break;

                        case constants.way:
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(labirinth[a][b]);
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        static void FindPlayerPosition(ref int x, ref int y, char[][] labirinth, Dictionary<char, string> symbolDictionary)
        {
            for (; x < labirinth.Length; x++)
            {
                bool stop_loop = false;

                for (y = 0; y < labirinth[x].Length; y++)
                {
                    if (symbolDictionary[labirinth[x][y]] == constants.character)
                    {
                        stop_loop = true;
                        break;
                    }
                }
                if (stop_loop == true)
                    break;
            }
        }

        static int DollarCount(char[][] labirinth, int descriptionSize, Dictionary<char, string> symbolDictionary)
        {
            int dollar = 0;

            for (int x = descriptionSize; x < labirinth.Length; x++)
            {
                for (int y = 0; y < labirinth[x].Length; y++)
                {
                    if (symbolDictionary[labirinth[x][y]] == constants.win)
                    {
                        dollar += 1;
                    }
                }
            }
            return dollar;
        }

        static void Win(char[][] labirinth, int descriptionSize, Dictionary<char, string> symbolDictionary)
        {
            int dollarcount = DollarCount(labirinth, descriptionSize, symbolDictionary);
            //Console.WriteLine(dollarcount);
            if (dollarcount == 0)
                Console.WriteLine("Ты выиграл!");
        }

        void Swap(ref int firstNumber, ref int secondNumber)
        {
            int temporary = firstNumber;
            firstNumber = secondNumber;
            secondNumber = temporary;
        }

        static void Wasd(ref int x, ref int y, char[][] labirinth, int descriptionSize, Dictionary<char, string> symbolDictionary, Dictionary<string, char> entitiesDictionary)
        {
            char wasd = Console.ReadKey().KeyChar;

            switch (wasd)
            {
                case 'w':
                case 'W':
                case 'ц':
                case 'Ц':

                    if (symbolDictionary[labirinth[x - 1][y]] == constants.win)
                    {
                        x -= 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x + 1][y] = entitiesDictionary[constants.way];
                    }

                    else if (symbolDictionary[labirinth[x - 1][y]] == constants.way)
                    {
                        x -= 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x + 1][y] = entitiesDictionary[constants.way];
                    }

                    else { }

                    break;

                case 'a':
                case 'A':
                case 'ф':
                case 'Ф':

                    if (symbolDictionary[labirinth[x][y - 1]] == constants.win)
                    {
                        y -= 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x][y + 1] = entitiesDictionary[constants.way];
                    }

                    else if (symbolDictionary[labirinth[x][y - 1]] == constants.way)
                    {
                        y -= 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x][y + 1] = entitiesDictionary[constants.way];
                    }

                    else { }

                    break;

                case 's':
                case 'S':
                case 'ы':
                case 'Ы':

                    if (symbolDictionary[labirinth[x + 1][y]] == constants.win)
                    {
                        x -= 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x - 1][y] = entitiesDictionary[constants.way];
                    }

                    else if (symbolDictionary[labirinth[x + 1][y]] == constants.way)
                    {
                        x += 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x - 1][y] = entitiesDictionary[constants.way];
                    }

                    else { }

                    break;

                case 'd':
                case 'D':
                case 'в':
                case 'В':

                    if (symbolDictionary[labirinth[x][y + 1]] == constants.win)
                    {
                        y += 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x][y - 1] = entitiesDictionary[constants.way];
                    }

                    else if (symbolDictionary[labirinth[x][y + 1]] == constants.way)
                    {
                        y += 1;
                        labirinth[x][y] = entitiesDictionary[constants.character];
                        labirinth[x][y - 1] = entitiesDictionary[constants.way];
                    }

                    else { }
               
                    break; 

                default:

                    Console.WriteLine("");
                    Console.Write("Введите корректный символ");
                    Console.ReadKey();

                    break; 
            }
            Restart(labirinth, descriptionSize, symbolDictionary);
        }

        static void Restart(char[][] labirinth, int descriptionSize, Dictionary<char, string> symbolDictionary)
        {
            Console.Clear();
            PrintArr(labirinth, symbolDictionary, descriptionSize);
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

