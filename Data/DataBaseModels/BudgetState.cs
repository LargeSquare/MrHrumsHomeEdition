using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class BudgetState : BasePropertyChanged
    {
        public BudgetState()
        {
            this.Budget = new HashSet<Budget>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int TypeOfBudgetActionID { get; set; }

        [Required]
        public bool Visible { get; set; } = true;

        [Required]
        public bool IsSystem { get; set; } = false;

        public virtual ICollection<Budget> Budget { get; set; }
        public virtual TypeOfBudgetAction TypeOfBudgetAction { get; set; }
    }
}
