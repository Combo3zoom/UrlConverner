namespace DataLayer.Model;

public class Description
{
    public Description(int descriptionId, string text)
    {
        DescriptionId = descriptionId;
        Text = text;
    }

    private Description() { }

    public int DescriptionId { get; set; }
    public string Text { get; set; }
}