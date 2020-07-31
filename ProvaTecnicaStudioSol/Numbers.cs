using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProvaTecnicaStudioSol
{
    class Numbers
    {
        public static void Menu()
        {
            Console.OutputEncoding = Encoding.Unicode;

            const int MAXMENUCHOICE = 2;
            const int MAXGUESS = 300;
            int userInputMenu, userInputGuess;
            do
            {
                Console.Clear();

                Console.WriteLine("\nBem Vindo ao Qual é o número !\n");
                Console.WriteLine("┏━━━━━━━━━━━━━MENU━━━━━━━━━━━━━┓");
                Console.WriteLine("┃                              ┃");
                Console.WriteLine("┃ 1 - Para acessar o Jogo      ┃");
                Console.WriteLine("┃ 2 - Para encerrar a execução ┃");
                Console.WriteLine("┃                              ┃");
                Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
                Console.Write("\nInsira uma das opções acima: ");

                userInputMenu = Int32.Parse(Console.ReadLine());
                if (userInputMenu < 0 || userInputMenu > MAXMENUCHOICE)
                {
                    Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                    Console.WriteLine("ERRO DETECTADO\n");
                    Console.WriteLine("Você inseriu " + userInputMenu + " por favor insira um número de 0 a " + MAXMENUCHOICE );
                    Console.Write("\nPressione qualquer tecla para continuar: ");
                    Console.ReadKey();
                }

            } while (userInputMenu < 0 || userInputMenu > MAXMENUCHOICE);

            switch (userInputMenu)
            {
                case 1:
                    do
                    {
                        Console.Clear();
                        Console.Write("Insira um número de 0 a " + MAXGUESS + ": ");
                        userInputGuess = Int32.Parse(Console.ReadLine());
                        if (userInputGuess < 0 || userInputGuess > MAXGUESS)
                        {
                            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                            Console.WriteLine("Você inseriu: " + userInputGuess + " por favor insira um número de 0 a " + MAXGUESS);
                            Console.WriteLine("\nPressione qualquer tecla para continuar: ");
                            Console.ReadKey();
                        }
                        else FetchNumber(userInputGuess).Wait();

                    } while (userInputGuess < 0 || userInputGuess > MAXGUESS) ;

                    break;
                case 2:
                    Environment.Exit(0);

                    break;
                default:
                    break;
            }
        }
        public async static Task FetchNumber(int userGuess)
        {
            string baseURL = "https://us-central1-ss-devops.cloudfunctions.net/rand?min=1&max=300";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using(HttpResponseMessage response = await client.GetAsync(baseURL))
                    {
                        using(HttpContent responseContent = response.Content)
                        {
                            string responseJson = await responseContent.ReadAsStringAsync();

                            dynamic deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);

                            PrintGuessAnswer(responseJson, ((int)response.StatusCode), userGuess);
                        }
                    }               
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            Console.Write("\nPressione qualquer tecla para continuar: ");          
            Console.ReadLine();
            Console.Clear();
            Menu();
        }

        public static void PrintGuessAnswer(string responseJson, int responseStatus, int userGuess)
        {
            Console.Clear();

            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            int toBeGuessed;

            if (responseStatus == 200)
            {
                dynamic deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);

                toBeGuessed = deserializedJson.value;

                if (userGuess >= toBeGuessed)
                {
                    if (userGuess > toBeGuessed)
                        Console.WriteLine(" É maior");
                    else
                        Console.WriteLine(" Acertou! ＼(＾▽＾)／");
                }
                else
                    Console.WriteLine(" É menor");

                GenerateGuessAnswer(userGuess.ToString(), false);
            }
            else
            {
                toBeGuessed = responseStatus;

                Console.WriteLine(" ERRO");

                GenerateGuessAnswer(toBeGuessed.ToString(), true);
            }
            Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        }
        public static void GenerateGuessAnswer(string toBePrinted, bool isError){
            string[,] displayArray = new string[3, 9];

            for (int i = 0; i < displayArray.GetLength(0); i++)
            {
                Console.WriteLine("");
                int indexColumn = 0;

                for (int j = 0; j < displayArray.GetLength(1); j++)
                {
                    if (indexColumn == 3)
                        indexColumn = 0;

                    if (j <= 2 && toBePrinted.Length >= 1)
                        displayArray[i, j] = GenerateLedDisplayNumber(toBePrinted[0])[i, indexColumn];
                    else if (j <= 5 && toBePrinted.Length >= 2)
                        displayArray[i, j] = GenerateLedDisplayNumber(toBePrinted[1])[i, indexColumn];
                    else if (j <= 8 && toBePrinted.Length >= 3)
                        displayArray[i, j] = GenerateLedDisplayNumber(toBePrinted[2])[i, indexColumn];

                    Console.Write(displayArray[i, j]);
                    indexColumn++;
                }
            }
        }
        public static string[,] GenerateLedDisplayNumber(char toBeDisplayed)
        {
            string[,] digit = new string[3, 3];

            switch (toBeDisplayed)
            {
                case '0':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = " ";
                    digit[1, 2] = "|";
                    digit[2, 0] = "|";
                    digit[2, 1] = "_";
                    digit[2, 2] = "|";

                    break;
                case '1':
                    digit[0, 0] = " ";
                    digit[0, 1] = " ";
                    digit[0, 2] = " ";
                    digit[1, 0] = " ";
                    digit[1, 1] = " ";
                    digit[1, 2] = "|";
                    digit[2, 0] = " ";
                    digit[2, 1] = " ";
                    digit[2, 2] = "|";

                    break;
                case '2':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = " ";
                    digit[1, 1] = "_";
                    digit[1, 2] = "|";
                    digit[2, 0] = "|";
                    digit[2, 1] = "_";
                    digit[2, 2] = " ";

                    break;
                case '3':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = " ";
                    digit[1, 1] = "_";
                    digit[1, 2] = "|";
                    digit[2, 0] = " ";
                    digit[2, 1] = "_";
                    digit[2, 2] = "|";

                    break;
                case '4':
                    digit[0, 0] = " ";
                    digit[0, 1] = " ";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = "_";
                    digit[1, 2] = "|";
                    digit[2, 0] = " ";
                    digit[2, 1] = " ";
                    digit[2, 2] = "|";

                    break;
                case '5':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = "_";
                    digit[1, 2] = " ";
                    digit[2, 0] = " ";
                    digit[2, 1] = "_";
                    digit[2, 2] = "|";

                    break;
                case '6':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = "_";
                    digit[1, 2] = " ";
                    digit[2, 0] = "|";
                    digit[2, 1] = "_";
                    digit[2, 2] = "|";

                    break;
                case '7':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = " ";
                    digit[1, 2] = "|";
                    digit[2, 0] = " ";
                    digit[2, 1] = " ";
                    digit[2, 2] = "|";

                    break;
                case '8':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = "_";
                    digit[1, 2] = "|";
                    digit[2, 0] = "|";
                    digit[2, 1] = "_";
                    digit[2, 2] = "|";

                    break;
                case '9':
                    digit[0, 0] = " ";
                    digit[0, 1] = "_";
                    digit[0, 2] = " ";
                    digit[1, 0] = "|";
                    digit[1, 1] = "_";
                    digit[1, 2] = "|";
                    digit[2, 0] = " ";
                    digit[2, 1] = " ";
                    digit[2, 2] = "|";

                    break;
                default:
                    break;
            }
            return digit;
        }
    }
}
