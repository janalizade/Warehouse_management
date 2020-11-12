using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster
{
    public abstract class Storage
    {
 
     
     public string Name { set; get; }
     public int Capacity { set { Capacity = value; } get 
            {
                int max = products.Select(i => Convert.ToInt32(i.Weight)).Max();
                return max;


            }
        }
     public int GarageSlots
        {
            set { GarageSlots = value; }
            get
            {
                int sum = products.Select(i => Convert.ToInt32(i.Weight)).Sum();
                return sum;
            }
        }
  
     public bool IsFull { get; }
        private readonly List<Vehicle> garage = new List<Vehicle>(); 
     private IReadOnlyCollection<Vehicle> Garage { get { return garage; } }
      
        private readonly List<Product> products = new List<Product>();

       
        public virtual IReadOnlyCollection<Product> Products
        {
            get
            {
                return products;
            }
        }
       
        public void AddWorkItem(Product product)
        {
            products.Add(product);
        }

        public Storage(string name, int capacity, int garageSlots)
            //, IEnumerable<Vehicle> vehicles) 
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
          //  this.vehi


        
        }
    
    }



}

