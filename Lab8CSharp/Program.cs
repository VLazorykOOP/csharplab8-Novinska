using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Tasks:");
            Console.WriteLine("1. Text Processing with Subtexts and Coordinates");
            Console.WriteLine("2. Remove Specific Words and Replace Prefixes");
            Console.WriteLine("3. Create Third Text from Words Not in Second Text");
            Console.WriteLine("4. Binary File Operations with Real Numbers");
            Console.WriteLine("5. Additional Task");
            Console.WriteLine("6. Exit");
            Console.Write("Task Number: ");
            string choice = Console.ReadLine() ?? "6";

            switch (choice)
            {
                case "1":
                    Task1();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "2":
                    Task2();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "3":
                    Task3();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "4":
                    Task4();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case "5":
                    Task5();
                    Console.ReadLine();
                    Console.Clear();
                    continue;    
                case "6":
                    return;
                default:
                    Console.WriteLine("ERROR.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
            }
        }
    }

    static void Task1()
    {
        string inputFilePath = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\input.txt"; // Вхідний файл
        string outputFilePath = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\output.txt"; // Вихідний файл

        // Зчитування тексту з файлу
        string[] lines = File.ReadAllLines(inputFilePath);

        List<string> validLines = new List<string>();
        int subtextCount = 0;

        // Зчитування параметрів заміни від користувача
        Console.Write("Введіть текст для заміни: ");
        string textToReplace = Console.ReadLine();
        Console.Write("Введіть текст для заміни на: ");
        string replacementText = Console.ReadLine();

        // Обробка рядків
        foreach (string line in lines)
        {
            if (IsMeaningful(line))
            {
                string processedLine = line;
                if (processedLine.Contains(textToReplace))
                {
                    processedLine = processedLine.Replace(textToReplace, replacementText);
                }
                validLines.Add(processedLine);
                subtextCount++;
            }
        }

        // Запис у новий файл
        File.WriteAllLines(outputFilePath, validLines);

        // Вивід кількості підрядків
        Console.WriteLine($"Кількість підрядків: {subtextCount}");
    }

    // Метод для перевірки осмисленості рядка
    static bool IsMeaningful(string line)
    {
        // Простий приклад: рядок має містити хоча б одне слово (можна доповнити додатковими умовами)
        return !string.IsNullOrWhiteSpace(line) && Regex.IsMatch(line, @"\w+");
    }

    static void Task2()
    {
        string inputFilePath = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\task2.txt"; // Вхідний файл
        string outputFilePath = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\output.txt"; // Вихідний файл

        // Зчитування тексту з файлу
        string[] lines = File.ReadAllLines(inputFilePath);

        List<string> processedLines = new List<string>();

        foreach (string line in lines)
        {
            string processedLine = ProcessLine(line);
            processedLines.Add(processedLine);
        }

        // Запис у новий файл
        File.WriteAllLines(outputFilePath, processedLines);
        
        Console.WriteLine("Обробка завершена. Результати записано у output.txt.");
    }

    static string ProcessLine(string line)
    {
        // Видалення слів з закінченням "re", "nd" та "less"
        string patternToRemove = @"\b\w*(re|nd|less)\b";
        string processedLine = Regex.Replace(line, patternToRemove, "", RegexOptions.IgnoreCase);

        // Замінюємо слова з префіксом "to" на "at"
        string patternToReplace = @"\bto(\w*)\b";
        processedLine = Regex.Replace(processedLine, patternToReplace, "at$1", RegexOptions.IgnoreCase);

        // Видалення зайвих пробілів, що можуть виникнути після видалення слів
        processedLine = Regex.Replace(processedLine, @"\s+", " ").Trim();

        return processedLine;
    }

    static void Task3()
    {
        string inputFilePath1 = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\input1.txt"; // Вхідний файл 1
        string inputFilePath2 = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\input2.txt"; // Вхідний файл 2
        string outputFilePath = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\output.txt"; // Вихідний файл

        // Зчитування тексту з файлів
        string text1 = File.ReadAllText(inputFilePath1);
        string text2 = File.ReadAllText(inputFilePath2);

        // Отримання слів з тексту
        HashSet<string> words1 = new HashSet<string>(GetWords(text1));
        HashSet<string> words2 = new HashSet<string>(GetWords(text2));

        // Визначення слів, які є в першому тексті, але відсутні в другому
        words1.ExceptWith(words2);

        // Створення третього тексту
        string resultText = string.Join(" ", words1);

        // Запис у новий файл
        File.WriteAllText(outputFilePath, resultText);

        Console.WriteLine("Обробка завершена. Результати записано у output.txt.");
    }

    static IEnumerable<string> GetWords(string text)
    {
        // Використання регулярного виразу для отримання слів з тексту
        MatchCollection matches = Regex.Matches(text, @"\b[\w']+\b");
        foreach (Match match in matches)
        {
            yield return match.Value.ToLower(); // Приведення до нижнього регістру для коректного порівняння
        }
    }

    static void Task4()
    {
        string binaryFilePath = "C:\\Users\\pc\\Desktop\\csharplab8-Novinska\\Lab8CSharp\\data.bin"; // Двійковий файл

        Console.Write("Введіть кількість дійсних чисел: ");
        int n = int.Parse(Console.ReadLine());

        double[] numbers = new double[n];

        Console.WriteLine("Введіть числа:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Число {i + 1}: ");
            numbers[i] = double.Parse(Console.ReadLine());
        }

        // Запис чисел у двійковий файл
        using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
        {
            foreach (double number in numbers)
            {
                writer.Write(number);
            }
        }

        // Зчитування чисел з файлу та знаходження максимального серед непарних позицій
        double maxOddIndexValue = double.MinValue;
        using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
        {
            int index = 0;
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                double number = reader.ReadDouble();
                if (index % 2 == 0) // Непарні позиції у масиві (0-based index)
                {
                    if (number > maxOddIndexValue)
                    {
                        maxOddIndexValue = number;
                    }
                }
                index++;
            }
        }

        Console.WriteLine($"Максимальне значення серед чисел на непарних позиціях: {maxOddIndexValue}");
    }

static void Task5()
    {
        Console.WriteLine("Task 5\n");

        string studentName = "Novinska";
        string folder1Path = $"D:\\temp\\{studentName}1";
        string folder2Path = $"D:\\temp\\{studentName}2";
        string allFolderPath = $"D:\\temp\\ALL";

        // Task1: Створення директорій
        Directory.CreateDirectory(folder1Path);
        Directory.CreateDirectory(folder2Path);

        // Task2: Створення файлів t1.txt та t2.txt
        string t1FilePath = Path.Combine(folder1Path, "t1.txt");
        string t2FilePath = Path.Combine(folder1Path, "t2.txt");

        string t1Text = "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми";
        string t2Text = "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ";

        File.WriteAllText(t1FilePath, t1Text);
        File.WriteAllText(t2FilePath, t2Text);

        // Task3: Створення файлу t3.txt та запис у нього вмісту t1.txt та t2.txt
        string t3FilePath = Path.Combine(folder2Path, "t3.txt");
        string t3Content = File.ReadAllText(t1FilePath) + "\n" + File.ReadAllText(t2FilePath);
        File.WriteAllText(t3FilePath, t3Content);

        // Task4: Виведення інформації про файли t1.txt, t2.txt, t3.txt
        PrintFileInfo(t1FilePath);
        PrintFileInfo(t2FilePath);
        PrintFileInfo(t3FilePath);

        // Task5: Переміщення файлу t2.txt у папку <прізвище_студента>2
        string moveT2FilePath = Path.Combine(folder2Path, "t2.txt");
        if (File.Exists(moveT2FilePath))
        {
            File.Delete(moveT2FilePath);
        }
        File.Move(t2FilePath, moveT2FilePath);

        // Task6: Копіювання файлу t1.txt у папку <прізвище_студента>2
        string copyT1FilePath = Path.Combine(folder2Path, "t1.txt");
        if (File.Exists(copyT1FilePath))
        {
            File.Delete(copyT1FilePath);
        }
        File.Copy(t1FilePath, copyT1FilePath);

        // Task7: Переміщення папки <прізвище_студента>1 у ALL
        if (Directory.Exists(allFolderPath))
        {
            Directory.Delete(allFolderPath, true);
        }
        Directory.Move(folder1Path, allFolderPath);

        // Task8: Виведення інформації про файли у папці ALL
        Console.WriteLine("\nFiles in All directory:");
        string[] filesInAll = Directory.GetFiles(allFolderPath);
        foreach (string file in filesInAll)
        {
            PrintFileInfo(file);
        }
    }

    static void PrintFileInfo(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        Console.WriteLine($"File Name: {fileInfo.Name}");
        Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
        Console.WriteLine($"Size (bytes): {fileInfo.Length}");
        Console.WriteLine($"Created: {fileInfo.CreationTime}");
        Console.WriteLine($"Last Modified: {fileInfo.LastWriteTime}");
        Console.WriteLine();
    }
}