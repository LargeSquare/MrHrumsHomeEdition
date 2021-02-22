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
    public partial class Price
    {
        public Price()
        {
            this.Food = new HashSet<Food>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Retail { get; set; } = 0;

        [Required]
        public decimal Kennel { get; set; } = 0;

        [Required]
        public decimal Purchase { get; set; } = 0;

        [Required]
        public decimal ForSelf { get; set; } = 0;

        [Required]
        public bool Visible { get; set; } = false;

        [Required]
        public System.DateTime Date { get; set; } = DateTime.Now;

        public virtual ICollection<Food> Food { get; set; }
    }
}
