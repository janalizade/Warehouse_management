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
        private readonly List<Vehicle> garage = new List<Vehicle>();
        private IReadOnlyCollection<Vehicle> Garage { get { return vehicles; } }
        private readonly List<Product> products = new List<Product>();
        private readonly List<Vehicle> vehicles = new List<Vehicle>();
        public string Name { set; get; }
        public int Capacity
        {
            set { capacity = value; }
            get
            {
                int max = products.Select(i => Convert.ToInt32(i.Weight)).Max();
                return max;
            }

        }
        public int GarageSlots
        {
            set { garageslots = value; }
            get
            {
                int sum = products.Select(i => Convert.ToInt32(i.Weight)).Sum();
                return sum;
            }
        }
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

            if (garageSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }
            else if (Garage.ElementAt(garageSlot) == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }
            return Garage.ElementAt(garageSlot);
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            var sentVehicle = GetVehicle(garageSlot);

            var FreeSlotInGarage = deliveryLocation.Garage.Any(p => p == null);
            if (FreeSlotInGarage == false)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            garage[garageSlot] = null;
            int FreedSlot = Convert.ToInt32(garage[garageSlot]);
            return FreedSlot;


        }
        public int UnloadVehicle(int garageSlot)
        {
            if (IsFull) { throw new InvalidOperationException("Storage is full!"); }
            else
            {
                vehicle = GetVehicle(garageSlot);
            }
            var VehicleToGet = GetVehicle(garageSlot);
            //  var UnloadedProducts = 0;

            //  while (!IsFull && !VehicleToGet.IsEmpty)
            //  {
            //      var temp = VehicleToGet.UnLoad();
            //      StorageProducts.Add(temp);
            //      UnloadedProducts++;
            //  }
            //  return UnloadedProducts;

            return 1;
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
            this.garage = vehicles.ToList();

        }

    }



}

