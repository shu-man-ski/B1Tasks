using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace B1Tasks
{
    internal class Program
    {
        const string globalFolderPath = "D:\\B1TestTask";
        const string filesFolderPath = globalFolderPath + "\\Files";
        static int fileCount = 100;

        static void Main(string[] args)
        {
            // 1.	Сгенерировать 100 текстовых файлов со следующей структурой, каждый из которых содержит 100 000 строк
            //GenerateFiles(filesFolderPath, fileCount);
            Console.WriteLine("Files was successfully created");

            // 2.	Реализовать объединение файлов в один
            //MergeFiles(filesFolderPath, globalFolderPath, "abc");
            Console.WriteLine("File was successfully merged");

            // 3. Импорт файла
            ImportFile(filesFolderPath + "\\1.txt");
            Console.WriteLine("File was successfully imported");
        }

        static void GenerateFiles(string filesFolderPath, int fileCount)
        {
            if (!Directory.Exists(filesFolderPath))
            {
                Directory.CreateDirectory(filesFolderPath);
            }

            var randomStringGenerator = new RandomStringGenerator()
                .SetYears(5)
                .SetLinesNumber(100_000)
                .SetSymbolsNumber(10)
                .SetIntNumberRange(1, 100_000_000)
                .SetDoubleNumberRange(1d, 20d);
            for (int i = 1; i <= fileCount; i++)
            {
                var fileName = i + ".txt";
                var filePath = string.Format("{0}\\{1}", filesFolderPath, fileName);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    string line = randomStringGenerator.GenerateRandomStrings();
                    writer.WriteLine(line);
                }
            }
        }

        static void MergeFiles(string filesFolderPath, string unionfielFolderPath, string? exclusionString = null)
        {
            var unionFileName = "union.txt";
            var unionFilePath = string.Format("{0}\\{1}", unionfielFolderPath, unionFileName);
            using (StreamWriter writer = new StreamWriter(unionFilePath))
            {
                for (int i = 1; i <= fileCount; i++)
                {
                    var fileName = i + ".txt";
                    var filePath = string.Format("{0}\\{1}", filesFolderPath, fileName);
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        if (!string.IsNullOrEmpty(exclusionString))
                        {
                            var lines = reader
                                .ReadToEnd()
                                .Split('\n')
                                .Where(line => !line.Contains(exclusionString))
                                .ToList();
                            foreach (var line in lines)
                            {
                                writer.WriteLine(line);
                            }
                        }

                        writer.Write(reader.ReadToEnd());
                    }
                }
            }
        }

        static void ImportFile(string filePath)
        {
            using (AppContext context = new AppContext())
            {
                List<string> rows = File.ReadAllLines(filePath).ToList();
                int i = 0;
                foreach (var row in rows)
                {
                    var modelData = row.Split("||");
                    context.ImportTable.Add(new ImportTableModel
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Parse(modelData[0]),
                        LathinString = modelData[1],
                        RussianString = modelData[2],
                        IntNumber = int.Parse(modelData[3]),
                        DecimalNumber = double.Parse(modelData[4])
                    });
                    context.SaveChanges();
                    Console.WriteLine(string.Format("{0}/{1} rows imported", ++i, rows.Count));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }
            }
        }
    }
}
