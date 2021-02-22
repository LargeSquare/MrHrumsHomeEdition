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
    public partial class PositionInOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int FoodID { get; set; }

        [Required]
        public int TypeOfSaleID { get; set; }

        [Required]
        public int CountOfBags { get; set; } = 0;

        [Required]
        public int CountOfKG { get; set; } = 0;
        public decimal IndividualPrice { get; set; }

        [Required]
        public decimal Discount { get; set; } = 0;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool CarriedOut { get; set; } = false;

        [Required]
        public bool Paid { get; set; } = false;

        [Required]
        public bool Returned { get; set; } = false;

        [Required]
        public int ReturnedKG { get; set; } = 0;

        public virtual Food Food { get; set; }
        public virtual Order Order { get; set; }
        public virtual TypeOfSale TypeOfSale { get; set; }
    }
}
