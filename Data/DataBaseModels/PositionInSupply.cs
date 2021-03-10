﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class PositionInSupply : BasePropertyChanged
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SupplyID { get; set; }

        [Required]
        public int FoodID { get; set; }

        [Required]
        public int CountOfBags { get; set; }

        [Required]
        public bool CarriedOut { get; set; } = false;

        [Required]
        public bool Paid { get; set; } = false;

        public virtual Food Food { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
