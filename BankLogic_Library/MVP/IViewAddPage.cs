
namespace Lesson10.MVP
{
    public interface IViewAddPage
    {
        Department SelectedDepartmentName_CB { get;  }
        string ClientName_TB { get; }
        string ClientSurname_TB { get; }
        string ClientMiddlename_TB { get; }
        string ClientPhone_TB { get; }
        string ClientPasport_TB { get; }

    }
}
