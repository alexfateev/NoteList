
static class ListNotes
{
    static public Dictionary<string, Note> list = new Dictionary<string, Note>();

    private static void ShowMainMenuText()
    {
        Console.Clear();
        Console.WriteLine("1. Создать заметку");
        Console.WriteLine("2. Показать все заметки");
        Console.WriteLine("3. Поиск по заметкам");
        Console.WriteLine("4. Показать корзину");
        Console.Write("Выберите действие: ");
    }

    public static void ShowNoteMenuText(Note note)
    {
        Console.Clear();
        note.Print();
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("1. Изменить имя заметки");
        Console.WriteLine("2. Редактировать заметку");
        Console.WriteLine("3. Удалить в корзину");
        Console.WriteLine("4. Назад...");
        Console.Write("Выберите действие: ");
    }

    public static void NoteMenu(Note note)
    {
        bool isRunning = true;
        while (isRunning)
        {
            ShowNoteMenuText(note);
            bool result = int.TryParse(Console.ReadLine(), out int number);
            if (result)
            {
                switch (number)
                {
                    case 1:
                        //Изменить название
                        note.Rename();
                        break;
                    case 2:
                        //Редактировать заметку
                        note.EditText();
                        break;
                    case 3:
                        //Переместить в корзину
                        note.IsDeleted = true;
                        break;
                    case 4:
                        isRunning = false;
                        break;
                }
            }
        }
    }

    public static void MainMenu()
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
        string result = Console.ReadLine();
        if (!string.IsNullOrEmpty(result))
        {
            list.Add(result, new Note(result));
        }
    }

    static public bool getValueByIndex(int index, out Note note)
    {
        note = null;
        if (index <= list.Count)
        {
            note = list.ElementAtOrDefault(index - 1).Value;
            return true;
        }
        else
        {
            return false;
        }
    }

    static public void showList()
    {
        Console.Clear();
        int count = 0;
        foreach (var note in list)
        {
            if (!note.Value.IsDeleted)
                Console.WriteLine($"{++count}. {note.Key}");
        }
        Console.Write("Выберите заметку (оставьте пустым для возврата в главное меню): ");
        var result = Console.ReadLine();
        if (result != null)
        {
            bool resultParsed = int.TryParse(result, out int parseInt);
            if (resultParsed)
            {
                if (getValueByIndex(parseInt, out Note noteResult))
                    NoteMenu(noteResult);
                Console.ReadKey();
            }
        }
    }

}

