﻿using System.ComponentModel.DataAnnotations;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class Supply
    {
        public Supply()
        {
            this.PositionInSupply = new HashSet<PositionInSupply>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool CarriedOut { get; set; } = false;

        [Required]
        public bool Paid { get; set; } = false;

        [Required]
        public System.DateTime Date { get; set; } = DateTime.Now;

        public virtual ICollection<PositionInSupply> PositionInSupply { get; set; }
    }
}
