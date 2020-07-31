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
            const int MAXMENUCHOICE = 2;
            const int MAXGUESS = 300;
            bool isCloseMenu,isAnotherTry;
            int userInputMenu, userInputGuess;

            isCloseMenu = false;
            isAnotherTry = false;

            Console.WriteLine("\nBem Vindo ao Qual é o número !");
            Console.WriteLine("\n============MENU============");
            Console.WriteLine("1 - Para acessar o Jogo");
            Console.WriteLine("2 - Para encerrar a execução");
            Console.WriteLine("============================");
            Console.Write("\nInsira uma das opções acima: ");
            userInputMenu = Int32.Parse(Console.ReadLine());

            if (userInputMenu < 0 || userInputMenu > MAXMENUCHOICE)
            {
                Console.WriteLine("Você inseriu: " + userInputMenu + " por favor insira um número de 0 a " + MAXMENUCHOICE);
            }

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
                            Console.WriteLine("=======================================================");
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
                            string json = await responseContent.ReadAsStringAsync();

                            ConvertJsonToNumber(json, ((int)response.StatusCode), userGuess);
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
            Menu();
        }

        public static void ConvertJsonToNumber(string json, int responseStatus, int userGuess)
        {
            int number;
            string numbers;
            bool isAError;

            if (responseStatus == 200)
            {
                dynamic deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                number = deserializedJson.value;
                numbers = userGuess.ToString();

                isAError = false;

                PrintUserResponse(numbers, isAError);
            }
            else
            {
                number = responseStatus;
                numbers = number.ToString();

                isAError = true;
                Console.WriteLine("ERROR");
                PrintUserResponse(numbers, isAError);
            }
        }

        public static void PrintUserResponse(string toBePrinted, bool isAError){
            string[] ledDisplay = new string[3];
            
            for (int i = 0;i < toBePrinted.Length; i++)
            {
                ledDisplay = GenerateLedDisplayNumber(toBePrinted[i]);
                Console.WriteLine(ledDisplay[0]);
                Console.WriteLine(ledDisplay[1]);
                Console.WriteLine(ledDisplay[2]);
            }

        }
        public static string[] GenerateLedDisplayNumber(char userGuess)
        {
            string[] ledToDisplay = new string[3];

            switch (userGuess)
            {
                case'0':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = "| | ";
                    ledToDisplay[2] = "|_| ";
                    break;
                case '1':
                    ledToDisplay[0] = "    ";
                    ledToDisplay[1] = "  | ";
                    ledToDisplay[2] = "  | ";
                    break;
                case '2':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = " _| ";
                    ledToDisplay[2] = "|_  ";
                    break;
                case '3':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = " _| ";
                    ledToDisplay[2] = " _| ";
                    break;
                case '4':
                    ledToDisplay[0] = "    ";
                    ledToDisplay[1] = "|_| ";
                    ledToDisplay[2] = "  | ";
                    break;
                case '5':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = "|_  ";
                    ledToDisplay[2] = " _| ";
                    break;
                case '6':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = "|_  ";
                    ledToDisplay[2] = "|_| ";
                    break;
                case '7':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = "  | ";
                    ledToDisplay[2] = "  | ";
                    break;
                case '8':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = "|_| ";
                    ledToDisplay[2] = "|_| ";
                    break;
                case '9':
                    ledToDisplay[0] = " _  ";
                    ledToDisplay[1] = "|_| ";
                    ledToDisplay[2] = "  | ";
                    break;
                default:
                    break;
            }

            return ledToDisplay;
        }
    }
}
