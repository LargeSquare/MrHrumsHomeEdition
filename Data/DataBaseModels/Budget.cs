using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class Budget : BasePropertyChanged
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int BudgetStateID { get; set; }

        [Required]
        public System.DateTime Date { get; set; } = DateTime.Now;
        public string Note { get; set; }

        public virtual BudgetState BudgetState { get; set; }
    }
}
