using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster
{
    public abstract class Storage
    {

        private int capacity;
        private int garageslots;
        private Vehicle vehicle;
        public readonly List<Vehicle> garage = new List<Vehicle>();
        private IReadOnlyCollection<Vehicle> Garage { get { return vehicles; } }
        private readonly List<Product> products = new List<Product>();
        private readonly List<Vehicle> vehicles = new List<Vehicle>();
        public string Name { set; get; }
        //I used LINQ to calculate the maximum weight of products the storage can handle
        public int Capacity
        {
            set { capacity = value; }
            get
            {
                int max = products.Select(i => Convert.ToInt32(i.Weight)).Max();
                return max;
            }

        }

        //the number of garage slots 
        public int GarageSlots
        {
            set; 
            get;

        }
        //	I used LINQ to calculate sum of the products’ weights,it return true if  is equal to or larger than the storage capacity
        public bool IsFull
        {
            get
            {
                double sum = products.Select(i => Convert.ToInt32(i.Weight)).Sum();
                return sum >= this.Capacity;

            }
        }

        public virtual IReadOnlyCollection<Product> Products
        {
            get
            {
                return products;
            }
        }
        public Vehicle GetVehicle(int garageSlot)
        {
            //if garage slot number is equal to or larger than the garage slots
            if (garageSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }
            //If the garage slot is empty
            else if (Garage.ElementAt(garageSlot) == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }
            //retrieved vehicle
            return Garage.ElementAt(garageSlot);
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            //Gets the vehicle from the specified garage slot 
            var sentVehicle = GetVehicle(garageSlot);
            //Find a free garage slot
            var FreeSlotInGarage = deliveryLocation.Garage.Any(p => p == null);
            //there is no free garage slot
            if (FreeSlotInGarage == false)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            //returns the garage slot the vehicle was assigned when it was transferred
            garage[garageSlot] = null;
            int FreedSlot = Convert.ToInt32(garage[garageSlot]);
            return FreedSlot;


        }
        public int UnloadVehicle(int garageSlot)
        {
            //storage is full
            if (IsFull) { throw new InvalidOperationException("Storage is full!"); }
            else
            {
                //Gets the vehicle from the specified garage slot 
                vehicle = GetVehicle(garageSlot);
            }
            
              var UnloadedProducts = 0;
            //products are added to the storage's product until the vehicle empties, or the storage fills up
            while (!IsFull && GetVehicle(garageSlot)!=null)
              {
                  products.Add(vehicle.Unload());
                  UnloadedProducts++;
              }
             return UnloadedProducts;

           
        }



        public Storage(int capacity, int garageSlots)
        {
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
        }

        public Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            //add IEnumerable vehicles to garage 
            this.garage = vehicles.ToList();

        }

    }



}

