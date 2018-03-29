namespace BusinessLayer.Models
{
    public class TIMSTableListing
    {
        public int DBKey { get; set; }
        public string TableID { get; set; }
        public string TableName { get; set; }
        public string TableInfo { get; set; }
        public string SecondaryKeyInd { get; set; }
        public string DataCharCount { get; set; }
        public string EffectiveHistInd { get; set; }
        public string ContainsEffectiveHistReqdInd { get; set; }
        public string ExternalEditProgram { get; set; }
    }
}