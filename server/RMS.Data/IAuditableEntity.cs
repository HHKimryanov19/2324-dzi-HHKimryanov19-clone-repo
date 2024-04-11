namespace RMS.Data
{
    public interface IAuditableEntity
    {
        /// <summary>
        /// Id of user who created the entity.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Time when user created the entity.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Id of user who made last change.
        /// </summary>
        string UpdateBy { get; set; }

        /// <summary>
        /// Time when user made last change.
        /// </summary>
        DateTime UpdateOn { get; set; }
    }
}
