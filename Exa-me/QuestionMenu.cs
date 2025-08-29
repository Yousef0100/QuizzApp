using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class QuestionMenu : Menu
    {
        public Question question { get; private set; }
        public int prefix;

        public QuestionMenu(int prefix, Question question, bool showNumbering = true, bool deactivated = true, bool allowSelection = true) : base()
        {
            this.prefix = prefix;

            this.question = question;
            this.Header = $"({prefix})";

            StringBuilder sb = new StringBuilder();
            //sb.Append($"({prefix})\n");
            sb.Append($"Points: {question.Mark.ToString()}                                                                                             {question.Type.ToString()}\n");
            sb.Append($"{question.Title}");

            this.Title = sb.ToString();

            int optionsCount = question.GetOptionsCount();
            for (int i = 0; i < optionsCount; ++i)
                AddMenuItem(question.GetOption(i).ToString());


            menuSettings = MenuThemes.myTheme;
        }

        public override void Select(int itemIdx)
        {
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

        public ValidationResult Validate()
        {
            return question.ValidateAnswer();
        }
    }
}
