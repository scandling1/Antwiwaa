namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class ReferenceData<TId>
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public TId Value { get; set; }
    }
}