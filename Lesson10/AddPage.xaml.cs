using Lesson10.MVP;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lesson10
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page, IViewAddPage
    {
        Presenter p;

        #region Свойства
        public Department SelectedDepartmentName_CB => ItemsDepartamentComboBox.SelectedItem as Department;

        public string ClientName_TB => SurnameTextBox.Text;

        public string ClientSurname_TB => NameTextBox.Text;

        public string ClientMiddlename_TB => MiddlenameTextBox.Text;

        public string ClientPhone_TB => PhoneTextBox.Text;

        public string ClientPasport_TB => PasportDataTextBox.Text;
        #endregion

        public AddPage()
        {
            InitializeComponent();
            InitButtons();
        }

        private void AddPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow SetWindow = Window.GetWindow(this) as MainWindow;
            p = SetWindow.GetPresenter();
            InitCombobox();
        }

        public void InitButtons()
        { 
            CancelButton.Click += (s, e) => GoMainPage();
            AddButton.Click += (s, e) => AddClient();
        }

        private void AddClient()
        {
            p.AddClient(this);
            GoMainPage();
        }

        /// <summary>
        /// переход на главную страницу
        /// </summary>
        public void GoMainPage()
        {
            MainWindow SetWindow = Window.GetWindow(this) as MainWindow;
            SetWindow.ChangeFrame("MainPage");
        }

        private void InitCombobox()
        {
            ItemsDepartamentComboBox.ItemsSource = p.Departments;
            ItemsDepartamentComboBox.SelectedIndex = 0;
        }
    }
}
