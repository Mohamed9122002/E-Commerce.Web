using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModul;
using ServiceAbstraction;
using Shared.DTOS.AuthDTOs;
using Shared.DTOS.OrderDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTo> CreateOrder(OrderDto orderDto, string Email)
        {
            // Mapping Address To Order Address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);
            // Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);
            // Create OrderItem List 
            List<OrderItem> orderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = Product.Id,
                        PictureURL = Product.PictureUrl,
                        ProductName = Product.Name,

                    },
                    Price = Product.Price,
                    Quantity = item.Quantity,
                };
                orderItems.Add(OrderItem);
            }
            //Get Delivery Method 
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            // Calculate Sub Total 
            var SubTotal = orderItems.Sum(I => I.Quantity * I.Price);
            var Order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, SubTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<Order, OrderToReturnDTo>(Order);
        }
    }
}
