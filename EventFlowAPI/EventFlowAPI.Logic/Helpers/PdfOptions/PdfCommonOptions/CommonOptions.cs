namespace EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions
{
    public class CommonOptions
    {
        public CommonOptions() 
        {
            BottomLine = new();
            TopLine = new();
        }  

        // Bottom Line
        public BottomLineOptions BottomLine { get; private set; }

        // Top Line
        public TopLineOptions TopLine { get; private set; }
    }
}
