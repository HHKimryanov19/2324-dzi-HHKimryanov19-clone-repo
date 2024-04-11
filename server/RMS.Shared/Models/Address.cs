namespace RMS.Shared.Models
{
    /// <summary>
    /// Class representing ValueObject in other classes or tables.
    /// </summary>
    public class Address
    {
        public string? Country { get; set; } = default!;

        public string? City { get; set; } = default!;

        public string? Street { get; set; } = default!;

        public string? Number { get; set; } = default!;
    }
}
