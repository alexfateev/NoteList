
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

static class ListNotes
{
    static public List<Note> list = new List<Note>();
    static string filePath = "notes.json";

    static public void SaveToFile()
    {
        // Сериализация списка в JSON
        string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        // Запись JSON в файл
        File.WriteAllText(filePath, json);
    }

    static public List<Note> LoadFromFile()
    {
        // Чтение JSON из файла
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            // Десериализация JSON в список объектов
            return JsonSerializer.Deserialize<List<Note>>(json);
        }
        return new List<Note> { };
    }

    private static void ShowMainMenuText()
    {
        Console.Clear();
        Console.WriteLine("1. Создать заметку");
        Console.WriteLine("2. Показать все заметки");
        Console.WriteLine("3. Поиск по заметкам");
        Console.WriteLine("4. Показать корзину");
        Console.WriteLine("5. Выход");
        Console.Write("Выберите действие: ");
    }

    public static void ShowNoteMenuText(Note note)
    {
        Console.Clear();
        note.Print();
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("1. Изменить имя заметки");
        Console.WriteLine("2. Редактировать заметку");
        if (note.IsDeleted)
        {
            Console.WriteLine("3. Вернуть из корзины");
        }
        else
        {
            Console.WriteLine("3. Удалить в корзину");
        }
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
                        //Пометить как удаленную заметку
                        note.IsDeleted = !note.IsDeleted;
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
        bool isRunning = true;
        while (isRunning)
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
                        showList(list);
                        break;
                    case 3:
                        SearchNotes();
                        break;
                    case 4:
                        showResycle();
                        break;
                    case 5:
                        isRunning = false;
                        SaveToFile();
                        break;
                }
            }
        }
    }

    private static void showResycle()
    {
        var deletedNotes = list.Where(n => n.IsDeleted).ToList();

        if (deletedNotes.Any())
        {
            showList(deletedNotes);
        }
    }

    private static void SearchNotes()
    {
        Console.Write("Введите текст для поиска (по названию или содержимому): ");
        string searchText = Console.ReadLine();

        var foundNotes = list.Where(n => (n.Title.Contains(searchText) ||
                                           n.Text.ToString().Contains(searchText)) &&
                                          !n.IsDeleted).ToList();

        if (foundNotes.Any())
        {
            showList(foundNotes);
        }
    }

    static public void addNote()
    {
        Console.Clear();
        Console.Write("Введите название заметки: ");
        string result = Console.ReadLine();
        if (!string.IsNullOrEmpty(result))
        {
            list.Add(new Note(result));
        }
    }

    static public bool getValueByIndex(List<Note> list, int index, out Note note)
    {
        note = null;
        if (index <= list.Count)
        {
            note = list.ElementAtOrDefault(index - 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    static public void showList(List<Note> list)
    {
        Console.Clear();
        int count = 0;
        foreach (var note in list)
        {
            if (!note.IsDeleted)
                Console.WriteLine($"{++count}. {note.Title}");
        }
        Console.Write("Выберите заметку (оставьте пустым для возврата в главное меню): ");
        var result = Console.ReadLine();
        if (result != null)
        {
            bool resultParsed = int.TryParse(result, out int parseInt);
            if (resultParsed)
            {
                if (getValueByIndex(list, parseInt, out Note noteResult))
                    NoteMenu(noteResult);
                Console.ReadKey();
            }
        }
    }

}

