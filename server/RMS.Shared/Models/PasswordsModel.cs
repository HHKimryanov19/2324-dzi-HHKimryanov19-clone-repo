namespace RMS.Shared.Models
{
    /// <summary>
    /// Represents a model for managing password, including old and new password.
    /// </summary>
    public class PasswordsModel
    {
        public string OldPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
