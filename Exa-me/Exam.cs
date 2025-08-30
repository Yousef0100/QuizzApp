using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class Exam
    {
        private readonly List<Question> questions;
        private readonly List<ValidationResult> results;

        private int curr_ptr;
        private int score;
        private int totalTimeInSeconds;
        private int currTimeInSeconds;


        public Exam (int totalTimeInSeconds = 180)
        {
            questions = new List<Question> ();
            results = new List<ValidationResult> ();
            curr_ptr = 0;
            score = 0;

            this.totalTimeInSeconds = totalTimeInSeconds;
            this.currTimeInSeconds = 0;
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }
        public void RemoveQuestion(Question question)
        {
            int idx = questions.IndexOf(question);
            
            if (idx != -1)
                questions.RemoveAt(idx);
        }


        public void AddValidationResult(ValidationResult result)
        {
            results.Add(result);
        }
        private void ClearResults()
        {
            results.Clear();
        }


        private void CalculateScore()
        {
            foreach (ValidationResult res in results) {
                score += res.finalScore;
            }
        }


        public Question GetNextQuestion ()
        {
            if (!HasNextQuestion())
                return null;

            return questions[curr_ptr++];
        }
        public Question GetQuestion(int idx)
        {
            if (idx < 0 || idx >= questions.Count)
                return null;

            return questions[idx];
        }
        public int GetQuestionsCount()
        { 
            return questions.Count; 
        }


        public bool HasNextQuestion()
        {
            if (questions.Count == 0) return false;

            return curr_ptr < questions.Count;
        }
    }
}
