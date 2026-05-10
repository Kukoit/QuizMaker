using Quiz_Maker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Quiz_Maker.Repositories
{
    internal class QuizRepository

    {

        private string filePath = "questions.xml";
        public void Save(List<Question> questions)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, questions);
            }
        }

        public List<Question> Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            if (!File.Exists(filePath))
                return new List<Question>();
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                return (List<Question>)serializer.Deserialize(stream);
            }


        }
    }
}
