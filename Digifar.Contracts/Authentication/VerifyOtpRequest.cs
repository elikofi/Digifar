using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digifar.Contracts.Authentication
{
    public record VerifyOtpRequest(string PhoneNumber, string Otp);
}
