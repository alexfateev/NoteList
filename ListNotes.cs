
static class ListNotes
{
    static public Dictionary<string, Note> list = new Dictionary<string, Note>();

    private static void ShowMainMenuText()
    {
        Console.Clear();
        Console.WriteLine("1. Создать заметку");
        Console.WriteLine("2. Показать все заметки");
        Console.WriteLine("3. Поиск по заметкам");
        Console.WriteLine("4. Открыть корзину");
        Console.Write("Выберите действие: ");

    }

    public static void ShowNoteMenuText(Note note)
    {
        Console.Clear();
        Console.WriteLine("1. Изменить имя заметки");
        Console.WriteLine("2. Показать заметку");
        Console.WriteLine("3. Добавить текст");
        Console.WriteLine("4. Удалить в корзину");
        Console.WriteLine("5. Назад...");
        Console.Write("Выберите действие: ");
    }

    public static void ShowNoteMenu(Note note)
    {

    }

    public static void ShowMainMenu()
    {
        while (true)
        {
            ShowMainMenuText();
            bool result = int.TryParse(Console.ReadLine(), out var number);
            if (result)
            {
                switch (number)
                {
                    case 1:
                        addNote();
                        break;
                    case 2:
                        showList();
                        break;
                    case 3:
                        findNoteOrText();
                        break;
                    case 4:
                        showResycle();
                        break;
                }
            }
        }
    }

    private static void showResycle()
    {
        throw new NotImplementedException();
    }

    private static void findNoteOrText()
    {
        throw new NotImplementedException();
    }

    static public void addNote()
    {
        Console.Clear();
        Console.Write("Введите название заметки: ");
        string? result = Console.ReadLine();
        if (result != null)
        {
            list.Add(result, new Note(result));
        }
    }

    //static public bool getValueByIndex(int index, out Note note)
    //{
        
    //}

    static public void showList()
    {
        Console.Clear();
        int count = 0;
        foreach (var note in list)
        {
            Console.WriteLine($"{++count}. {note.Key}");
        }
        Console.Write("Выберите заметку (оставьте пустым для возврата в главное меню): ");
        var result = Console.ReadLine();
        if (result != null)
        {
            bool resultParsed = int.TryParse(result, out int parseInt);
            if (resultParsed)
            {
                Console.WriteLine( list.ElementAtOrDefault(parseInt-1));
                Console.ReadKey();
            }
        }
    }

}

