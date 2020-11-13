using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using System.Text;

namespace StorageMaster
{
    class StoragMaster
    {
        public List<Product> productsPool = new List<Product>();
        public List<Storage> storagesPool = new List<Storage>();
        //I used IDictionary for StorageRegistery because object by passing the type of keys and values it can store 
        public IDictionary<string, Storage> StorageRgistery = new Dictionary<string, Storage>();
        public Vehicle currentvehicle;

        //Creates a product and adds it to the product pool
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
            return $"Added {type} to pool.";
        }

        //Creates a storage and adds it to the storage registry
        public string RegisterStorage(string type, string name)
        {
            Storage newStorage;


            switch (type)
            {
                case "AutomatedWarehouse":
                    newStorage = new AutomatedWarehouse();
                    break;
                case "DistributionCenter":
                    newStorage = new DistributionCenter();
                    break;
                case "Warehouse":
                    newStorage = new Warehouse();
                    break;

                default: { throw new InvalidOperationException("Invalid storage type!"); }
            }

            storagesPool.Add(newStorage);
            return $"Registered {name} ";


        }
        //Sets the current vehicle to the vehicle in that storage’s garage slot. The current vehicle is Sets with GetVehicle(garageSlot) 
        public string SelectVehicle(string storageName, int garageSlot)
        {

            currentvehicle = StorageRgistery[storageName].GetVehicle(garageSlot);

            return $"Selected {currentvehicle.GetType()}";

        }
        public string LoadVehicle(IEnumerable<string> productNames)
        {
            int loadedProductsCount = 0;
            int EnumerableCount = 0;
            //goes through each item of the productspool
            foreach (var item in productsPool)
            {
                if (currentvehicle.IsFull) {  break; }
                //goes through each of the product names 
                foreach (string value in productNames)
                {
                    if (productNames != item)
                    {
                        throw new InvalidOperationException($"{item} is out of stock!");
                    }
                    //the last product with that name in the pool is removed from the pool and loaded in the vehicle
                   else if (productsPool.Count > 0)
                    {
                        var lasrProduct = productsPool[productsPool.Count - 1];
                        productsPool.Remove(lasrProduct);
                    }

                    EnumerableCount++;
                }
                loadedProductsCount++;
            }
            return $"Loaded { loadedProductsCount}/{ EnumerableCount} products into {currentvehicle.GetType() }";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            //If either the source storage or the destination storages don’t exist
            if (!StorageRgistery.ContainsKey(sourceName))
            { throw new InvalidOperationException("Invalid source storage!"); }
            if (!StorageRgistery.ContainsKey(destinationName))
            { throw new InvalidOperationException("Invalid destination storage!"); }

            //gets the vehicle from the storage at the provided garage slot and sends it to the destination storage
            var vehicle = SelectVehicle(sourceName, sourceGarageSlot);
            var sourceStorage = StorageRgistery[sourceName];
            var destinationStorage = StorageRgistery[destinationName];

            var destinationGarageSlot = sourceStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);

            return $"Sent {vehicle.GetType()} to {destinationName} (slot {destinationGarageSlot}.)";
        }
        public string UnloadVehicle(string storageName, int garageSlot)
        {
            //gets the vehicle in the storage’s garage slot
            var storage = StorageRgistery[storageName];
            var productsInVehicle = storage.GetVehicle(garageSlot);
            //the vehicle is unloaded at the storage.
            int unloadedProductsCount = storage.UnloadVehicle(garageSlot);
            return $"Unloaded {unloadedProductsCount}/{productsInVehicle} products at {storageName}.";
        }
        public string GetStorageStatus(string storageName) 
        {
            var storage = StorageRgistery[storageName];

            //The storage’s products are counted, grouped by name, sorted by the product count (descending), then by product name (ascending).

            var ProductInfo = from pd in storage.Products
                            group pd by pd.GetType().Name into pdGroup
                            orderby pdGroup.Count() descending
                            select new
                            {
                                Key = pdGroup.Key,
                                products = pdGroup.OrderBy(x => x.GetType().Name)
                            };
 
            
            List<string> GarageVehicleNames = new List<string>();

            //every vehicle’s name in the garage is retrieved
            foreach (var vehicle in storage.garage)
            {
                if (vehicle == null)
                    GarageVehicleNames.Add("empty");
                else
                    GarageVehicleNames.Add(vehicle.GetType().Name);
            }
            //sum of the products’ weight
            double productsWeights = storage.Products.Sum(p => p.Weight);
           
         

            return $"Stock ({productsWeights} / {storage.Capacity}) : [{ProductInfo} \nGarage: [{GarageVehicleNames}]";

            

        }

    }

}