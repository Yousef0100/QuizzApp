using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class SingleChoiceQuestion : Question
    {
        public SingleChoiceQuestion(string title, int mark, int penalty) : base(title, QuestionType.SingleChoice, mark, penalty)
        {
            
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


        public override string ToString()
        {
            return base.ToString();
        }
    }
}
