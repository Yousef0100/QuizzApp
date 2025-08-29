using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class MultiChoiceQuestion : Question
    {
        public MultiChoiceQuestion(string title, int mark, int penalty) : base(title, QuestionType.MultiChoice, mark, penalty)
        {

        }
    }
}
