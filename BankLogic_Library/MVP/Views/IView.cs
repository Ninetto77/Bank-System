using System.Collections.Generic;
using BankAccount_Library.account;
using BankAccount_Library.deposit;
using BankLogic_Library.DB;


namespace Lesson10.MVP
{
    public interface IView
    {
        /// Вкладка клиент
        Department SelectedDepartmentName_CB { get;  }
        string SelectedItem_CB { get;  }
        int SelectedItemIndex_CM { get;  }
        string ChangedClientData_TB { get;  }
        IEnumerable<Clients> ClientData_IV { get; set; }
        Clients SelectedClient_IV { get; }

        /// Вкладка счета
        Department SelectedDepartmentName1_CB { get; }
        IEnumerable<Clients> ClientData1_IV { get; set; }
        Clients SelectedClient1_IV { get; }
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
        Clients SelectedClient3_IV{ get; }
        ICapital<BankAccount> SelectedAccount3_IV{ get; }

        Department SecondSelectedDepartmentName3_CB{ get; }
        Clients SecondSelectedClient3_IV{ get; }
        ICapital<BankAccount> SecondSelectedAccount3_CB { get; }
        string Rubles3_TB { get; }


    }
}
