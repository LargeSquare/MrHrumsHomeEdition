using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class Warehouse : BasePropertyChanged
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FoodID { get; set; }

        [Required]
        public int BagsID { get; set; }

        [Required]
        public int KgID { get; set; }

        public virtual Bag Bags { get; set; }
        public virtual Food Food { get; set; }
        public virtual KG KG { get; set; }
    }
}
