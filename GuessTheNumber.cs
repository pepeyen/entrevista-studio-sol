using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumberGame
{
    class GuessTheNumber
    {
        const int MAXMENUCHOICE = 2;
        const int MAXGUESS = 300;
        public static void Menu()
        {
            Console.OutputEncoding = Encoding.Unicode;

            int userInputMenu = 0, userInputGuess = 0, userInputAnotherTry = 0;
            bool isAnotherTry = false;
            do
            {
                MenuHomeScreen();

                try
                {
                    userInputMenu = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    InputErrorScreen("fatalError", 0);

                    Environment.Exit(0);
                }


                if (userInputMenu <= 0 || userInputMenu > MAXMENUCHOICE)
                {
                    InputErrorScreen("menu", userInputMenu);
                }

            } while (userInputMenu <= 0 || userInputMenu > MAXMENUCHOICE);
            switch (userInputMenu)
            {
                case 1:
                    do
                    {
                        do
                        {
                            GameplayScreen();

                            try
                            {
                                userInputGuess = Int32.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                InputErrorScreen("fatalError", 0);

                                Environment.Exit(0);
                            }

                            if (userInputGuess <= 0 || userInputGuess > MAXGUESS)
                            {
                                InputErrorScreen("insideGame", userInputGuess);
                            }
                        } while (userInputGuess <= 0 || userInputGuess > MAXGUESS);

                        Number.GetNumber(userInputGuess).Wait();

                        TryAgainScreen();

                        try
                        {
                            userInputAnotherTry = Int32.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            InputErrorScreen("fatalError", 0);

                            Environment.Exit(0);
                        }
                        if (userInputAnotherTry == 1)
                            isAnotherTry = true;
                        else
                            if (userInputAnotherTry == 2)
                            isAnotherTry = false;

                    } while (isAnotherTry == true);

                    Menu();

                    break;
                case 2:
                    Environment.Exit(0);

                    break;
                default:
                    break;
            }
        }
        public static void MenuHomeScreen()
        {
            Console.Clear();

            Console.WriteLine("\n┏━━━━━━━━━━━━━MENU━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                              ┃");
            Console.WriteLine("┃       Qual é o Número        ┃");
            Console.WriteLine("┃                              ┃");
            Console.WriteLine("┃   1 - Para acessar o Jogo    ┃");
            Console.WriteLine("┃ 2 - Para encerrar o Programa ┃");
            Console.WriteLine("┃                              ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛\n");

            Console.Write("Insira uma das opções acima: ");
        }
        public static void InputErrorScreen(string errorLocation, int userInput)
        {
            Console.Clear();

            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                                                         ┃");
            Console.WriteLine("┃                      ERRO DETECTADO                     ┃");
            switch (errorLocation)
            {
                case "menu":
                    Console.WriteLine("┃                                                         ┃");
                    Console.WriteLine("┃   Você inseriu " + userInput + " por favor insira um número de 1 a " + MAXMENUCHOICE + "    ┃");
                    break;
                case "insideGame":
                    Console.WriteLine("┃                                                         ┃");
                    Console.WriteLine("┃   Você inseriu " + userInput + " por favor insira um número de 0 a " + MAXGUESS + " ┃");
                    break;
                case "fatalError":
                    Console.WriteLine("┃                                                         ┃");
                    Console.WriteLine("┃  Forma de Inserção inválida,o programa será finalizado  ┃");
                    break;
                default:
                    break;
            }
            Console.WriteLine("┃                                                         ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

            Console.Write("\nPressione qualquer tecla para continuar ");
            Console.ReadKey();
        }
        public static void GameplayScreen()
        {
            Console.Clear();
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┃       Você terá uma chance por tentiva para chutar o número,      ┃");
            Console.WriteLine("┃  após finalizida a tentativa o número será novamente randomizado. ┃");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┃        Traduzindo, a cada tentativa o número será diferente.      ┃");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┃                            Boa Sorte !                            ┃");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");
            Console.Write("\nInsira um número de 0 a " + MAXGUESS + ": ");
        }
        public static void TryAgainScreen()
        {
            Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┃                     Quer tentar novamente ?                       ┃");
            Console.WriteLine("┃ Lembre-se que o número qual você deverá descobrir será diferente. ┃");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┃                    1 - Para tentar novamente                      ┃");
            Console.WriteLine("┃                     2 - Para voltar ao menu                       ┃");
            Console.WriteLine("┃                                                                   ┃");
            Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

            Console.Write("\nInsira uma das opções acima: ");

        }
    }
}