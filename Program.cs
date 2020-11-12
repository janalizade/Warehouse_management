using System;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster
{
    //Product product = new Product();
   public class Program
    {
        static void Main(string[] args)
        {

            Program p = new Program();
            p.Storage_addProduct();
            //p.Storage_Master();
           // p.Vehicle_IsFull_Test();

           // p.Vehicle_Truck_Trunck_LoadProduct_Test();
            //p.Vehicle_Truck_Trunck_UNLoadProduct_Test();
           // p.Vehicle_Truck_Trunck_LoadProduct_Test();
            //p.Vehicle_Truck_Capacity_Test();
            //  p.Product_Ram_Create_happy_price_Test();
            //  p.Product_Ram_Create_happy_price_Test();
            //  p.Product_Ram_Create_Zero_Price_Test();
            //  p.Product_Ram_Create_Negative_Price_constructor_Test();
            //  p.Product_Ram_Create_Negative_Price_setter_Test();
            // p.Vehicle_Truck_Trunck_EMPTY_Test();

        }
        public void Storage_addProduct()
        {
            string type; double price;
           type = Console.ReadLine();
           price =Convert.ToDouble(Console.ReadLine());
           StoragMaster sm = new StoragMaster();
           sm.AddProduct(type, price);
        }
       
        public void Storage_SendVehicleTo_Test()
        {
            Vehicle truck = new Truck();
            Storage warehouse= new Warehouse();
            warehouse.SendVehicleTo(0,warehouse);
            warehouse.SendVehicleTo(1, warehouse);
        }

        public void Storage_GetVehicle_Test()
        {
            
            Vehicle truck = new Truck();
            Storage warehouse = new Warehouse();
            warehouse.GetVehicle(2);

        }

        public void Vehicle_IsFull_Test()
        {
            Vehicle truck = new Truck();
            Console.WriteLine(truck.IsFull);



        }
        public void Vehicle_Truck_Trunck_UNLoadProduct_Test()
        {

            try
            {
                Vehicle truck = new Truck();
                truck.LoadProduct(new Gpu(100));
                Product actual = new Ram(10);
                truck.LoadProduct(actual);
                
             Product expected=   truck.Unload();

                if (expected.Price == actual.Price) { Console.WriteLine("actual and expected price are equals"); }
                if (expected.Weight == actual.Weight) { Console.WriteLine("actual and expected weight are equals"); }

                if (truck.products.Count()==1) { Console.WriteLine("Truck trunk size is 1"); }

                Console.WriteLine("expected clas type="+ expected.GetType());
            }
            catch (InvalidOperationException e)
            {
                if (e.Message == "No products left in vehicle!") 
                {
                    Console.WriteLine("No products left in vehicle!");
                }
            }


           

        }

        public void Vehicle_Truck_Trunck_LoadProduct_Test()
        {
            Vehicle truck = new Truck();
            IReadOnlyCollection<Product> Trunk = truck.products;
            Product gpu = new Gpu(100);
            Product ram = new Ram(200);
            truck.LoadProduct(ram);
            truck.LoadProduct(gpu);
            
            Console.WriteLine("The Count of Product in Trunk "+Trunk.Count());

        }
        public void Vehicle_Truck_Trunck_EMPTY_Test()
        {
            Vehicle truck = new Truck();
            IReadOnlyCollection<Product> Trunk = truck.products;

            if(Trunk==null)
              Console.WriteLine("truck trunk is null");

            Console.WriteLine(Trunk.Count());

        }

        public void Vehicle_Truck_Capacity_Test() 
        {
            Vehicle truck = new Truck();
            Console.WriteLine(truck.Capacity);
        
        
        }
        public void Vehicle_AddProductInTrunk_Test()
        {
            Product ram = new Ram(200);
            Vehicle truck = new Truck();
            truck.LoadProduct(ram);
           


        }
        public void Product_Ram_Create_happy_price_Test()
        {
            Product ram = new Ram(200);
            Console.WriteLine(ram.Price);
            Console.WriteLine(ram.Weight);
        }


        public void Product_Ram_Create_Zero_Price_Test()
        {
            Product ram = new Ram(0);

            Console.WriteLine(ram.Price);
            Console.WriteLine(ram.Weight);
        }


        public void Product_Ram_Create_Negative_Price_constructor_Test()
        {
            try { 
            Product ram = new Ram(-1);
            }
            catch (Exception e) {
                if (e.Message == "price can not be negetive") {
                    Console.WriteLine("Correct Error Validation Message by constructor");
                }
            }
                    }



        public void Product_Ram_Create_Negative_Price_setter_Test()
        {
            Product ram = new Ram(100);
            try
            {
                ram.Price = -100;
            }
            catch (Exception e)
            {
                if (e.Message == "price can not be negetive")
                {
                    Console.WriteLine("Correct Error Validation Message by setter");
                }
            }
        }

        public void Product_Trunk_Check_Test()
        {
            Product ram = new Ram(200);
            Console.WriteLine(ram.Price);
            Console.WriteLine(ram.Weight);
        }
    }



 
}
