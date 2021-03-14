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
using System.ComponentModel.DataAnnotations.Schema;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class PositionInOrder : BasePropertyChanged
    {
        public delegate void ValueChangedHandler(PositionInOrder position);
        public event ValueChangedHandler CarriedOutChanged;
        public event ValueChangedHandler PaidChanged;
        public event ValueChangedHandler ReturnedChanged;

        [NotMapped]
        internal bool CanSetCarriedOut { get; set; } = true;

        [NotMapped]
        internal bool CanSetPaid { get; set; } = true;



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
        public decimal IndividualPrice { get; set; } = 0;

        [Required]
        public decimal Discount { get; set; } = 0;

        [Required]
        public decimal Amount { get; set; }


        private bool carriedout = false;
        [Required]
        public bool CarriedOut
        {
            get
            {
                return carriedout;
            }
            set
            {
                if (!CanSetCarriedOut) value = !value;
                if (carriedout != value) carriedout = value;
            }
        }

        bool paid = false;
        [Required]
        public bool Paid
        {
            get
            {
                return paid;
            }
            set
            {
                if (!CanSetPaid) value = !value;
                if (paid != value) paid = value;
            }
        }

        [Required]
        public bool Returned { get; set; } = false;

        [Required]
        public int ReturnedKG { get; set; } = 0;

        public virtual Food Food { get; set; }

        Order order;
        public virtual Order Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
                if (order != null)
                {
                    CarriedOutChanged += order.PositionInOrder_CarriedOutChanged;
                    PaidChanged += order.PositionInOrder_PaidChanged;
                }
            }
        }
        public virtual TypeOfSale TypeOfSale { get; set; }


        public void OnCarriedOutChanged()
        {
            CarriedOutChanged?.Invoke(this);
        }
        public void OnPaidChanged()
        {
            PaidChanged?.Invoke(this);
        }
        public void OnReturnedChanged()
        {
            ReturnedChanged?.Invoke(this);
        }


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
            decimal amountOfPosition = (GetPrice() * CountOfBags) +
                                       (GetPrice() / Convert.ToDecimal(Food.FoodWeight.Weight) * CountOfKG);
            decimal amountWithDiscount = amountOfPosition - (amountOfPosition / 100 * this.Discount);
            Amount = amountWithDiscount;
        }

        public decimal GetPrice()
        {
            switch (TypeOfSale.Type)
            {
                case "Розница":
                    return Food.Price.Retail;
                case "Питомник":
                    return Food.Price.Kennel;
                case "Индивидуально":
                    return IndividualPrice;
                case "Для себя":
                    return Food.Price.ForSelf;
                default:
                    MessageBox.Show("Указанный тип продажи отсутствует в прайсе! Используется значение по умолчанию - 0", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
            }
        }
    }
}
