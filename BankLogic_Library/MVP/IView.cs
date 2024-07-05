using System.Collections.Generic;
using BankAccount_Library.account;
using BankAccount_Library.deposit;

namespace Lesson10.MVP
{
    public interface IView
    {
        /// Вкладка клиент
        Department SelectedDepartmentName_CB { get;  }
        string SelectedItem_CB { get;  }
        int SelectedItemIndex_CM { get;  }
        string ChangedClientData_TB { get;  }
        IEnumerable<Client> ClientData_IV { get; set; }
        Client SelectedClient_IV { get; }

        /// Вкладка счета
        Department SelectedDepartmentName1_CB { get; }
        IEnumerable<Client> ClientData1_IV { get; set; }
        Client SelectedClient1_IV { get; }
        IEnumerable<ICapital<BankAccount>> AccountsData_IV { get; set; }
        ICapital<BankAccount> SelectedAccount_IV { get; }
        IEnumerable<ICapital<BankAccount>> SecondAccountData_CB { get; set; }
        ICapital<BankAccount> SecondSelectedAccount_CB { get; }
        string Rubles_TB { get;  }

        /// Вкладка депозит
        Department SelectedDepartmentName2_CB { get ; }
        string Rubles2_TB { get; }


        // вкладка переводы
        Department SelectedDepartmentName3_CB{ get; }
        Client SelectedClient3_IV{ get; }
        ICapital<BankAccount> SelectedAccount3_IV{ get; }

        Department SecondSelectedDepartmentName3_CB{ get; }
        Client SecondSelectedClient3_IV{ get; }
        ICapital<BankAccount> SecondSelectedAccount3_CB { get; }
        string Rubles3_TB { get; }


    }
}
