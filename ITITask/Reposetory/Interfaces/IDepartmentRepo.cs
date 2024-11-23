using ITITask.DTO;
using ITITask.Models;

namespace ITITask.Reposetory.Interfaces
{
    public interface IDepartmentRepo
    {
        List<DepartmentWithEmployeesDTO> GetAll();
        DepartmentWithEmployeesDTO GetById(int id);
        void Insert(Department department);
        bool Update(int id, Department department); 
        bool Delete(int id);
    }
}
