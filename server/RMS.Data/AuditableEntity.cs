namespace RMS.Data
{
    /// <summary>
    /// Abstract class contains properties for tracking changes and more.
    /// </summary>
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        /// <inheritdoc/>
        public string CreatedBy { get; set; } = default!;

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string UpdateBy { get; set; } = default!;

        /// <inheritdoc/>
        public DateTime UpdateOn { get; set; }
    }
}
