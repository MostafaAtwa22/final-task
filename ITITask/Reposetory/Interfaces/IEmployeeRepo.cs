using ITITask.DTO;
using ITITask.Models;

namespace ITITask.Reposetory.Interfaces
{
    public interface IEmployeeRepo
    {
        List<EmployeeWithDepartmentDTO> GetAll();
        EmployeeWithDepartmentDTO GetById(int id);
        EmployeeWithDepartmentDTO GetByName(string name);
        void Insert(Employee employee);
        bool Update(int id, Employee employee);
        bool Delete(int id);
    }
}
