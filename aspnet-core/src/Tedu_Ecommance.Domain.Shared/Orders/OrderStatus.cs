using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Orders
{
    public enum OrderStatus
    {
        New,
        Confirmed,
        Processing,
        Shipping,
        Finished,
        Canceled
    }
}
