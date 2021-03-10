﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class Client : BasePropertyChanged
    {
        public Client()
        {
            this.Order = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Number { get; set; }
        public string Note { get; set; }

        [Required]
        public bool Visible { get; set; } = true;

        public virtual ICollection<Order> Order { get; set; }
    }
}
