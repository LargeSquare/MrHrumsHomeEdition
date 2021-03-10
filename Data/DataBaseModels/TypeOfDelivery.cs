using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class TypeOfDelivery : BasePropertyChanged
    {
        public TypeOfDelivery()
        {
            this.Order = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public bool Visible { get; set; } = true;

        [Required]
        public bool IsSystem { get; set; } = false;

        public virtual ICollection<Order> Order { get; set; }
    }
}
