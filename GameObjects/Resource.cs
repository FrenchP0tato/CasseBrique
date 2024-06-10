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
            Data.Add("STONE", new Resource { Type = "STONE" , InventorySlot=1});
            Data.Add("WOOD", new Resource { Type = "WOOD", InventorySlot = 2 });
            Data.Add("FOOD", new Resource { Type = "FOOD", InventorySlot = 3 });
            Data.Add("GOLD", new Resource { Type = "GOLD", InventorySlot = 4 });
            Data.Add("SCIENCE", new Resource { Type = "SCIENCE", InventorySlot = 5 });
        }

    }
}
