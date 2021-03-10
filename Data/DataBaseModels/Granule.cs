using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class Granule : BasePropertyChanged
    {
        public Granule()
        {
            this.Food = new HashSet<Food>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Size { get; set; }

        [Required]
        public bool Visible { get; set; } = true;

        public virtual ICollection<Food> Food { get; set; }
    }
}
