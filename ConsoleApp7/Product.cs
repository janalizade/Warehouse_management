using System;
using System.ComponentModel;

namespace StorageMaster
{
   public abstract  class Product
    {
        private double price;
       
        public double Price
        {
            get { return price; }
            set {
                price = value;
            }
        }
        public  double Weight { set; get; }
        //I use validation here for encapsulation
        public Product(double price, double weight){
            this.priceValidation(price);
            this.Price = price;
            this.Weight = weight;
            }
        private void priceValidation(double price)
        {
            if (price<0) throw new InvalidOperationException("price can not be negetive"); 
        }



    }
}