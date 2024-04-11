namespace RMS.Shared.Models
{
    /// <summary>
    /// Class representing necessary information while user log in.
    /// </summary>
    public class LoginModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
