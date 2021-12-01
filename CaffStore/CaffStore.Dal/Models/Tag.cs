namespace CaffStore.Dal.Models
{
    public class Tag
    {
        public Tag(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
