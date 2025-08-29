using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class Dashboard
    {
        private Exam exam;

        private bool exit;

        private string header = $"To answer a question, write the options you want to choose sperated by a space.\nTo submit a question press the enter / return key";

        private List<QuestionMenu> menus;
        private Menu fixedMenu;
        private Menu focusMenu;

        private int currPtr;
        private int printCount;

        public Dashboard() : this(new Exam())
        {

        }

        public Dashboard(Exam exam)
        {
            this.exam = exam;
            this.exit = false;
            this.currPtr = -1;
            this.printCount = 3;

            menus = new List<QuestionMenu>();

            fixedMenu = new Menu($"User Dashboard\n{header}", "", false, false, false);
            //fixedMenu.AddMenuItem("Back", () => { currPtr = Math.Max(-1, ((currPtr % printCount) - 1) * printCount); });
            //fixedMenu.AddMenuItem("Forward", () => { currPtr = Math.Min(menus.Count - 1, ((currPtr % printCount) + 1) * printCount); });
            fixedMenu.AddMenuItem("Back", () => { currPtr = Math.Max(-1, currPtr - printCount); ChangeMenuFocus(currPtr); });
            fixedMenu.AddMenuItem("Forward", () => { currPtr = Math.Min(menus.Count - 1, currPtr + printCount); ChangeMenuFocus(currPtr); });
            fixedMenu.AddMenuItem("Submit", () => { ValidateExam(); });


            int count = exam.GetQuestionsCount();
            for (int i = 0; i < count; ++i)
            {
                Question q = exam.GetQuestion(i);

                QuestionMenu menu = new QuestionMenu(i + 1, q);
                AddMenu(menu);
            }
        }

        public void AddMenu (QuestionMenu menu)
        {
            int idx = menus.FindIndex((x) => { return menu.Id == x.Id; });

            if (idx == -1)
                menus.Add(menu);
            else
                menus[idx] = menu;
        }

        public void Display()
        {
            while (!exit)
            {
                Clear();
                Console.WriteLine("Keys: (q) => exit ... (r) => restart");

                ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Q) {
                    exit = true;
                    break;
                }
                else if (key.Key == ConsoleKey.R)
                    currPtr = 0;

                focusMenu = fixedMenu;
                
                while (!exit && currPtr < menus.Count)
                {
                    int strIdx = (currPtr / printCount);

                    Console.SetCursorPosition(0, 0);
                    Clear();

                    fixedMenu.Display();
                    for (int i = strIdx * printCount; i < strIdx * printCount + printCount && i < menus.Count; ++i)
                        menus[i].Display();

                    ConsoleKeyInfo k = Console.ReadKey(intercept: true);

                    switch (k.Key)
                    {
                        case ConsoleKey.DownArrow:
                            bool navigatedDown = focusMenu.NavigateDown();
                            if (!navigatedDown && currPtr <= menus.Count - 2) {
                                currPtr++;
                                ChangeMenuFocus(currPtr);
                            }
                            break;

                        case ConsoleKey.UpArrow:
                            bool navigatedUp = focusMenu.NavigateUp();
                            if (!navigatedUp) {
                                if (currPtr == 0) {
                                    currPtr = -1;
                                    ChangeMenuFocus(fixedMenu);
                                }
                                else if (currPtr > 0) {
                                    currPtr--;
                                    ChangeMenuFocus(currPtr);
                                }
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            fixedMenu.Select(1);
                            break;

                        case ConsoleKey.LeftArrow:
                            fixedMenu.Select(0);
                            break;

                        case ConsoleKey.Enter:
                            focusMenu.Select();
                            break;

                        case ConsoleKey.Q:
                            exit = true; 
                            break;

                        default:
                            break;
                    }
                }
            }


            Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Thanks for trying our program");
            Console.ResetColor();
            Console.ReadKey(intercept: true);
        }

        private void ChangeMenuFocus (Menu newMenu)
        {
            focusMenu.Deactivate();
            newMenu.Activate();

            focusMenu = newMenu;
        }
        private void ChangeMenuFocus (int menuIdx)
        {
            if (menuIdx == -1) {
                ChangeMenuFocus(fixedMenu);
                return;
            }

            if (menuIdx < 0 || menuIdx >= menus.Count)
                return;

            ChangeMenuFocus(menus[menuIdx]);
        }

        private void ValidateExam()
        {
            List<ValidationResult> results = new List<ValidationResult>();

            foreach (QuestionMenu qMenu in menus)
            {
                ValidationResult result = qMenu.Validate();
                results.Add(result);
            }

            //ShowResults(results);
        }

        public static void Clear()
        {
            for (int i = 0; i < Console.BufferHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.BufferWidth));
            }

            Console.SetCursorPosition(0, 0);
        }
    }
}
