using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemention
{
    public class ServiceManager(IUnitOfWork _unitOfWork ,IMapper _mapper) : IServiceManager
    {
        //  Lazy Impementation of the ProductServices
        private readonly Lazy<IProductServices> _lazyProductServices = new Lazy<IProductServices>(() => new ProductServices(_unitOfWork,_mapper));
        public IProductServices ProductServices => _lazyProductServices.Value;
    }
}
