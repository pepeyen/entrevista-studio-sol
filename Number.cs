using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumberGame
{
    class Number
    {
        public async static Task GetNumber(int userGuess)
        {
            string baseURL = "https://us-central1-ss-devops.cloudfunctions.net/rand?min=1&max=300";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync(baseURL))
                    {
                        using (HttpContent responseContent = response.Content)
                        {
                            string responseJson = await responseContent.ReadAsStringAsync();

                            dynamic deserializedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);

                            GuessLedDisplayScreen(responseJson, ((int)response.StatusCode), userGuess);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
        public static void GuessLedDisplayScreen(string responseJson, int responseStatus, int userGuess)
        {
            int toBeGuessed;

            Console.Clear();

            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━");

            switch (responseStatus)
            {
                case 200:
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

                    GenerateLedDisplay(userGuess.ToString());

                    break;

                case 502:
                    Console.WriteLine(" ERRO");

                    GenerateLedDisplay(responseStatus.ToString());

                    break;
                default:
                    break;
            }

            Console.WriteLine("\n\n━━━━━━━━━━━━━━━━━━━━━━");
        }
        public static void GenerateLedDisplay(string toBeDisplayed)
        {
            string[,] ledDisplayArray = new string[3, 9];

            for (int i = 0; i < ledDisplayArray.GetLength(0); i++)
            {
                Console.WriteLine("");
                int indexColumn = 0;

                for (int j = 0; j < ledDisplayArray.GetLength(1); j++)
                {
                    if (indexColumn == 3)
                        indexColumn = 0;

                    if (j <= 2 && toBeDisplayed.Length >= 1)
                        ledDisplayArray[i, j] = GenerateLedNumber(toBeDisplayed[0])[i, indexColumn];
                    else if (j <= 5 && toBeDisplayed.Length >= 2)
                        ledDisplayArray[i, j] = GenerateLedNumber(toBeDisplayed[1])[i, indexColumn];
                    else if (j <= 8 && toBeDisplayed.Length >= 3)
                        ledDisplayArray[i, j] = GenerateLedNumber(toBeDisplayed[2])[i, indexColumn];

                    Console.Write(ledDisplayArray[i, j]);
                    indexColumn++;
                }
            }
        }
        public static string[,] GenerateLedNumber(char toBeDisplayed)
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
