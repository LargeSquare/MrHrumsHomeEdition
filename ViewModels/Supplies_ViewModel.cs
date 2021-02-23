using MrHrumsHomeEdition.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;
using System.Collections.ObjectModel;
using MrHrumsHomeEdition.Data.DataBaseModels;
using System.Windows.Controls;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Supplies_ViewModel
    {
        public ObservableCollection<Supply> Supplies => OtherClasses.Models.SuppliesModel.Supplies;
        public ObservableCollection<PositionInSupply> PositionInSupplies => OtherClasses.Models.SuppliesModel.PositionInSupplies;

        public Supply SelectedSupply { get; set; }
        public Supply SelectedPositionInSupply { get; set; }



        public Supplies_ViewModel()
        {
            
        }
    }
}
