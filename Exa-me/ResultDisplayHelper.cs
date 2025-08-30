using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal static class ResultDisplayHelper
    {
        /// <summary>
        /// Applies result states to question menu items based on validation results
        /// </summary>
        public static void ApplyResultStates(List<QuestionMenu> questionMenus, List<ValidationResult> results)
        {
            if (questionMenus.Count != results.Count)
                throw new ArgumentException("Question menus and results count must match");

            for (int i = 0; i < questionMenus.Count; i++)
            {
                ApplyResultStateToQuestion(questionMenus[i], results[i]);
            }
        }

        /// <summary>
        /// Applies result state to a single question menu based on its validation result
        /// </summary>
        private static void ApplyResultStateToQuestion(QuestionMenu questionMenu, ValidationResult result)
        {
            // Clear existing selection states but keep default state
            foreach (var menuItem in questionMenu.GetMenuItems())
            {
                menuItem.State = MenuItemState.Default;
            }

            foreach (int correctId in result.correctAnswers)
            {
                var correctItem = questionMenu.GetMenuItems().FirstOrDefault(item => GetOptionId(item) == correctId);
                if (correctItem != null)
                {
                    correctItem.State |= MenuItemState.Correct;
                }
            }

            foreach (int wrongId in result.wrongAnswers)
            {
                var wrongItem = questionMenu.GetMenuItems().FirstOrDefault(item => GetOptionId(item) == wrongId);
                if (wrongItem != null)
                {
                    wrongItem.State |= MenuItemState.Wrong;
                }
            }

            foreach (int missingId in result.missingAnswers)
            {
                var missingItem = questionMenu.GetMenuItems().FirstOrDefault(item => GetOptionId(item) == missingId);
                if (missingItem != null)
                {
                    missingItem.State |= MenuItemState.Missing;
                }
            }
        }

        /// <summary>
        /// Helper method to extract option ID from menu item
        /// Assumes menu item title or has some way to identify the option ID
        /// </summary>
        private static int GetOptionId(MenuItem menuItem)
        {
            // This would need to be implemented based on how you store option IDs
            // You might need to modify MenuItem to store the option ID
            // For now, this is a placeholder that would need actual implementation
            throw new NotImplementedException("Need to implement option ID extraction from MenuItem");
        }

        /// <summary>
        /// Displays a summary of exam results
        /// </summary>
        public static void DisplayResultSummary(List<ValidationResult> results)
        {
            int totalQuestions = results.Count;
            int totalCorrect = results.Sum(r => r.correctAnswers.Count);
            int totalWrong = results.Sum(r => r.wrongAnswers.Count);
            int totalMissing = results.Sum(r => r.missingAnswers.Count);
            int totalScore = results.Sum(r => r.finalScore);

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("EXAM RESULTS SUMMARY");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Total Questions: {totalQuestions}");
            Console.WriteLine($"Correct Answers: {totalCorrect}");
            Console.WriteLine($"Wrong Answers: {totalWrong}");
            Console.WriteLine($"Missing Answers: {totalMissing}");
            Console.WriteLine($"Final Score: {totalScore}");
            Console.WriteLine($"Percentage: {(double)totalScore / (totalQuestions * 10) * 100:F1}%");
            Console.WriteLine(new string('=', 50));
        }
    }
}
