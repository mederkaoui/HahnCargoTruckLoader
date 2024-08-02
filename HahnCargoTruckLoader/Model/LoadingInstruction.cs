namespace HahnCargoTruckLoader.Model
{
    public class LoadingInstruction
    {
        public int LoadingStepNumber { get; set; }
        public int CrateId { get; set; }
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public bool TurnHorizontal { get; set; }
        public bool TurnVertical { get; set; }
    }
}