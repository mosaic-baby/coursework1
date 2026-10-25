using coursework1;
using System;
using System.Collections.Generic;
namespace coursework1
{
    public class Question
    {
        public string Text { get; set; }
        public List<AnswerOption> Options { get; set; } = new List<AnswerOption>();

        public Question(string text)
        {
            Text = text;
        }

        public void AddOption(string text, bool isCorrect)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Текст ответа не может быть пустым");

            Options.Add(new AnswerOption(text, isCorrect));
        }
    }
}