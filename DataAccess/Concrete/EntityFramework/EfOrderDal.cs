using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, NorthwindContext> , IOrderDal
    {
        //public List<OrderDetailDto> GetOrderDetails()
        //{
        //    using (NorthwindContext context = new NorthwindContext())
        //    {
        //        var result = from o in context.Orders
        //                     join c in context.Customers
        //                     on o.CustomerID equals c.CustomerID
        //                     select new OrderDetailDto {};


        //    }
        //}
    }
}
