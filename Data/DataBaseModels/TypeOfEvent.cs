using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class TypeOfEvent : BasePropertyChanged
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
