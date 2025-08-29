using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class Answer
    {
        private readonly Question qusetion;
        private readonly List<int> answers;

        private Answer ()
        {
            answers = new List<int> ();
        }
        public Answer(Question qusetion) : this()
        {
            this.qusetion = qusetion;
        }


        public void Add(int optionId)
        {
            int idx = answers.IndexOf (optionId);

            if (idx == -1)
                answers.Add(optionId);
        }
        public void Remove(int optionId)
        {
            int idx = answers.IndexOf(optionId);

            if (idx == -1)
                return;

            answers.RemoveAt(idx);
        }
        public void Clear()
        {
            answers.Clear();
        }

        public bool Contains (int Id)
        {
            return answers.Contains (Id);
        }
        public int GetAnswerId (int idx)
        {
            return answers[idx];
        }
        public int IndexOf(int optionId)
        {
            return answers.IndexOf(optionId);
        }

        public void Sort()
        {
            answers.Sort();
        }
        public int GetCount()
        {
            return answers.Count;
        }
    }
}
