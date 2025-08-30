using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    enum QuestionType
    {
        TrueOrFalse,
        SingleChoice,
        MultiChoice
    }

    struct ValidationResult
    {
        public Question question;
        public List<int> correctAnswers;
        public List<int> wrongAnswers;
        public List<int> missingAnswers;

        public int finalScore;

        public override string ToString()
        {
            var sb = new StringBuilder();

            //sb.AppendLine($"Question: {question.Title}");
            sb.AppendLine($"Final Score: {finalScore}");

            sb.AppendLine("Correct Answers: " +
                (correctAnswers.Count > 0 ? string.Join(", ", correctAnswers) : "None"));

            sb.AppendLine("Wrong Answers: " +
                (wrongAnswers.Count > 0 ? string.Join(", ", wrongAnswers) : "None"));

            sb.AppendLine("Missing Answers: " +
                (missingAnswers.Count > 0 ? string.Join(", ", missingAnswers) : "None"));

            return sb.ToString();
        }
    }

    internal abstract class Question
    {
        private string _title;
        private int _mark;
        private int _penalty;
        private int _maxMark;
        private QuestionType _type;
        private ValidationResult _validationResult;


        protected readonly Answer correctAnswer;
        protected readonly Answer userAnswer;

        private readonly List<Option> options;

        public string Title
        {
            get {
                return _title;
            }
            set {
                _title = value;
            }
        }
        public int Mark
        {
            get {
                return _mark;
            }
            set {
                if (value <= 0)
                    throw new InvalidOperationException("Mark must be greater than 0.");
                _mark = value;
            }
        }
        public int Penalty
        {
            get {
                return _penalty;
            }
            set {
                if (value <= 0)
                    throw new InvalidOperationException("Penalty must be greater than 0.");
                _penalty = value;
            }
        }
        public int MaxMark
        {
            get {
                return correctAnswer.GetCount() * Mark;
            }
        }
        public QuestionType Type
        {
            get {
                return _type;
            }

            set { 
                _type = value;
            }
        }
        public ValidationResult ValidationResult
        {
            get {
                return _validationResult;
            }
            protected set {
                _validationResult = value;
            }
        }


        protected Question ()
        {
            _validationResult = new ValidationResult
            {
                question = this,
                correctAnswers = new List<int>(),
                wrongAnswers = new List<int>(),
                missingAnswers = new List<int>(),
                finalScore = 0
            };

            correctAnswer = new Answer(this);
            userAnswer = new Answer(this);

            options = new List<Option>();
        }
        public Question (QuestionType type) : this ()
        {
            this.Type = type;
        }
        public Question (string title, QuestionType type, int mark, int penalty) : this()
        {
            this.Title = title;
            this.Type = type;
            this.Mark = mark;
            this.Penalty = penalty;
        }


        public virtual ValidationResult ValidateAnswer()
        {
            _validationResult = new ValidationResult
            {
                question = this,
                correctAnswers = new List<int>(),
                wrongAnswers = new List<int>(),
                missingAnswers = new List<int>(),
                finalScore = 0
            };

            // if no correct answers are defined, all user answers are wrong
            if (correctAnswer.GetCount() == 0)
            {
                foreach (var opt in options)
                    _validationResult.wrongAnswers.Add(opt.Id);

                return _validationResult;
            }

            // start with all correct answers in missing
            for (int i = 0; i < correctAnswer.GetCount(); i++)
                _validationResult.missingAnswers.Add(correctAnswer.GetAnswerId(i));

            // check each user answer against correct answers
            for (int i = 0; i < userAnswer.GetCount(); i++)
            {
                int usrAnsId = userAnswer.GetAnswerId(i);

                if (correctAnswer.Contains(usrAnsId))  // user picked a valid correct answer
                {
                    _validationResult.correctAnswers.Add(usrAnsId);
                    _validationResult.missingAnswers.Remove(usrAnsId);
                }
                else // user picked something not in correct answers
                {
                    _validationResult.wrongAnswers.Add(usrAnsId);
                }
            }

            int maxScore = correctAnswer.GetCount() * Mark;
            int gained = _validationResult.correctAnswers.Count * Mark;
            int lost = _validationResult.wrongAnswers.Count * Penalty;
            _validationResult.finalScore = gained - lost;
            _validationResult.finalScore = Math.Max(0, Math.Min(maxScore, _validationResult.finalScore));

            return _validationResult;
        }



        public virtual void AddUserAnswer(int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= options.Count)
                return;

            AddUserAnswer(options[optionIdx]);
        }
        public virtual void AddUserAnswer (Option option)
        {
            userAnswer.Add(option.Id);
        }
        public virtual void RemoveUserAnswer(int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= options.Count)
                return;

            RemoveUserAnswer(options[optionIdx]);
        }
        public virtual void RemoveUserAnswer (Option option)
        {
            userAnswer.Remove(option.Id);
        }


        public virtual void AddValidAnswer(int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= options.Count)
                return;

            AddValidAnswer(options[optionIdx]);
        }
        public virtual void AddValidAnswer(Option option)
        {
            correctAnswer.Add(option.Id);
        }
        public virtual void RemoveValidAnswer(int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= options.Count)
                return;

            RemoveValidAnswer(options[optionIdx]);
        }
        public virtual void RemoveValidAnswer(Option option)
        {
            correctAnswer.Remove(option.Id);
        }


        public virtual void AddOption (string  title)
        {
            options.Add(new Option(title));
        }
        public virtual void AddOption (Option option)
        {
            int x = options.IndexOf(option);

            if (x == -1) options.Add(option);
            else options[x] = option;
        }
        public virtual void RemoveOption (Option option)
        {
            // clear dependecies
            RemoveValidAnswer(option);
            RemoveUserAnswer(option);

            options.Remove(option);
        }
        public virtual void RemoveOption (int optionIdx)
        {
            if (optionIdx < 0 || optionIdx >= options.Count)
                return;

            RemoveOption(options[optionIdx]);
        }


        public void ClearUserAnswers()
        {
            userAnswer.Clear();
        }
        public void ClearValidAnswers()
        {
            correctAnswer.Clear();
        }
        public void ClearOptions()
        {
            ClearUserAnswers();
            ClearValidAnswers();
            options.Clear();
        }


        public int GetCorrectAnswersCount()
        {
            return correctAnswer.GetCount();
        }


        public int GetOptionsCount()
        {
            return options.Count;
        }
        public Option GetOption(int idx)
        {
            if (idx < 0 || idx >= options.Count)
                return null;

            return options[idx];
        }
        public int GetOptionIdx(int id)
        {
            int idx = options.FindIndex((option) => option.Id == id);

            return idx;
        }
        public void UpdateOptionTitle(int idx, string newTitle)
        {
            if (idx < 0 || idx >= options.Count)
                return;

            Option o = options[idx];
            o.Title = newTitle;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"~rPoints: {Mark}#\t\t{Type.ToString()}");
            sb.AppendLine($"~g{Title}#");

            for (int i = 0; i < options.Count; ++i)
            {
                sb.Append($"\t{i+1}. ");
                sb.AppendLine(options[i].ToString());
            }

            return sb.ToString();
        }
    }
}