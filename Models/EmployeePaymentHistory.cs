namespace BusinessLayer.Models
{
    public class EmployeePaymentHistory
    {
        public string PaymentHistorySSN { get; set; }
        public string PositionSequenceNumber { get; set; }
        public string PositionNumber { get; set; }
        public string PayYear { get; set; }
        public string PayMonth { get; set; }
        public string DeductionPayYear { get; set; }
        public string DeductionPayMonth { get; set; }
        public string ZeroIndicator { get; set; }
        public string FutureUse1 { get; set; }
        public string PayPeriod { get; set; }
        public string DayPaid { get; set; }
        public string HoursPaid { get; set; }
        public string LowestCostEmpShareforEmpOnlyCoverage { get; set; }
        public string HealthDeductionFlag { get; set; }
        public string DependentLevel { get; set; }
        public string DeductionPayPeriod { get; set; }    
    }
}