using Quiz_Maker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz_Maker.Models;

namespace Quiz_Maker.UI
{

    internal class ConsoleUI
    {
        private QuizService service = new QuizService();
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("1. Add Question");
                Console.WriteLine("2. Take Quiz");
                Console.WriteLine("3. Exit");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddQuestion();
                        break;
                    case "2":
                        TakeQuiz();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }
        public void AddQuestion()
        {
            Console.WriteLine("Enter question text:");
            string text = Console.ReadLine();
            List<string> choices = new List<string>();
            while (true)
            {
                Console.WriteLine("Enter a choice (or 'done' to finish):");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "done")
                    break;
                choices.Add(choice);
               
                if (choices.Count == 3)
                {
                    Console.WriteLine("Maximum 3 choices reached.");
                    break;
                }
            }
            List<string> correctAnswers = new List<string>();

            while (true)
            {
                Console.WriteLine("Enter a correct answer (or 'done' to finish):");
                string answer = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(answer))
                {
                    Console.WriteLine("Answer cannot be empty.");
                    continue;
                }
                if (answer.ToLower() == "done")
                    break;
                correctAnswers.Add(answer);
            }
            if (correctAnswers.Count == 0)
            {
                Console.WriteLine("You must enter at least one correct answer. Question not saved.");
                return;
            }
            service.AddQuestion(new Question { Text = text, Choices = choices, CorrectAnswers = correctAnswers });
        }
        public void TakeQuiz()
        {
            List<Question> questions = service.GetAllQuestions();
            if (questions.Count == 0) { Console.WriteLine("No questions!"); return; }
            Random random = new Random();
            var shuffled = questions.OrderBy(q => random.Next()).ToList();
            int score = 0;
            foreach (var question in shuffled)
            {
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Choices.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Choices[i]}");
                }
                List<string> selectedAnswers = new List<string>();
                while (true)
                {
                    Console.WriteLine("Enter the number of a choice (or 'done' to finish):");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "done")
                        break;
                    if (int.TryParse(input, out int choiceNumber) && choiceNumber > 0 && choiceNumber <= question.Choices.Count)
                    {
                        selectedAnswers.Add(question.Choices[choiceNumber - 1]);
                    }
                   
                    else
                    {
                        Console.WriteLine("Invalid choice, try again.");
                    }
                }
                bool isCorrect = service.CheckAnswer(question, selectedAnswers);
                if (isCorrect)
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect. The correct answers are:");
                    foreach (var answer in question.CorrectAnswers)
                    {
                        Console.WriteLine(answer);
                    }
                }
                
                Console.WriteLine();
            }
            Console.WriteLine($"Your score: {score}/{questions.Count}");    
        }
    }
}                           