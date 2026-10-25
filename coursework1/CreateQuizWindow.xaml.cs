using System.Windows;

namespace coursework1
{
    public partial class CreateQuizWindow : Window
    {
        // Сюда мы будем собирать новую викторину
        public Quiz NewQuiz { get; private set; }

        public CreateQuizWindow()
        {
            InitializeComponent();
            NewQuiz = new Quiz("");
        }

        // Кнопка: Добавить вопрос
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text) ||
                string.IsNullOrWhiteSpace(CorrectAnswerTextBox.Text) ||
                string.IsNullOrWhiteSpace(WrongAnswer1TextBox.Text))
            {
                MessageBox.Show("Заполните хотя бы текст вопроса, правильный ответ и один неправильный!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
                // Обновляем надпись с количеством вопросов
               

            }

            // Создаем вопрос и добавляем варианты ответов
            var question = new Question(QuestionTextBox.Text);
            question.AddOption(CorrectAnswerTextBox.Text, true);
            question.AddOption(WrongAnswer1TextBox.Text, false);

            if (!string.IsNullOrWhiteSpace(WrongAnswer2TextBox.Text))
            {
                question.AddOption(WrongAnswer2TextBox.Text, false);
            }

            // Добавляем вопрос в нашу викторину и обновляем список на экране
            NewQuiz.AddQuestion(question);
            AddedQuestionsListBox.ItemsSource = null;
            AddedQuestionsListBox.ItemsSource = NewQuiz.Questions;

            // Очищаем поля ввода для следующего вопроса
            QuestionTextBox.Clear();
            CorrectAnswerTextBox.Clear();
            WrongAnswer1TextBox.Clear();
            WrongAnswer2TextBox.Clear();

            AddedQuestionsText.Text = $"Вопросов в текущей викторине: {NewQuiz.Questions.Count}";
        }

        // Кнопка: Сохранить всю викторину
        private void SaveQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuizTitleTextBox.Text))
            {
                MessageBox.Show("Введите название викторины!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NewQuiz.Questions.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один вопрос в викторину!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewQuiz.Title = QuizTitleTextBox.Text;
            this.DialogResult = true; // Указывает, что окно закрыто успешно с сохранением
            this.Close();
        }
    }
}
