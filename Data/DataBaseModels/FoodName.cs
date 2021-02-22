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
    public partial class FoodName
    {
        public FoodName()
        {
            this.Food = new HashSet<Food>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool Visible { get; set; } = true;

        public virtual ICollection<Food> Food { get; set; }
    }
}
