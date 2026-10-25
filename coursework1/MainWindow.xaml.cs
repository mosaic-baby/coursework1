using System.Collections.Generic;
using System.Windows;

namespace coursework1
{
    public partial class MainWindow : Window
    {
        private QuizRepository _repository = new QuizRepository();
        private List<Quiz> _quizzes = new List<Quiz>();

        public MainWindow()
        {
            InitializeComponent();
            LoadQuizzes();
        }

        // Загрузка списка викторин в ListBox
        private void LoadQuizzes()
        {
            _quizzes = _repository.Load();

            // Если файл пустой (первый запуск), создадим тестовую викторину для проверки
            if (_quizzes.Count == 0)
            {
                var testQuiz = new Quiz("Тестовая викторина по C#");
                var question = new Question("Что такое CLR?");
                question.AddOption("Common Language Runtime", true);
                question.AddOption("Компьютерная мышь", false);
                testQuiz.AddQuestion(question);

                _quizzes.Add(testQuiz);
                _repository.Save(_quizzes); // Сохраняем тестовую
            }

            QuizzesListBox.ItemsSource = _quizzes;
        }

        // Кнопка: Начать прохождение
        // Кнопка: Начать прохождение
        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedQuiz = QuizzesListBox.SelectedItem as Quiz;
            if (selectedQuiz == null)
            {
                MessageBox.Show("Пожалуйста, выберите викторину из списка!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Открываем окно игры и передаем туда выбранную викторину
            GameWindow gameWindow = new GameWindow(selectedQuiz);
            gameWindow.Owner = this; // Чтобы окно открывалось по центру главного
            gameWindow.ShowDialog(); // Открываем как модальное окно
        }



        // Кнопка: Создать викторину
        private void CreateQuizButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно создания
            CreateQuizWindow createWindow = new CreateQuizWindow();
            createWindow.Owner = this;

            // Если пользователь нажал кнопку "Сохранить викторину" (DialogResult == true)
            if (createWindow.ShowDialog() == true)
            {
                // Добавляем новую викторину в общий список
                _quizzes.Add(createWindow.NewQuiz);

                // Сохраняем обновленный список в JSON файл
                _repository.Save(_quizzes);

                // Перезагружаем список на главном экране
                QuizzesListBox.ItemsSource = null;
                QuizzesListBox.ItemsSource = _quizzes;

                MessageBox.Show("Викторина успешно создана и сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
