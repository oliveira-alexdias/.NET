﻿namespace Safe2Pay
{
    public class MerchantPaymentDate
    {
        public int Id { get; set; }
        public Merchant Merchant { get; set; }
        public PlanFrequence PlanFrequence { get; set; }
        public int? PaymentDay { get; set; }
    }
}