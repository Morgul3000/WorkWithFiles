using System;
using System.IO;

namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long a = 0;
            long b = SizeDirectory(@"text.txt", ref a);
            Console.WriteLine(b);
        }
        static long SizeDirectory(string path, ref long sizeDir)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] dirList = dir.GetDirectories();  //получаем список подпапок в директории
                FileInfo[] fileList = dir.GetFiles();            //получаем список файлов в директории

                foreach (FileInfo file in fileList)               //считаем размер всех файлов в дирректории
                {
                    sizeDir += file.Length;
                }

                foreach (DirectoryInfo directory in dirList)     //проходим циклом по каждой подпапке, на каждой итерации рекурсивно
                {                                                //запускаем метод
                    SizeDirectory(directory.FullName, ref sizeDir);
                }
                return sizeDir;
            }

            catch (ArgumentException ex)
            {
                Console.WriteLine($"Путь к дирректории не может быть пустым {ex.Message}");  //Отработка исключения "пустого" пути
                return 0;
            }
            catch (DirectoryNotFoundException ex)                    //Отработка исключения в случае отсутствия папки 
            {
                Console.WriteLine($"Папка не найдена {ex.Message}");
                return 0;
            }

            catch (UnauthorizedAccessException ex)                   //отработка исключения в случае отказа доступа
            {
                Console.WriteLine($"Нет доступа к файлам {ex.Message}");
                return 0;
            }
            catch (Exception ex)                                     //отработка любых других исключений
            {
                Console.WriteLine($"Произошла ошибка {ex.Message}");
                return 0;
            }
        }
    }
}
