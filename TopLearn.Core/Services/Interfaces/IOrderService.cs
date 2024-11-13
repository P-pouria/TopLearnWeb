using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Order;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(string userName, int courseId);
        void UpdatePriceOrder(int orderId);
        Order GetOrderForUserPanel(string userName, int orderId);
        Order GetOrderById(int orderId);
        bool FinalyOrder(string userName, int orderId);
        List<Order> GetUserOrders(string userName);
        void UpdateOrder(Order order);

        #region Discount

        DiscountUseType UseDiscount(int orderId, string code);

        #endregion
    }
}
