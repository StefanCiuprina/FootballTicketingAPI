using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballTicketingAPI.Payment.Card
{
    public class CreditCardValidator
    {
        public static bool Validate(CreditCard card)
        {
            if (card.SecurityCode == "789")
            {
                return true;
            }
            return false;
        }
    }
}
