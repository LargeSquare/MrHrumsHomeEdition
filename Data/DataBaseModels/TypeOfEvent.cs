using System.ComponentModel.DataAnnotations;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class TypeOfEvent
    {
        public TypeOfEvent()
        {
            this.Event = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public bool IsSystem { get; set; } = false;

        [Required]
        public bool Visible { get; set; } = true;

        public virtual ICollection<Event> Event { get; set; }
    }
}
