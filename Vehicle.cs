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
         public int Capacity { set; get; }
         public bool IsFull{ get 
            {
                double sum= products.Select(i=> Convert.ToInt32(i.Weight)).Sum();
                return  sum>=Capacity;

            }
        }
         public bool ISEMPTY { get { return this.products != null &&  this.products.Count==0; } }


        public List<Product> products = new List<Product>();
     
       
        public Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.products = new List<Product>();
          
            }


        public void LoadProduct(Product product)
        {
            this.products.Add(product);
            this.Trunk.Append(product);
            Console.WriteLine("The count of elements in the trunk is  "+this.products.Count);
       
           if (IsFull == true) throw new InvalidOperationException("Vehicle is full!");
           


        }

        public Product Unload() {
            if (this.ISEMPTY == true) { throw new InvalidOperationException("No products left in vehicle!"); } else
            {
                foreach (var item in products) 
                {
                    Console.WriteLine("Capacity" + products.Capacity);
                }
                Console.WriteLine("The last Element is Removed");
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
    
    

