using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class Food : BasePropertyChanged
    {
        public Food()
        {
            this.PositionInOrder = new HashSet<PositionInOrder>();
            this.PositionInSupply = new HashSet<PositionInSupply>();
            this.Warehouse = new HashSet<Warehouse>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int FoodNameID { get; set; }

        [Required]
        public int FoodWeightID { get; set; }

        [Required]
        public int GranuleID { get; set; }

        [Required]
        public int PriceID { get; set; }

        [Required]
        public bool Visible { get; set; } = true;

        public virtual FoodName FoodName { get; set; }
        public virtual FoodWeight FoodWeight { get; set; }
        public virtual Granule Granule { get; set; }
        public virtual Price Price { get; set; }
        public virtual ICollection<PositionInOrder> PositionInOrder { get; set; }
        public virtual ICollection<PositionInSupply> PositionInSupply { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
