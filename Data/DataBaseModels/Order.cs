using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrHrumsHomeEdition.OtherClasses;

namespace MrHrumsHomeEdition.Data.DataBaseModels
{
    public partial class Order : BasePropertyChanged
    {
        public delegate void OrderEventsHandler();
        public event OrderEventsHandler CarriedOutIsTrue;
        public event OrderEventsHandler CarriedOutIsFalse;
               
        public event OrderEventsHandler PaidIsTrue;
        public event OrderEventsHandler PaidIsFalse;
               
        public event OrderEventsHandler DeliveryPaidIsTrue;
        public event OrderEventsHandler DeliveryPaidIsFalse;
               
        public event OrderEventsHandler AllPositionsCarriedOutIsTrue;
        public event OrderEventsHandler OnePositionCarriedOutIsFalse;
               
        public event OrderEventsHandler AllPositionsPaidIsTrue;
        public event OrderEventsHandler OnePositionPaidIsFalse;
                      
        //public static event OrderEventsHandler AllPositionsReturnIsTrue;
        //public static event OrderEventsHandler OnePositionReturnIsFalse;


        public Order()
        {
            this.PositionInOrder = new HashSet<PositionInOrder>();


            //DataBaseModels.PositionInOrder.CarriedOutChanged += PositionInOrder_CarriedOutChanged;
            AllPositionsCarriedOutIsTrue += Order_AllPositionsCarriedOutIsTrue;
            OnePositionCarriedOutIsFalse += Order_OnePositionCarriedOutIsFalse;


            //DataBaseModels.PositionInOrder.PaidChanged += PositionInOrder_PaidChanged;
            AllPositionsPaidIsTrue += Order_AllPositionsPaidIsTrue;
            OnePositionPaidIsFalse += Order_OnePositionPaidIsFalse;

            CarriedOutIsTrue += Order_CarriedOutIsTrue;
            CarriedOutIsFalse += Order_CarriedOutIsFalse;
            PaidIsTrue += Order_PaidIsTrue;
            PaidIsFalse += Order_PaidIsFalse;
        }

        private void Order_PaidIsFalse()
        {
            if (GetCountOfFalsePaidInPosition() != 1)
            {
                foreach (PositionInOrder p in PositionInOrder)
                {
                    if (p.Paid)
                    {
                        p.Paid = false;
                    }
                }
            }
        }

        private void Order_PaidIsTrue()
        {
            foreach (PositionInOrder p in PositionInOrder)
            {
                if (!p.Paid)
                {
                    p.Paid = true;
                }
            }
        }

        private void Order_CarriedOutIsFalse()
        {
            if (GetCountOfFalseCarriedOutInPosition() != 1)
            {
                foreach (PositionInOrder p in PositionInOrder)
                {
                    p.CanSetCarriedOut = true;
                    if (p.CarriedOut)
                    {
                        p.CarriedOut = false;
                    }
                }
            }
        }

        private void Order_CarriedOutIsTrue()
        {
            foreach (PositionInOrder p in PositionInOrder)
            {
                if (!p.CarriedOut)
                {
                    p.CarriedOut = true;
                }
                p.CanSetCarriedOut = false;
            }
        }

        private void Order_OnePositionPaidIsFalse()
        {
            Paid = false;
        }

        private void Order_AllPositionsPaidIsTrue()
        {
            Paid = true;
        }

        internal void PositionInOrder_PaidChanged(PositionInOrder position)
        {
            switch (GetCountOfFalsePaidInPosition())
            {
                case 0:
                    AllPositionsPaidIsTrue?.Invoke();
                    break;

                case 1:
                    if (!position.Paid)
                    {
                        OnePositionPaidIsFalse?.Invoke();
                    }
                    break;
            }
        }

        private void Order_OnePositionCarriedOutIsFalse()
        {
            CarriedOut = false;
        }

        private void Order_AllPositionsCarriedOutIsTrue()
        {
            CarriedOut = true;
        }

        internal void PositionInOrder_CarriedOutChanged(PositionInOrder position)
        {
            switch (GetCountOfFalseCarriedOutInPosition())
            {
                case 0:
                    AllPositionsCarriedOutIsTrue?.Invoke();
                    break;

                case 1:
                    if (!position.CarriedOut)
                    {
                        OnePositionCarriedOutIsFalse?.Invoke();
                    }
                    break;
            }
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int ClientID { get; set; }

        [Required]
        public int TypeOfDeliveryID { get; set; }

        [Required]
        public decimal CostOfDelivery { get; set; } = 0;

        [Required]
        public decimal Amount { get; set; }


        bool carriedout = false;
        [Required]
        public bool CarriedOut
        {
            get
            {
                return carriedout;
            }
            set
            {
                if(!value) return;
                if (carriedout != value) 
                { 
                    carriedout = value; 
                    switch (value)
                    {
                        case true:
                            CarriedOutIsTrue?.Invoke();
                            break;
                        case false:
                            CarriedOutIsFalse?.Invoke();
                            break;
                    }
                }
            }
        }


        bool paid = false;
        [Required]
        public bool Paid
        {
            get
            {
                return paid;
            }
            set
            {
                if (paid != value)
                {
                    paid = value;
                    switch (value)
                    {
                        case true:
                            PaidIsTrue?.Invoke();
                            break;
                        case false:
                            PaidIsFalse?.Invoke();
                            break;
                    }
                }
            }
        }


        bool deliverypaid = false;
        [Required]
        public bool DeliveryPaid
        {
            get
            {
                return deliverypaid;
            }
            set
            {
                if (deliverypaid != value) deliverypaid = value;


                switch (value)
                {
                    case true:
                        DeliveryPaidIsTrue?.Invoke();
                        break;
                    case false:
                        DeliveryPaidIsFalse?.Invoke();
                        break;
                }
            }
        }


        public string Note { get; set; }
        public System.DateTime Date { get; set; } = DateTime.Now;

        public virtual Client Client { get; set; }
        public virtual TypeOfDelivery TypeOfDelivery { get; set; }
        public virtual ICollection<PositionInOrder> PositionInOrder { get; set; }

        public void OnPositionInOrderChanged()
        {
            RecalcAmount();
        }

        public void RecalcAmount()
        {
            decimal sum = 0;
            foreach(PositionInOrder p in PositionInOrder)
            {
                sum += p.Amount;
            }
            Amount = sum;
        }



        public int GetCountOfFalseCarriedOutInPosition()
        {
            int count = 0;

            foreach (PositionInOrder p in PositionInOrder)
            {
                if (!p.CarriedOut)
                {
                    count++;
                }
            }

            return count;
        }

        public int GetCountOfFalsePaidInPosition()
        {
            int count = 0;

            foreach (PositionInOrder p in PositionInOrder)
            {
                if (!p.Paid)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
