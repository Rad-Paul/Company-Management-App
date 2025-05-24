using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace CompanyManagementAPI.DTOs
{
    [DataContract]
    public record CompanyDto
    {
        [DataMember]
        public Guid Id { get; init; }

        [DataMember]
        public string? Name { get; init; }

        [DataMember]
        public string? FullAddress { get; init; }
    }

    [Serializable]
    public record EmployeeDto(Guid Id, string Name, int Age, string Position);
    public record CompanyForCreationDto : CompanyForManipulationDto;
    public record CompanyForUpdateDto : CompanyForManipulationDto;
    public record EmployeeForCreationDto : EmployeeForManipulationDto;
    public record EmployeeForUpdateDto : EmployeeForManipulationDto;

    public abstract record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }

        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lowerthan 18")]
        public int Age { get; init; }

        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string? Position { get; init; }
    }

    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company name is required.")]
        [MaxLength(60, ErrorMessage = "Maximum length for company name is 60 characters.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "Company address is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for company address is 80 characters.")]
        public required string Address { get; init; }

        [Required(ErrorMessage = "Country of company is required.")]
        [MaxLength(20, ErrorMessage = "Maximum length for country is 20 characters.")]
        public required string Country { get; init; }

        public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
    }

    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }
    }

    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; init; }
    }
}
