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
    public partial class Order
    {
        public Order()
        {
            this.PositionInOrder = new HashSet<PositionInOrder>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientID { get; set; }

        [Required]
        public int TypeOfDeliveryID { get; set; }

        [Required]
        public decimal CostOfDelivery { get; set; } = 0;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool CarriedOut { get; set; } = false;

        [Required]
        public bool Paid { get; set; } = false;

        [Required]
        public bool DeliveryPaid { get; set; } = false;
        public string Note { get; set; }
        public System.DateTime Date { get; set; } = DateTime.Now;

        public virtual Client Client { get; set; }
        public virtual TypeOfDelivery TypeOfDelivery { get; set; }
        public virtual ICollection<PositionInOrder> PositionInOrder { get; set; }
    }
}
