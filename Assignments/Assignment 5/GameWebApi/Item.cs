using System;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi
{
    public enum ItemType { SWORD, POTION, SHIELD }
    public class Item
    {
        public Guid Id { get; set; }
        [Range(1, 99)]
        public int Level { get; set; }
        [EnumDataType(typeof(ItemType))]
        public ItemType itemType { get; set; }
        [DateValidation]
        public DateTime CreationDate { get; set; }

        public Item()
        {
            this.Id = Guid.NewGuid();
            this.Level = 1;
            this.itemType = ItemType.SWORD;
            this.CreationDate = DateTime.Now;
        }
    }

    public class NewItem
    {
        [Range(1, 99)]
        public int Level { get; set; }
        [EnumDataType(typeof(ItemType))]
        public ItemType itemType { get; set; }
    }

    public class ModifiedItem
    {
        [Range(1, 99)]
        public int Level { get; set; }
        [EnumDataType(typeof(ItemType))]
        public ItemType itemType { get; set; }
    }

}