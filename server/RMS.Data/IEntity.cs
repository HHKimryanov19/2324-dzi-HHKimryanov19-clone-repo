namespace RMS.Data
{
    public interface IEntity
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        Guid Id { get; set; }
    }
}