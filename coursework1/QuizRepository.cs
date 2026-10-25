using System.IO;
using System.Collections.Generic;
// Вместо System.Text.Json используем Newtonsoft
using Newtonsoft.Json;

namespace coursework1
{
    public class QuizRepository
    {
        private readonly string _filePath = "quizzes.json";

        public void Save(List<Quiz> quizzes)
        {
            // Formatting.Indented делает JSON красивым и читаемым человеком
            string json = JsonConvert.SerializeObject(quizzes, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public List<Quiz> Load()
        {
            if (!File.Exists(_filePath)) return new List<Quiz>();

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Quiz>>(json) ?? new List<Quiz>();
        }
    }
}
