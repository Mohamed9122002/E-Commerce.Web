using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class AddressNotFoundExpection(string UserName) : NotFoundException($"User {UserName} Has Not Address")
    {
    }
}
