﻿namespace Safe2Pay
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        //Para SingleSale
        public string CodePaymentMethod { get; set; }
    }
}