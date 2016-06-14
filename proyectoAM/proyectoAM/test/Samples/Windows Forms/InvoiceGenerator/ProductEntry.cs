using System;
using System.ComponentModel;

namespace InvoiceGenerator
{   
    /// <summary>
    /// Base product entry which can be used to implement your own product logic.
    /// </summary>
    abstract class ProductEntry:INotifyPropertyChanged
    {
        #region Fields

        private int _pos;
        private string _description;
        private int _qty;
        private decimal _price;
        private decimal _discount;
        private decimal _total;

        #endregion

        #region Properties

        /// <summary>
        /// Position in list.
        /// </summary>
        public int Pos
        {
            get { return _pos; }
            set
            {
                _pos = value; 
                RaisePropertyChanged("Pos");
            }
        }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        /// <summary>
        /// Quantity.
        /// </summary>
        public int Qty
        {
            get { return _qty; }
            set
            {
                _qty = value;
                RaisePropertyChanged("Qty");

                RecalculateTotal();
            }
        }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged("Price");

                RecalculateTotal();
            }
        }

        /// <summary>
        /// Discount if any.
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set
            {
                // check boundaries
                if (value < 0 || value > 100)
                {
                    value = 0;
                }

                _discount = value;
                RaisePropertyChanged("Discount");

                RecalculateTotal();
            }
        }      

        /// <summary>
        /// Total, calculated field.
        /// </summary>
        public decimal Total
        {
            get { return _total; }          
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Ctor

        protected ProductEntry()
        {
        }

        #endregion

        #region Members
        
        /// <summary>
        /// Recalculates the total value which can't be set directly via property.
        /// </summary>
        protected virtual void RecalculateTotal()
        {
            Decimal total = Price * Qty;

            _total = total - (total * Discount / (decimal)100);

            RaisePropertyChanged("Total");
        }
        #endregion
    }
}
