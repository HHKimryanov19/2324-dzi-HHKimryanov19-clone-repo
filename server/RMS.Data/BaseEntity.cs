using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMS.Data
{
    /// <summary>
    /// Abstract class for base properties of entity.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <inheritdoc/>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
    }
}
