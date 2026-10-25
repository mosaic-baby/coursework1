using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coursework1
{
    public class AnswerOption
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerOption(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
    }

}
