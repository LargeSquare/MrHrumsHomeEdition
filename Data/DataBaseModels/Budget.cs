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
    public partial class Budget
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
