using ITITask.DTO;
using ITITask.Models;
using ITITask.Reposetory.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITITask.Reposetory
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ITIContext context;

        public EmployeeRepo(ITIContext iTIContext)
        {
            this.context = iTIContext;
        }

        public bool Delete(int id)
        {
            var emp = context.Employees.FirstOrDefault(e => e.Id == id);
            if(emp is null)
                return false;
            try
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public List<EmployeeWithDepartmentDTO> GetAll()
        {
            List<EmployeeWithDepartmentDTO> emps = new List<EmployeeWithDepartmentDTO>();
            foreach (var item in context.Employees.Include(e => e.Department))
            {
                EmployeeWithDepartmentDTO emp = new EmployeeWithDepartmentDTO();
                emp.Id = item.Id;
                emp.Name = item.Name;
                emp.Address = item.Address;
                emp.Salary = item.Salary;
                emp.Phone = item.Phone;
                emp.DepartmentName = item.Department.Name;
                emps.Add(emp);
            }
            return emps;
        }

        public EmployeeWithDepartmentDTO GetById(int id)
        {
            var item = context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);
            if (item is null)
                return null;

            EmployeeWithDepartmentDTO emp = new EmployeeWithDepartmentDTO();
            emp.Id = item.Id;
            emp.Name = item.Name;
            emp.Address = item.Address;
            emp.Salary = item.Salary;
            emp.Phone = item.Phone;
            emp.DepartmentName = item?.Department?.Name;

            return emp;
        }

        public EmployeeWithDepartmentDTO GetByName(string name)
        {
            var item = context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Name == name);

            if (item is null)
                return null;

            EmployeeWithDepartmentDTO emp = new EmployeeWithDepartmentDTO();
            emp.Id = item.Id;
            emp.Name = item.Name;
            emp.Address = item.Address;
            emp.Salary = item.Salary;
            emp.Phone = item.Phone;
            emp.DepartmentName = item?.Department?.Name;

            return emp;
        }

        public void Insert(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public bool Update(int id, Employee employee)
        {
            var old = context.Employees.FirstOrDefault(e => e.Id == id);

            if (old is null)
                return false;
            old.Name = employee.Name;
            old.Address = employee.Address;
            old.Salary = employee.Salary;
            old.Phone = employee.Phone;

            context.SaveChanges();
            return true;
        }
    }
}
