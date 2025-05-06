using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModel;
using Microsoft.AspNetCore.Mvc.Formatters;
using Shared.DataTransferObject.IdentityDTOS;
using Shared.OrderDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email)
        {
            // insert To Record inside Table Orders 
            // email , Address , DeliveryMethod ,Items, SubTotal
            // Steps  
            //1 Mapp Address To Order Address
            var OrderAddress = _mapper.Map<AddressDTo, OrderAddress>(orderDTo.Address);
            //2 Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDTo.BaskedId) ?? throw new BasketNotFoundException(orderDTo.BaskedId);

            //3 Create OrderItem List 
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                OrderItem orderItem = CreateOrderItem(item, Product);
                OrderItems.Add(orderItem);
            }

            // 4 Get Delivery Method 
            var DeliveryMethodRepo = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTo.DeliveryMethodId)??throw new DeliveryMethodNotFountException(orderDTo.DeliveryMethodId);
            // 5 Calculate Sub Total 
            var SubTotal = OrderItems.Sum(I => I.Price * I.Quantity);
            var Order = new Order(Email,OrderAddress, DeliveryMethodRepo, OrderItems, SubTotal);
            // 6 Add Order To Database
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            // 7 Save Changes
            await _unitOfWork.SaveChangesAsync();
            // 8 Map Order To OrderToReturnDTo
            return _mapper.Map<Order, OrderToReturnDTo>(Order);


        }

        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModule.BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder()
                {
                    ProductId = Product.Id,
                    ProductUrl = Product.PictureUrl,
                    ProductName = Product.Name
                },
                Price = Product.Price,
                Quantity = item.Quantity
            };
        }
    }
}
