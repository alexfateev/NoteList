
static class ConsoleEditor
{
    public static List<string> RunEditor(List<string> text)
    {
        Console.WriteLine("Ctrl+S - Сохранить и выйти, Esc - Выйти без сохранения");
        Console.WriteLine("-----------------------------");

        List<string> lines = text;
        int cursorX = 0; // Позиция курсора по горизонтали
        int cursorY = 0; // Позиция курсора по вертикали

        bool isRunning = true;
        while (isRunning)
        {
            // Выводим текст
            Console.Clear();
            for (int i = 0; i < lines.Count; i++)
            {
                Console.WriteLine(lines[i]);
            }

            // Установка позиции курсора
            Console.SetCursorPosition(cursorX, cursorY);

            // Обработка ввода пользователя
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    // Разделение строки на две части
                    string currentLine = lines[cursorY];
                    string leftPart = currentLine.Substring(0, cursorX);
                    string rightPart = currentLine.Substring(cursorX);
                    lines[cursorY] = leftPart;
                    lines.Insert(cursorY + 1, rightPart);
                    cursorY++;
                    cursorX = 0;
                    break;

                case ConsoleKey.Backspace:
                    if (cursorX > 0)
                    {
                        // Удаление символа слева
                        lines[cursorY] = lines[cursorY].Remove(cursorX - 1, 1);
                        cursorX--;
                    }
                    else if (cursorY > 0)
                    {
                        // Удаление перевода строки
                        string lineAbove = lines[cursorY - 1];
                        string currentLineText = lines[cursorY];
                        lines.RemoveAt(cursorY);
                        cursorY--;
                        cursorX = lineAbove.Length;
                        lines[cursorY] = lineAbove + currentLineText;
                    }
                    break;

                case ConsoleKey.Delete:
                    if (cursorX < lines[cursorY].Length)
                    {
                        // Удаление символа справа
                        lines[cursorY] = lines[cursorY].Remove(cursorX, 1);
                    }
                    else if (cursorY < lines.Count - 1)
                    {
                        // Удаление перевода строки
                        string currentLineText = lines[cursorY];
                        string lineBelow = lines[cursorY + 1];
                        lines.RemoveAt(cursorY + 1);
                        lines[cursorY] = currentLineText + lineBelow;
                    }
                    break;

                case ConsoleKey.LeftArrow:
                    if (cursorX > 0)
                    {
                        cursorX--;
                    }
                    else if (cursorY > 0)
                    {
                        cursorY--;
                        cursorX = lines[cursorY].Length;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (cursorX < lines[cursorY].Length)
                    {
                        cursorX++;
                    }
                    else if (cursorY < lines.Count - 1)
                    {
                        cursorY++;
                        cursorX = 0;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (cursorY > 0)
                    {
                        cursorY--;
                        cursorX = Math.Min(cursorX, lines[cursorY].Length);
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (cursorY < lines.Count - 1)
                    {
                        cursorY++;
                        cursorX = Math.Min(cursorX, lines[cursorY].Length);
                    }
                    break;

                case ConsoleKey.Escape:
                    //Выходим без изменений. Возвращается оригинальный текст (return text);
                    isRunning = false;
                    break;

                case ConsoleKey.S when key.Modifiers == ConsoleModifiers.Control:
                    // Возвращение измененного текста
                    isRunning = false;
                    return lines;

                default:
                    if (!char.IsControl(key.KeyChar))
                    {
                        // Вставка символа
                        lines[cursorY] = lines[cursorY].Insert(cursorX, key.KeyChar.ToString());
                        cursorX++;
                    }
                    break;
            }
        }

        return text;
    }
}

