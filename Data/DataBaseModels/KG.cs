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
    public partial class KG
    {
        public KG()
        {
            this.Warehouse = new HashSet<Warehouse>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; } = 0;

        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
