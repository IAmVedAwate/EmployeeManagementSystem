namespace EmployeeManagement.Models
{
    public interface IEmployeeRepository
    {
        public Employee GetById(int id);
        public IEnumerable<Employee> GetAll();
        public Employee AddInDb(Employee employee);
        public Employee UpdateInDb(Employee newEmployee);
        public Employee DeleteInDb(Employee employee); 
    }
}
