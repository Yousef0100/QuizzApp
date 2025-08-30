using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    enum Mode
    {
        Exam,
        Results
    }

    internal class QuestionMenu : Menu
    {
        public Question question { get; private set; }
        public Dictionary<MenuItem, int> menuItemOption; // stores menuItem, OptionId
        public int prefix;
        public Mode mode;

        public QuestionMenu(int prefix, Question question, bool showNumbering = true, bool deactivated = true, bool allowSelection = true) : base()
        {
            menuItemOption = new Dictionary<MenuItem, int>();

            this.prefix = prefix;
            this.mode = Mode.Exam;

            this.question = question;
            this.Header = $"({prefix})";

            StringBuilder sb = new StringBuilder();
            //sb.Append($"({prefix})\n");
            //sb.Append($"Points: {question.Mark.ToString()}                                                                                             {question.Type.ToString()}\n");
            sb.Append($"{question.Title}");

            this.Title = sb.ToString();

            int optionsCount = question.GetOptionsCount();
            for (int i = 0; i < optionsCount; ++i) {
                AddMenuItem(question.GetOption(i).ToString());
                menuItemOption.Add(items[items.Count - 1], question.GetOption(i).Id);
            }

            menuSettings = MenuThemes.myExamTheme;
        }

        public override void Select(int itemIdx)
        {
            if (question.Type == QuestionType.TrueOrFalse || question.Type == QuestionType.SingleChoice)
                if (!items[itemIdx].State.HasFlag(MenuItemState.Selected))
                    ClearSelection();

            base.Select(itemIdx);

            if (items[itemIdx].State.HasFlag(MenuItemState.Selected))
                question.AddUserAnswer(itemIdx);
            else
                question.RemoveUserAnswer(itemIdx);
        }
        public override void Select()
        {
            Select(currPtr);
        }

        public void ChangeMode(Mode newMode)
        {
            mode = newMode;

            if (mode == Mode.Results)
                Validate();
        }

        public override void Display()
        {
            if (mode == Mode.Exam) {
                base.Display();
                return;
            }

            menuSettings = MenuThemes.myResultsTheme;
            ValidationResult result = question.ValidationResult;

            // header
            if (result.finalScore >= question.MaxMark / 2)
                SetConsoleColor(ConsoleColor.Green, ConsoleColor.White);
            else 
                SetConsoleColor(ConsoleColor.Red, ConsoleColor.White);

            string totalS = "";
            string s = $"(Q-{prefix}) ";
            Console.Write(s);
            FlipConsoleColor();
            
            totalS += s;
            s = $"{(question.Type == QuestionType.TrueOrFalse ? "TF" : question.Type == QuestionType.SingleChoice ? "SC" : "MC")}";
            Console.Write(s);
            FlipConsoleColor();
            
            totalS += s;
            s = $"{result.correctAnswers.Count} / {question.GetCorrectAnswersCount()}";
            int carterXPos = CenterCarter(s.Length);
            s = new string(' ', carterXPos - totalS.Length) + s;
            Console.Write(s);

            totalS += s;
            s = $"[{result.finalScore} / {question.MaxMark}] ";
            s = new string(' ', Console.WindowWidth - totalS.Length - s.Length) + s;
            Console.WriteLine(s);
            
            totalS += s;


            //string[] headerLines = Header.Split('\n');
            //foreach (string line in headerLines)
            //{
            //    int carterXPos = CenterCarter(line.Length);
            //    string s = new String(' ', carterXPos) + line;
            //    s += new String(' ', Console.WindowWidth - s.Length);
            //    Console.WriteLine(s);
            //}

            Console.ResetColor();

            // title
            SetConsoleColor(menuSettings.titleBackgroundColor, menuSettings.titleForegroundColor);

            string[] titleLines = Title.Split('\n');
            foreach (string line in titleLines)
            {
                if (line.Length == 0)
                    continue;

                string str = line.Trim();
                str += new String(' ', Console.WindowWidth - str.Length);
                Console.WriteLine(str);
            }

            Console.ResetColor();

            // menuItems
            for (int i = 0; i < items.Count; ++i)
            {
                MenuItem item = items[i];
                int optionId = menuItemOption[item];

                if (result.correctAnswers.Contains(optionId))
                    item.AddState(MenuItemState.Correct);
                else if (result.wrongAnswers.Contains(optionId))
                    item.AddState(MenuItemState.Wrong);
                else if (result.missingAnswers.Contains(optionId))
                    item.AddState(MenuItemState.Missing);

                // determining color schema
                switch (item.State)
                {
                    //case var x when deactivated:
                    //    SetConsoleColor(menuSettings.deactivatedItemBackgroundColor, menuSettings.deactivatedItemForegroundColor);
                    //    break;

                    case var x when x.HasFlag(MenuItemState.Highlighted) && mode == Mode.Results:
                        SetConsoleColor(menuSettings.highlightedItemBackgroundColor, menuSettings.highlightedItemForegroundColor);
                        break;

                    case var x when x.HasFlag(MenuItemState.Correct) && mode == Mode.Results:
                        SetConsoleColor(menuSettings.correctItemBackgroundColor, menuSettings.correctItemForegroundColor);
                        break;

                    case var x when x.HasFlag(MenuItemState.Wrong) && mode == Mode.Results:
                        SetConsoleColor(menuSettings.wrongItemBackgroundColor, menuSettings.wrongItemForegroundColor);
                        break;

                    case var x when x.HasFlag(MenuItemState.Missing) && mode == Mode.Results:
                        SetConsoleColor(menuSettings.missingItemBackgroundColor, menuSettings.missingItemForegroundColor);
                        break;

                    default:
                        SetConsoleColor(menuSettings.titleBackgroundColor, menuSettings.titleForegroundColor);
                        break;
                }

                // printing the item
                int prefix = i + 1;
                string itemStr = item.ToString();
                //string str = $"{(showNumbering ? $"[ ]  {prefix}. " : "")}{itemStr}";
                string str = $"{(showNumbering ? $"[{ (item.State.HasFlag(MenuItemState.Correct) ? "✅" : item.State.HasFlag(MenuItemState.Wrong) ? "❌" : item.State.HasFlag(MenuItemState.Missing) ? "❕" : "  ") }]   " : "")}{itemStr}";
                str += new String(' ', 80 - str.Length);
                Console.WriteLine(str);

                Console.ResetColor();
            }

        }

        public ValidationResult Validate()
        {
            return question.ValidateAnswer();
        }
    }
}
