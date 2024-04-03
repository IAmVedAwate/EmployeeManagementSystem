using EmployeeManagement.Data;

namespace EmployeeManagement.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Employee AddInDb(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
            return employee;
        }
        public Employee UpdateInDb(Employee employee)
        {
            _db.Employees.Update(employee);
            _db.SaveChanges();
            return employee;
        }
        public Employee DeleteInDb(Employee employee)
        {
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return employee;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _db.Employees;
        }

        public Employee GetById(int id)
        {
            return _db.Employees.Find(id);
        }
    }
}
