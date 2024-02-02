namespace TA.Data.Contracts.Dto
{
    public class Comments
    {
        public required int Position {  get; set; }
        public required List<string> Comment { get; set; }
    }
}
