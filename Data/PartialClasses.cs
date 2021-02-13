using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace MrHrumsHomeEdition.Data
{
    class PartialClasses
    {
        [AddINotifyPropertyChangedInterface]
        public partial class Bags { }


        [AddINotifyPropertyChangedInterface]
        public partial class Budget { }


        [AddINotifyPropertyChangedInterface]
        public partial class BudgetState { }


        [AddINotifyPropertyChangedInterface]
        public partial class Client { }


        [AddINotifyPropertyChangedInterface]
        public partial class Event { }


        [AddINotifyPropertyChangedInterface]
        public partial class Food { }


        [AddINotifyPropertyChangedInterface]
        public partial class FoodName { }


        [AddINotifyPropertyChangedInterface]
        public partial class FoodWeight { }


        [AddINotifyPropertyChangedInterface]
        public partial class Granule { }


        [AddINotifyPropertyChangedInterface]
        public partial class KG { }


        [AddINotifyPropertyChangedInterface]
        public partial class Order { }


        [AddINotifyPropertyChangedInterface]
        public partial class PositionInOrder { }


        [AddINotifyPropertyChangedInterface]
        public partial class PositionInSupply { }


        [AddINotifyPropertyChangedInterface]
        public partial class Price { }


        [AddINotifyPropertyChangedInterface]
        public partial class Supply { }


        [AddINotifyPropertyChangedInterface]
        public partial class TypeOfBudgetAction { }


        [AddINotifyPropertyChangedInterface]
        public partial class TypeOfDelivery { }


        [AddINotifyPropertyChangedInterface]
        public partial class TypeOfEvent { }


        [AddINotifyPropertyChangedInterface]
        public partial class TypeOfSale { }


        [AddINotifyPropertyChangedInterface]
        public partial class Warehouse { }
    }
}
