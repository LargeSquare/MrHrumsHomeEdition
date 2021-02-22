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
    public partial class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TypeOfEventID { get; set; }

        [Required]
        public System.DateTime Date { get; set; } = DateTime.Now;
        public string Message { get; set; }

        public virtual TypeOfEvent TypeOfEvent { get; set; }
    }
}
