using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(), Messages.Listed);
        }

        public IDataResult<List<Order>> GetAllByCustomerID(string customerId)
        {
           return  new SuccessDataResult<List<Order>>(_orderDal.GetAll(o=> o.CustomerID == customerId));
        }
        
        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(Messages.Updated);
        }
    }
}
