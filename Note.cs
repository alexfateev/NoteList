using static ConsoleEditor;

class Note
{

    public string Title { get; private set; }
    public List<string> Text { get; set; }
    public DateTime Date { get; private set; }
    public bool IsDeleted { get; set; }


    public Note(string title)
    {
        Title = title;
        Text = new List<string> { "" };
        Date = DateTime.Now;
        IsDeleted = false;
    }

    public void Rename()
    {
        Console.Write("Введите новое название: ");
        string newTitle = Console.ReadLine();
        if (!string.IsNullOrEmpty(newTitle))
        {
            Title = newTitle;
            Date = DateTime.Now;
        }
    }

    public void EditText()
    {
        Text = ConsoleEditor.RunEditor(Text);
        Date = DateTime.Now;
    }

    public void Print()
    {
        Console.WriteLine($"{Title} (Последнее изменение: {Date})"); 
        Text.ForEach(line => Console.WriteLine(line));
    }

}

