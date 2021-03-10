using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class TypeOfSale : BasePropertyChanged
    {
        public TypeOfSale()
        {
            this.PositionInOrder = new HashSet<PositionInOrder>();
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

        public virtual ICollection<PositionInOrder> PositionInOrder { get; set; }
    }
}
