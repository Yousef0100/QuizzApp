using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class Program
    {
        public static void SetConsoleSize(int width, int height)
        {
            // Ensure requested width/height are within legal bounds
            int safeWidth = Math.Min(width, Console.LargestWindowWidth);
            int safeHeight = Math.Min(height, Console.LargestWindowHeight);

            // Ensure buffer is at least as large as the current window
            safeWidth = Math.Max(safeWidth, Console.WindowWidth);
            safeHeight = Math.Max(safeHeight, Console.WindowHeight);

            // Set buffer size first
            Console.SetBufferSize(safeWidth, safeHeight);

            // Then set window size (must be <= buffer size)
            Console.SetWindowSize(safeWidth, safeHeight);
        }



        static void Main(string[] args)
        {
            Question q1 = new TrueFalseQuestion("Is C# an object-oriented language?", 10, 10);
            // Add valid answers
            q1.AddValidAnswer(0); // true

            Question q2 = new SingleChoiceQuestion("Which keyword is used to define a class in C#?", 10, 10);
            q2.AddOption("function");
            q2.AddOption("class");   // ✅
            q2.AddOption("struct");
            q2.AddOption("def");
            // Add valid answers
            q2.AddValidAnswer(1);

            Question q3 = new MultiChoiceQuestion("Which of the following are value types in C#?", 10, 10);
            q3.AddOption("int");     // ✅
            q3.AddOption("string");
            q3.AddOption("double");  // ✅
            q3.AddOption("object");
            // Add valid answers
            q3.AddValidAnswer(0);
            q3.AddValidAnswer(2);

            Question q4 = new SingleChoiceQuestion("Which method is the entry point of every C# program?", 10, 10);
            q4.AddOption("Run()");
            q4.AddOption("Main()");  // ✅
            q4.AddOption("Start()");
            q4.AddOption("Execute()");
            // Add valid answers
            q4.AddValidAnswer(1);

            Question q5 = new TrueFalseQuestion("In C#, arrays have a fixed size once created.", 10, 10);
            // Add valid answers
            q5.AddValidAnswer(0); // true

            Question q6 = new MultiChoiceQuestion("Which of the following are access modifiers in C#?", 10, 10);
            q6.AddOption("public");    // ✅
            q6.AddOption("private");   // ✅
            q6.AddOption("sealed");
            q6.AddOption("protected"); // ✅
                                       // Add valid answers
            q6.AddValidAnswer(0);
            q6.AddValidAnswer(1);
            q6.AddValidAnswer(3);

            Question q7 = new TrueFalseQuestion("Is encapsulation one of the four main pillars of OOP?", 10, 10);
            // Add valid answers
            q7.AddValidAnswer(0); // true

            Question q8 = new SingleChoiceQuestion("Which of the following best defines inheritance?", 10, 10);
            q8.AddOption("A class acquiring properties of another class"); // ✅
            q8.AddOption("Hiding implementation details from users");
            q8.AddOption("Representing real-world entities as objects");
            q8.AddOption("Binding data and methods together");
            // Add valid answers
            q8.AddValidAnswer(0);

            Question q9 = new MultiChoiceQuestion("Which of the following are OOP concepts?", 10, 10);
            q9.AddOption("Polymorphism");  // ✅
            q9.AddOption("Encapsulation"); // ✅
            q9.AddOption("Abstraction");   // ✅
            q9.AddOption("Recursion");
            // Add valid answers
            q9.AddValidAnswer(0);
            q9.AddValidAnswer(1);
            q9.AddValidAnswer(2);

            Question q10 = new SingleChoiceQuestion("Which keyword in C# is used to prevent a class from being inherited?", 10, 10);
            q10.AddOption("abstract");
            q10.AddOption("sealed");   // ✅
            q10.AddOption("static");
            q10.AddOption("readonly");
            // Add valid answers
            q10.AddValidAnswer(1);

            Question q11 = new MultiChoiceQuestion("Which of these are access modifiers in C#?", 10, 10);
            q11.AddOption("public");    // ✅
            q11.AddOption("private");   // ✅
            q11.AddOption("protected"); // ✅
            q11.AddOption("internal");  // ✅
            q11.AddOption("external");
            // Add valid answers
            q11.AddValidAnswer(0);
            q11.AddValidAnswer(1);
            q11.AddValidAnswer(2);
            q11.AddValidAnswer(3);



            //// Add user answers
            //q1.AddUserAnswer(0);

            //q2.AddUserAnswer(1);

            ////q3.AddUserAnswer(0);
            //q3.AddUserAnswer(1);
            //q3.AddUserAnswer(2);


            //Console.WriteLine(q1.ToString());
            //Console.WriteLine(q1.ValidateAnswer());
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine(q2.ToString());
            //Console.WriteLine(q2.ValidateAnswer());
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine(q3.ToString());
            //Console.WriteLine(q3.ValidateAnswer());
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.ReadKey();
            //Console.ReadKey();

            Exam exam = new Exam();

            exam.AddQuestion(q1);
            exam.AddQuestion(q2);
            exam.AddQuestion(q3);
            exam.AddQuestion(q4);
            exam.AddQuestion(q5);
            exam.AddQuestion(q6);
            exam.AddQuestion(q7);
            exam.AddQuestion(q8);
            exam.AddQuestion(q9);
            exam.AddQuestion(q10);
            exam.AddQuestion(q11);

            //StringBuilder header = new StringBuilder();
            //header.Append("Hamada, are you ok?\n");
            //header.Append("Hamada, are you ok? Hamada, are you ok? Hamada, are you ok?\n");
            //header.Append("Hamada, are you ok? Hamada, are you ok?");


            //Console.WriteLine(header.ToString());
            //Console.ReadKey();

            //Menu menu = new Menu(header.ToString(), "Menu Test Title, Mother Father...");
            //menu.AddMenuItem(q1.Title, () => { Console.WriteLine("hello1"); });
            //menu.AddMenuItem(q2.Title, () => { Console.WriteLine("hello2"); });
            //menu.AddMenuItem(q3.Title, () => { Console.WriteLine("hello3"); });

            //menu.Display();


            SetConsoleSize(120, 40);
            Console.CursorVisible = false;

            Dashboard dashboard = new Dashboard(exam);
            dashboard.Display();
        }
    }
}
