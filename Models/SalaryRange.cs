namespace BusinessLayer.Models
{
    public class SalaryRange
    {
        public string ClassKeyID { get; set; }
        public string ClassDataID { get; set; }
        public string SalaryPOS { get; set; }
        public string SalaryStructureCode { get; set; }
        public string SalaryRateCount { get; set; }
        public string SalaryRate1 { get; set; }
        public string SalaryRate2 { get; set; }
        public string SalaryAdjustCatCode { get; set; }
        public string SalaryAdjustPercentRate1 { get; set; }
        public string PayFreqIndicator { get; set; }
        public string PayFreqUnits { get; set; }
        public string IncrementPercent1 { get; set; }
    }
}