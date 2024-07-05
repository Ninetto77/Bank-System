using Lesson10.MVP;
using System.Windows;


namespace Lesson10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddPage addPage;
        MainPage mainPage;
        Presenter p;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            mainPage = new MainPage();
            addPage = new AddPage();
            ChangeFrame("MainPage");
        }

        public  void ChangeFrame(string s)
        {
            if (s == "MainPage")
            {
                MainFrame.Content = mainPage;
            }
            else if (s == "AddPage")
            {
                MainFrame.Content = addPage;
            }
        }

        public void SetPresenter(Presenter p)
        {
            this.p = p;
        }

        public Presenter GetPresenter()
        {
            return this.p;
        }

    }
}
