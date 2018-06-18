using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Contact
    {
        private string _lastName;
        private string _firstName;
        private long _id;
        private long _phoneNumber;
        private string _email;
        public long Id { get => _id; set => _id = value; }
        [Required]
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string Email { get => _email; set => _email = value; }
        [Phone]
        public long PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public Status Status { get => status; set => status = value; }
        private Status status;
    }

    public enum Status
    {
        Active,
        Inactive
    }
}