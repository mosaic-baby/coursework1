using System.Collections.Generic;
namespace coursework1
{
    public class Quiz
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();

        public Quiz(string title)
        {
            Title = title;
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
    }
}