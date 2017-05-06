namespace DomainModel
{
    public class LoyalityProg
    {
        public int Id { get; set; }

        public int PbOnPtsReciv { get; set; }
        public int LPGPtsReciv { get; set; }
        public int WashPtsReciv { get; set; }
        public int WaxPtsReciv { get; set; }

        public int PbOnPtsReq { get; set; }
        public int LPGPtsReq { get; set; }
        public int WashPtsReq { get; set; }
        public int WaxPtsReq { get; set; }
    }
}
