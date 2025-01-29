using System.Text;

class Note
{

    public string Name { get; private set; }
    public StringBuilder Text { get; private set; }
    public DateTime Date { get; private set; } = DateTime.Now;
    public Boolean IsDeleted { get; private set; } = false;


    public Note(string name)
    {
        Name = name;
        Text = new StringBuilder();
    }

    public void addText(string text)
    {
        Text.Append(text);
        Date = DateTime.Now;
    }

    public void rename(string newName)
    {
        Name = newName;
        Date = DateTime.Now;
    }

    public string getText()
    {
        return Text.ToString();
    }

}

