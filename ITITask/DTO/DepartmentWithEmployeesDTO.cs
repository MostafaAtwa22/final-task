using ITITask.Models;

namespace ITITask.DTO
{
    public class DepartmentWithEmployeesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ManagerName { get; set; }
        public List<string> Employees { get; set; } = new List<string>();
    }
}
