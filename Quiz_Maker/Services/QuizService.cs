using Quiz_Maker.Models;
using Quiz_Maker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maker.Services
{
    internal class QuizService
    {
        private QuizRepository repository = new QuizRepository();
        public void AddQuestion(Question question)
        {
           List<Question> questions = repository.Load();
            questions.Add(question);
            repository.Save(questions); 
        }
        public List<Question> GetAllQuestions()
        {
         
            return repository.Load();



        }


        public Question GetRandomQuestion()
        {
            List<Question> questions = repository.Load();
            Random random = new Random();
            int index = random.Next(0, questions.Count);
            return questions[index];
        }

        public bool CheckAnswer(Question question, List<string> selectedAnswers)
        {
            var sorted = selectedAnswers.OrderBy(a => a);
            return sorted.SequenceEqual(question.CorrectAnswers.OrderBy(a => a));
        }
    }
}
