using RMS.Shared.Models;

namespace RMS.Shared.Models.ResponseModels
{
    /// <summary>
    /// Represents a model for response of User entity.
    /// </summary>
    public class UserRM : BaseRMEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public Address Address { get; set; } = default!;
    }
}
