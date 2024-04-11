using RMS.Shared.Models;

namespace RMS.Shared.Models.InputModels
{
    /// <summary>
    /// Represents a model for adding or updating User entity.
    /// </summary>
    public class UserIM
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public Address Address { get; set; } = default!;
    }
}
