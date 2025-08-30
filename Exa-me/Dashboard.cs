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
        private Menu examFixedMenu;
        private Menu resultsFixedMenu;
        private Menu focusMenu;

        private int currPtr;
        private int printCount;

        private Mode mode;

        public Dashboard() : this(new Exam())
        {
            
        }

        public Dashboard(Exam exam)
        {
            this.exam = exam;
            this.mode = Mode.Exam;
            this.exit = false;
            this.currPtr = -1;
            this.printCount = 3;

            menus = new List<QuestionMenu>();

            examFixedMenu = new Menu($"User Dashboard (Exam)\n{header}", "", false, false, false);
            examFixedMenu.AddMenuItem("Back", () => { currPtr = Math.Max(-1, currPtr - printCount); ChangeMenuFocus(currPtr); });
            examFixedMenu.AddMenuItem("Forward", () => { currPtr = Math.Min(menus.Count - 1, currPtr + printCount); ChangeMenuFocus(currPtr); });
            examFixedMenu.AddMenuItem("Submit", () => { ValidateExam(); });

            resultsFixedMenu = new Menu($"User Dashboard (Results)\n{header}", "", false, false, false);
            resultsFixedMenu.AddMenuItem("Restart", () => {  });
            resultsFixedMenu.AddMenuItem("", () => { Console.WriteLine("Welcome!"); });


            int count = exam.GetQuestionsCount();
            for (int i = 0; i < count; ++i) {
                Question q = exam.GetQuestion(i);

                QuestionMenu menu = new QuestionMenu(i + 1, q);
                menu.ChangeMode(Mode.Exam);
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
                if (mode == Mode.Exam)
                    focusMenu = examFixedMenu;
                else focusMenu = resultsFixedMenu;

                while (!exit && currPtr < menus.Count)
                {
                    Clear();

                    if (mode == Mode.Exam)
                        examFixedMenu.Display();
                    else resultsFixedMenu.Display();

                        int strIdx = (currPtr / printCount);
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
                                    ChangeMenuFocus(currPtr);
                                }
                                else if (currPtr > 0) {
                                    currPtr--;
                                    ChangeMenuFocus(currPtr);
                                }
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            examFixedMenu.Select(1);
                            break;

                        case ConsoleKey.LeftArrow:
                            examFixedMenu.Select(0);
                            break;

                        case ConsoleKey.Enter:
                            if (mode == Mode.Exam || !(focusMenu is QuestionMenu))
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
            Console.WriteLine("Thanks for trying our program");
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
                if (mode == Mode.Exam) 
                    ChangeMenuFocus(examFixedMenu);
                else ChangeMenuFocus(resultsFixedMenu);
                
                return;
            }

            if (menuIdx < 0 || menuIdx >= menus.Count)
                return;

            ChangeMenuFocus(menus[menuIdx]);
        }

        private void ValidateExam()
        {
            foreach (QuestionMenu qMenu in menus) {
                ValidationResult result = qMenu.Validate();
                exam.AddValidationResult(result);

                qMenu.ChangeMode(Mode.Results);
                mode = Mode.Results;
            }
        }


        public static void Clear()
        {
            for (int i = 0; i < Console.BufferHeight; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.BufferWidth));
            }

            Console.SetCursorPosition(0, 0);
        }
    }
}
