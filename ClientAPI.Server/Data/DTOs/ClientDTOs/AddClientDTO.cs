namespace ClientAPI.Server.Data.DTOs.ClientDTOs
{
    public class AddClientDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Middle name is required.")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Initial deposit is required.")]
        [Range(1000, double.MaxValue, ErrorMessage = "Minimum deposit is 1000.00")]
        public decimal InitialDeposit { get; set; }
    }
}
