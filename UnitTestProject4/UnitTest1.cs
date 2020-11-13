using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StorageMaster
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Storage_GetVehicle_Test()
        {

            StorageMaster.Vehicle truck = new Truck();
            StorageMaster.Storage warehouse = new Warehouse(4, 3);
            warehouse.GetVehicle(2);

        }
    }
}
