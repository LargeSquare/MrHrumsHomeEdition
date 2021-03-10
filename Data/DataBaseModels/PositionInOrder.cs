using System.ComponentModel.DataAnnotations;
using System;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class PositionInOrder : BasePropertyChanged
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

        public void OnCountOfBagsChanged()
        {
            RecalcAmount();
        }
        public void OnCountOfKGChanged()
        {
            RecalcAmount();
        }
        public void OnDiscountChanged()
        {
            RecalcAmount();
        }
        public void OnTypeOfSaleChanged()
        {
            RecalcAmount();
        }
        public void OnIndividualPriceChanged()
        {
            RecalcAmount();
        }

        private void RecalcAmount()
        {
            if(TypeOfSale == null)
            {
                return;
            }
            decimal amountOfPosition = (AppModels.OrdersModel.ReturnPriceByTypeOfSale(this) * CountOfBags) +
                                    (AppModels.OrdersModel.ReturnPriceByTypeOfSale(this) / Convert.ToDecimal(Food.FoodWeight.Weight) * CountOfKG);
            decimal amountWithDiscount = amountOfPosition - (amountOfPosition / 100 * this.Discount);
            Amount = amountWithDiscount;
        }
    }
}
