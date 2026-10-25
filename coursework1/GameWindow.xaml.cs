using System.Windows;
using System.Windows.Controls;

namespace coursework1
{
    public partial class GameWindow : Window
    {
        private Quiz _quiz;
        private int _currentQuestionIndex = 0;
        private int _correctAnswersCount = 0;

        public GameWindow(Quiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;
            this.Title = _quiz.Title; // Меняем заголовок окна на название викторины
            DisplayQuestion();
        }

        private void DisplayQuestion()
        {
            // Очищаем старые радиокнопки перед показом нового вопроса
            AnswersStackPanel.Children.Clear();

            var currentQuestion = _quiz.Questions[_currentQuestionIndex];

            // Обновляем тексты
            QuestionNumberText.Text = $"Вопрос {_currentQuestionIndex + 1} из {_quiz.Questions.Count}";
            QuestionTitleText.Text = currentQuestion.Text;

            // Динамически создаем RadioButton для каждого варианта ответа
            foreach (var option in currentQuestion.Options)
            {
                var radioButton = new RadioButton
                {
                    Content = option.Text,
                    Tag = option, // Сохраняем весь объект ответа в Tag, чтобы потом проверить на правильность
                    FontSize = 16,
                    Margin = new Thickness(0, 8, 0, 8)
                };
                AnswersStackPanel.Children.Add(radioButton);
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Ищем, какую кнопку выбрал пользователь
            AnswerOption selectedOption = null;
            foreach (UIElement element in AnswersStackPanel.Children)
            {
                if (element is RadioButton rb && rb.IsChecked == true)
                {
                    selectedOption = rb.Tag as AnswerOption;
                    break;
                }
            }

            if (selectedOption == null)
            {
                MessageBox.Show("Пожалуйста, выберите вариант ответа!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверяем ответ
            if (selectedOption.IsCorrect)
            {
                MessageBox.Show("Правильно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                _correctAnswersCount++;
            }
            else
            {
                MessageBox.Show("Неправильно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Переходим к следующему вопросу или выводим результат
            _currentQuestionIndex++;
            if (_currentQuestionIndex < _quiz.Questions.Count)
            {
                DisplayQuestion();
            }
            else
            {
                MessageBox.Show($"Викторина окончена!\nПравильных ответов: {_correctAnswersCount} из {_quiz.Questions.Count}", "Результат");
                this.Close(); // Закрываем окно игры
            }
        }
    }
}
