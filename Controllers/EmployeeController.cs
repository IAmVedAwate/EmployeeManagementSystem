using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee EditableData = _employeeRepository.GetById(id);
            EmployeeEditModelView model = new EmployeeEditModelView()
            {
                Id = id,
                Name = EditableData.Name,
                Email = EditableData.Email,
                Department = EditableData.Department,
                ExistingPhotoPath = EditableData.PhotoPath
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditModelView obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            string uniqueFileName = ProcessUploadedFile(obj);

            Employee newEmployee = new Employee()
            {
                Id = obj.Id,
                Name = obj.Name,
                Email = obj.Email,
                Department = obj.Department,
                PhotoPath = obj.ExistingPhotoPath
            };
            if (obj.Photo != null)
            {
                if (obj.ExistingPhotoPath != null)
                {
                    System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "img", obj.ExistingPhotoPath));
                }
                newEmployee.PhotoPath = uniqueFileName;
            }
            _employeeRepository.UpdateInDb(newEmployee);
            return RedirectToAction("Details", new { id = obj.Id });
        }

        private string ProcessUploadedFile(EmployeeModelView obj)
        {
            string uniqueFileName = null;
            string filePath = null;

            if (obj.Photo != null)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.Photo.FileName;
                filePath = Path.Combine(uploadPath, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Photo.CopyTo(filestream);
                }
            }

            return uniqueFileName;
        }
        [HttpGet]
        public IActionResult DeleteBtn(int id)
        {
            Employee DeletableData = _employeeRepository.GetById(id);
            EmployeeEditModelView model = new EmployeeEditModelView()
            {
                Id = id,
                Name = DeletableData.Name,
                Email = DeletableData.Email,
                Department = DeletableData.Department,
                ExistingPhotoPath = DeletableData.PhotoPath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteBtn(EmployeeEditModelView obj)
        {

            Employee newEmployee = _employeeRepository.GetById(obj.Id);
            _employeeRepository.DeleteInDb(newEmployee);
            return RedirectToAction("EmployeeDetails");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModelView obj)
        {


            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            string uniqueFileName = ProcessUploadedFile(obj);

            Employee newEmployee = new Employee
            {
                Name = obj.Name,
                Email = obj.Email,
                Department = obj.Department,
                PhotoPath = uniqueFileName
            };
            _employeeRepository.AddInDb(newEmployee);
            return RedirectToAction("Details", new { id = newEmployee.Id });
        }

        [HttpGet]
        public IActionResult EmployeeDetails()
        {
            IEnumerable<Employee> AllData = _employeeRepository.GetAll();
            return View(AllData);
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            Employee DataById = _employeeRepository.GetById(id);

            if (DataById == null)
            {
                return View("NotFound", id);
            }

            return View(DataById);
        }

    }
}
