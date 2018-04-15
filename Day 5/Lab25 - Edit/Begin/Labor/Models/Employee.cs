using System.ComponentModel.DataAnnotations;

namespace Labor.Models
{
    public class Employee
    {
        [Key] public int EmployeeId { get; set; }

        [FirstNameValidation] public string FirstName { get; set; }

        [StringLength(5, ErrorMessage = "Last Name leght should br greater than 5")]
        public string LastName { get; set; }

        [Range(typeof(int), "5000", "50000", ErrorMessage = "Put a proper Salary value between 5000 and 50000")]
        public int Salary { get; set; }
    }

    public class FirstNameValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Please Provide First Name");

            if (value.ToString().Contains("@")) return new ValidationResult("First Name should not contain @");
            return ValidationResult.Success;
        }
    }
}