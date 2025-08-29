using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion (string title, int mark, int penalty) : base (title, QuestionType.TrueOrFalse, mark, penalty)
        {
            base.AddOption("True");
            base.AddOption("False");
        }


        public override void AddValidAnswer(int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= GetOptionsCount())
                return;

            
            AddValidAnswer(GetOption(optionIdx));
        }
        public override void AddValidAnswer(Option option)
        {
            ClearValidAnswers();
            base.AddValidAnswer(option);
        }


        public override void AddUserAnswer(int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= GetOptionsCount())
                return;

            AddUserAnswer(GetOption(optionIdx));
        }
        public override void AddUserAnswer(Option option)
        {
            ClearUserAnswers();
            base.AddUserAnswer(option);
        }


        public override void AddOption(string title)
        {
            throw new InvalidOperationException(
                "Cannot add options to a True/False question. Use SetLabels instead."
            );
        }
        public override void AddOption(Option option)
        {
            throw new InvalidOperationException(
                "Cannot add options to a True/False question. Use SetLabels instead."
            );
        }
        public override void RemoveOption(int optionIdx)
        {
            throw new InvalidOperationException(
                "Cannot remove options from a True/False question. Use SetLabels instead."
            );
        }
        public override void RemoveOption(Option option)
        {
            throw new InvalidOperationException(
                "Cannot remove options from a True/False question. Use SetLabels instead."
            );
        }


        public void SetLabels(string trueLabel, string falseLabel)
        {
            UpdateOptionTitle(0, trueLabel);
            UpdateOptionTitle(1, falseLabel);
        }


        public override string ToString()
        {
            return base.ToString();
        }
    }
}
