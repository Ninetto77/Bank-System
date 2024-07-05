namespace Lesson10
{
    public class Department
    {
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
 
        public Department(string name, int id)
        {
            DepartmentName = name;
            DepartmentId = id;
        }
    }
}
