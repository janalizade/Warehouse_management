using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace StorageMaster
{

    public abstract class Vehicle
    {
        
        private IReadOnlyCollection<Product> Trunk { set; get; }
        public List<Product> products = new List<Product>();

        public int Capacity { set; get; }
        //I used LINQ to calculate Isfull property, Returns true if the sum of the products’ weights is equal to or larger than the vehicle capacity
        public bool IsFull
        {
            get
            {
                double sum = products.Select(i => Convert.ToInt32(i.Weight)).Sum();
                return sum >= Capacity;

            }
        }
        //check ISEMPTY ,Returns true if the vehicle doesn’t have any products in the trunk
        public bool ISEMPTY { get { return this.products != null && this.products.Count == 0; } }


        

        public Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.products = new List<Product>();

        }


        public void LoadProduct(Product product)
        {
            //the product is added to the vehicle’s trunk
            this.products.Add(product);
            this.Trunk.Append(product);
            Console.WriteLine("The count of elements in the trunk is  " + this.products.Count);
            
            //vehicle is already full
            if (IsFull == true) throw new InvalidOperationException("Vehicle is full!");

        }

        public Product Unload()
        {
            if (this.ISEMPTY == true) { throw new InvalidOperationException("No products left in vehicle!"); }
            else
            {
                foreach (var item in products)
                {
                    Console.WriteLine("Capacity" + products.Capacity);
                }
                Console.WriteLine("The last Element is Removed");

                //the last product in the trunk is removed from the vehicle’s trunk and returned to the caller
                int index = products.Count() - 1;
                Product p = products.ElementAt(index);
                products.RemoveAt(index);
                foreach (var item in products)
                {
                    Trunk.Append(item);
                }

                return p;
            }


        }

    }


}



