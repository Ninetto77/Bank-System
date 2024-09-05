using Lesson10.MVP;
using System.Windows;
using System.Windows.Input;


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

        Worker worker;
        public MainWindow(Worker _worker)
        {
			worker = _worker;

			InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            mainPage = new MainPage(worker);
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

		private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}
    }
}
