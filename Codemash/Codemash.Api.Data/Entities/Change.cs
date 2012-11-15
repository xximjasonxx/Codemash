using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codemash.Api.Data.Entities
{
    public class Change : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChangeId { get; set; }

        [Required]
        public int Changeset { get; set; }

        [Required]
        public long EntityId { get; set; }

        [Required]
        public string EntityType { get; set; }

        [Required]
        public string Action { get; set; }

        [StringLength(50)]
        [Required]
        public string Key { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Value { get; set; }
    }
}
