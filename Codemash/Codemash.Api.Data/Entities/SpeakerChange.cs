using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Codemash.Api.Data.Entities.Interfaces;

namespace Codemash.Api.Data.Entities
{
    public class SpeakerChange : EntityBase, IChange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpeakerChangeId { get; set; }

        [Required]
        public int SpeakerId { get; set; }

        #region Implementation of IChange

        public int ChangeEntityId { set { SpeakerId = value; } }

        [Required]
        public ChangeAction Action { get; set; }

        [StringLength(50)]
        [Required]
        public string Key { set; get; }

        [Required]
        public string Value { set; get; }

        #endregion
    }
}
