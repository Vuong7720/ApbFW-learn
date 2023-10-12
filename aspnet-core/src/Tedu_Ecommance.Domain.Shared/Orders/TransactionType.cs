using System;
using System.Collections.Generic;
using System.Text;

namespace Tedu_Ecommance.Orders
{
    public enum TransactionType
    {
        ConfirmOrder,
        StartProcessing,
        FinishOrder,
        CancelOrder
    }
}
