using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique.GameObjects
{
    public class Resource
    {
        public string Type;
        public int Quantity;
        public int InventorySlot;

        public Resource() 
        { 
        
        }

        public Resource(Resource pCopy)
        {
            Type = pCopy.Type;
            Quantity = pCopy.Quantity;
            InventorySlot = pCopy.InventorySlot;
        }
    }

    public static class ResourceData
    {
        public static Dictionary<string, Resource> Data = new Dictionary<string, Resource>();

        public static void PopulateData()
        {
            Data.Add("Stone", new Resource { Type = "Stone" , InventorySlot = 1});
            Data.Add("Wood", new Resource { Type = "Wood", InventorySlot = 2});
            Data.Add("Food", new Resource { Type = "Food", InventorySlot = 3});
            Data.Add("Gold", new Resource { Type = "Gold", InventorySlot = 4 });
            Data.Add("Science", new Resource { Type = "Science", InventorySlot = 5 });
        }

    }
}
