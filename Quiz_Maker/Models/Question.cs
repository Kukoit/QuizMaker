using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maker.Models
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Choices { get; set; }
        public List<string> CorrectAnswers { get; set; }


    }
}
