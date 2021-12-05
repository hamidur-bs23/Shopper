using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    public class OrderReadDto
    {
        public string CustomerName{ get; set; }

        public IList<ItemWithPriceAndQuantity> ListOfOrderedItems { get; set; }       
    }

    public static class OrderReadDtoExtensions
    {
        //public static Order ToEntity(this OrderCreateDto dto)
        //{
            

        //    return order;
        //}
    }

    public class OrderedItems
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }
    }
}

