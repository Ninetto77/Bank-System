using Lesson10.MVP;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankLogic_Library.DB;


namespace Lesson10
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, IView
    {
        #region Свойства
        /// Вкладка клиент
        public Department SelectedDepartmentName_CB { get => ItemsDepartamentComboBox.SelectedItem as Department; }
        public string SelectedItem_CB { get => ItemsComboBox.SelectedItem.ToString(); }
        public int SelectedItemIndex_CM { get => ItemsComboBox.SelectedIndex; }
        public string ChangedClientData_TB { get => EditTxtBox.Text; }
        public IEnumerable<Clients> ClientData_IV { get => IvWorkers.ItemsSource as IEnumerable<Clients>; set => IvWorkers.ItemsSource = value; }
        public Clients SelectedClient_IV { get => IvWorkers.SelectedItem as Clients; }


        /// Вкладка счета
        public Department SelectedDepartmentName1_CB { get => ItemsDepartamentComboBox1.SelectedItem as Department; }
        public Clients SelectedClient1_IV { get => IvWorkers1.SelectedItem as Clients; }
        public ICapital<BankAccount> SelectedAccount_IV { get => IvAccounts1.SelectedItem as ICapital<BankAccount>; }
        public ICapital<BankAccount> SecondSelectedAccount_CB { get => ItemsAccountComboBox1.SelectedItem as ICapital<BankAccount>; }
        public string Rubles_TB { get => RubleTxtBox1.Text; }
        public IEnumerable<Clients> ClientData1_IV { get => IvWorkers1.ItemsSource as IEnumerable<Clients>; set => IvWorkers1.ItemsSource = value; }
        public IEnumerable<ICapital<BankAccount>> AccountsData_IV { get => IvAccounts1.ItemsSource as IEnumerable<ICapital<BankAccount>>; set => IvAccounts1.ItemsSource = value; }
        public IEnumerable<ICapital<BankAccount>> SecondAccountData_CB { get => ItemsAccountComboBox1.ItemsSource as IEnumerable<ICapital<BankAccount>>; set => ItemsAccountComboBox1.ItemsSource = value; }


        /// Вкладка депозит
        public Department SelectedDepartmentName2_CB { get => ItemsDepartamentComboBox2.SelectedItem as Department; }
        public string Rubles2_TB { get => RubleTxtBox2.Text; }


        // вкладка переводы
        public Department SelectedDepartmentName3_CB { get => ItemsDepartamentComboBox3.SelectedItem as Department; }
        public Clients SelectedClient3_IV { get => IvWorkers1.SelectedItem as Clients; }
        public ICapital<BankAccount> SelectedAccount3_IV { get => IvAccounts1.SelectedItem as ICapital<BankAccount>; }


        public Department SecondSelectedDepartmentName3_CB { get => ItemsDepartament2ComboBox3.SelectedItem as Department; }
        public Clients SecondSelectedClient3_IV { get => ItemsClientComboBox3.SelectedItem as Clients; }
        public ICapital<BankAccount> SecondSelectedAccount3_CB { get => ItemsAccountComboBox3.SelectedItem as ICapital<BankAccount>; }
        public string Rubles3_TB { get => RubleTxtBox3.Text; }
        #endregion

        #region Поля
        Presenter p;
        Worker worker;
        #endregion

        #region Конструкторы
        public MainPage(Worker _worker)
        {
            worker = _worker;

            InitializeComponent();
            InitializePresenter();
            Init();
        }
        #endregion

		#region Инициализация		
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow SetWindow = Window.GetWindow(this) as MainWindow;
            SetWindow.SetPresenter(p);
            //IvWorkers.ItemsSource = worker.clients.Where(w => int.Parse(w.DepartametId) == (ItemsDepartamentComboBox.SelectedItem as Department).DepartmentId);
        }

        /// <summary>
        /// Создание представления
        /// </summary>
        private void InitializePresenter()
        {
            p = new Presenter(worker, this);
        }

        private void Init()
        {
            InitializeCombobox();
            InitializeListView();
            InitClickEventButtons();
            InitSelectedChangedCombobox();
            InitSelectedChangedListView();
        }

        private void InitializeCombobox()
        {
            // клиенты
            ItemsDepartamentComboBox.ItemsSource = p.Departments;
            ItemsComboBox.ItemsSource = p.Items;
            // счета
            ItemsDepartamentComboBox1.ItemsSource = p.Departments;
            //депозит
            ItemsDepartamentComboBox2.ItemsSource = p.Departments;
            //переводы
            ItemsDepartamentComboBox3.ItemsSource = p.Departments;
            ItemsDepartament2ComboBox3.ItemsSource = p.Departments;



            // клиенты
            ItemsDepartamentComboBox.SelectedIndex = 0;
            ItemsComboBox.SelectedIndex = 0;
            // счета
            ItemsDepartamentComboBox1.SelectedIndex = 0;
            ItemsAccountComboBox1.SelectedIndex = 0;
            //депозит
            ItemsDepartamentComboBox2.SelectedIndex = 0;
            //переводы
            ItemsDepartamentComboBox3.SelectedIndex = 0;
            ItemsDepartament2ComboBox3.SelectedIndex = 0;


        }

        /// <summary>
        ///  Инициализация listviews
        /// </summary>
        /// <param name="worker"></param>
        private void InitializeListView()
        {
            // клиенты
            ClientData_IV = p.worker.clients.Where(FindWorkers);
            // счета
            ClientData1_IV = p.worker.clients.Where(FindWorkers1);
            // журнал
            IvActivityLog.ItemsSource = p.journal.ActivityLogsList;
        }

        #endregion

        #region Подписки
        /// <summary>
        /// Подписка на клик кнопок
        /// </summary>
        private void InitClickEventButtons()
        {
            AddBtn.Click += (s, e) => AskToAddClient();
            EditBtn.Click += (s, e) => p.EditClient();
            CompareBtn.Click += (s, e) => p.SortClient();

            DeleteBtn1.Click += (s, e) => p.DeleteAccount();
            TransferBtn1.Click += (s, e) => p.TransferAccounts();
            AddMoneyBtn1.Click += (s, e) => p.AddToAccountMoney();
            SpendMoneyBtn1.Click += (s, e) => p.SpendAccountMoney();

            AddWithCapitalBtn2.Click += (s, e) => p.AddWithCapital();
            AddWithoutCapitalBtn2.Click += (s, e) => p.AddWithoutCapital();
            AddMoneyForYearBtn2.Click += (s, e) => p.AddMoneyForYear();

            TransferBetweenClientsCapitalBtn3.Click += (s, e) => p.TransferBetweenClientsCapital();
        }

        private void InitSelectedChangedCombobox()
        {
            ItemsDepartamentComboBox.SelectionChanged += (s, e) => IvWorkers.ItemsSource = p.worker.clients.Where(FindWorkers);

            ItemsDepartamentComboBox1.SelectionChanged += (s, e) => IvWorkers1.ItemsSource = p.worker.clients.Where(FindWorkers1);

            ItemsDepartamentComboBox2.SelectionChanged += (s, e) => IvWorkers1.ItemsSource = p.worker.clients.Where(FindWorkers2);

            ItemsDepartamentComboBox3.SelectionChanged += (s, e) => IvWorkers1.ItemsSource = p.worker.clients.Where(FindWorkers3);
            ItemsDepartament2ComboBox3.SelectionChanged += (s, e) => ItemsClientComboBox3.ItemsSource = p.worker.clients.Where(FindSecondWorkers3);
            ItemsClientComboBox3.SelectionChanged += (s, e) => ItemsAccountComboBox3.ItemsSource = p.GetClientAccounts(SecondSelectedClient3_IV);

        }

        private void InitSelectedChangedListView()
        {
            IvWorkers1.SelectionChanged += (s, e) => p.FindClientAccounts(SelectedClient1_IV);
        }

        /// <summary>
        /// Изменение вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                int index = TabControl.SelectedIndex;
                switch (index)
                {
                    case 0:
                        HideForClientTabItem();
                        break;
                    case 1:
                        HideForAccountTabItem();
                        IvWorkers1.ItemsSource = p.worker.clients.Where(FindWorkers1);
                        break;
                    case 2:
                        HideForAccountTabItem();
                        IvWorkers1.ItemsSource = p.worker.clients.Where(FindWorkers2);
                        break;
                    case 3:
                        HideForAccountTabItem();
                        IvWorkers1.ItemsSource = p.worker.clients.Where(FindWorkers3);
                        break;
                    case 4:
                        HideForActivityLogTabItem();
                        break;
                }

                IvAccounts1.ItemsSource = null;
            }
        }
        #endregion

        #region TODO - можно ли изменить? p.worker.clients.Where(FindWorkers2)

        private bool FindWorkers(Clients client)
        {
            return int.Parse(client.DepartametId) == SelectedDepartmentName_CB.DepartmentId;
        }
        private bool FindWorkers1(Clients client)
        {
            return int.Parse(client.DepartametId) == SelectedDepartmentName1_CB.DepartmentId;
        }
        private bool FindWorkers2(Clients client)
        {
            return int.Parse(client.DepartametId) == SelectedDepartmentName2_CB.DepartmentId;
        }
        private bool FindWorkers3(Clients client)
        {
            return int.Parse(client.DepartametId) == SelectedDepartmentName3_CB.DepartmentId;
        }
        private bool FindSecondWorkers3(Clients client)
        {
            return int.Parse(client.DepartametId) == SecondSelectedDepartmentName3_CB.DepartmentId;
        }


        #endregion

        #region Звуки

        private void PlayQuestionSound()
        {
            SystemSounds.Question.Play();
        }



        #endregion

        #region Остальное
        /// <summary>
        /// вопрос о добавление нового клиента
        /// </summary>
        public void AskToAddClient()
        {
            if (worker.GetType() == typeof(Manager))
            {
                MessageBoxResult rezult = MessageBox.Show("Вы точно хотите добавить клиента?",
                    "Вопрос",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (rezult == MessageBoxResult.Yes)
                {
                    MainWindow SetWindow = Window.GetWindow(this) as MainWindow;
                    SetWindow.ChangeFrame("AddPage");
                }
            }
            else if (worker.GetType() == typeof(Consultate))
            {
                MessageBoxResult rezult = MessageBox.Show("Недостаточно прав для добавления клиента",
                    "Вопрос",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void HideForClientTabItem()
        {
            IvWorkers.Visibility = Visibility.Visible;
            IvAccounts1.Visibility = Visibility.Collapsed;
            IvWorkers1.Visibility = Visibility.Collapsed;
        }

        private void HideForAccountTabItem()
        {
            IvWorkers.Visibility = Visibility.Collapsed;
            IvAccounts1.Visibility = Visibility.Visible;
            IvWorkers1.Visibility = Visibility.Visible;
        }

        private void HideForActivityLogTabItem()
        {
            IvWorkers.Visibility = Visibility.Collapsed;
            IvAccounts1.Visibility = Visibility.Collapsed;
            IvWorkers1.Visibility = Visibility.Collapsed;
        }

        #endregion
    }

}

