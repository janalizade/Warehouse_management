using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace StorageMaster
{
    class StoragMaster
    {
       public  List<Product> productsPool = new List<Product>();
        public List<Storage> storagesPool = new List<Storage>();
        public IDictionary<string, Storage> StorageRgistery = new Dictionary<string, Storage>();
        public Vehicle currentvehicle;
        public string AddProduct(string type, double price)
        {
            Product newProduct;
            

            switch (type)
            {
                case "Gpu":
                    newProduct = new Gpu(price);
                    break;
                case "Ram":
                    newProduct = new Ram(price);
                    break;
                case "hardDrive":
                    newProduct = new HardDrive(price);
                    break;
                case "SolidStateDrive":
                    newProduct = new SolidStateDrive(price);
                    break;
                default: { throw new InvalidOperationException("Invalid product type!"); }
            }
            
            productsPool.Add(newProduct);
            return "Added {type} to pool.";
        }

        public string RegisterStorage(string type, string name)
        {
            Storage newStorage;


            switch (type)
            {
                case "AutomatedWarehouse":
                    newStorage= new AutomatedWarehouse();
                    break;
                case "DistributionCenter":
                    newStorage = new DistributionCenter();
                    break;
                case "Warehouse":
                    newStorage =new Warehouse();
                    break;
                
                default: { throw new InvalidOperationException("Invalid storage type!"); }
            }

            storagesPool.Add(newStorage);
            return $"Registered {name} ";

            
        }
        public string SelectVehicle(string storageName, int garageSlot)
        {

             currentvehicle = StorageRgistery[storageName].GetVehicle(garageSlot);

            return $"Selected {currentvehicle.GetType()}";
       
        }
        public void LoadVehicle(IEnumerable<string> productNames)
        {
            
            foreach (var item in productsPool)
            {
                if (currentvehicle.IsFull) break;    
              //  if(currentvehicle.products[item].)
            }
         //   IENumberable<string> tempList = productsPool..Select(x => x.).ToList();
        }



    }
}

