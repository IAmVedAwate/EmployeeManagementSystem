namespace EmployeeManagement.Models
{
    public class EmployeeEditModelView : EmployeeModelView
    {
        public int Id { get; set; }
        public string? ExistingPhotoPath { get; set; }
    }
}
