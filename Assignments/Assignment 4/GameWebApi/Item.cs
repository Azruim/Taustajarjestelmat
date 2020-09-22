using System;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi
{
    public class Item
    {
        enum ItemType { SWORD, POTION, SHIELD }
        public Guid Id { get; set; }
        [Range(1,99)]
        public int Level { get; set; }
        [EnumDataType(typeof(ItemType))]
        public int itemType { get; set; }
        [DateValidation]
        public DateTime CreationDate { get; set; }
    }
}