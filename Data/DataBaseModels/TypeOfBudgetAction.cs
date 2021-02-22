using System.ComponentModel.DataAnnotations;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class TypeOfBudgetAction
    {
        public TypeOfBudgetAction()
        {
            this.BudgetState = new HashSet<BudgetState>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public bool IsSystem { get; set; } = false;

        public virtual ICollection<BudgetState> BudgetState { get; set; }
    }
}
