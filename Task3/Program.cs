using System;
using System.IO;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"C:\Users\udav\Desktop\Rufus — копия";
                DirectoryInfo dir = new DirectoryInfo(path);
                long sizeDisk = 0;

                DirectoryInfo[] foldersList = dir.GetDirectories();   //получаем список подпапок в директории
                FileInfo[] filesList = dir.GetFiles();                //получаем список файлов в директории

                sizeDisk = SizeDirectory(path, ref sizeDisk);
                Console.WriteLine($"Исходный размер папки {sizeDisk} байт");

                Console.WriteLine("Папки");
                foreach (DirectoryInfo folder in foldersList)         //Проходим циклом по подпапкам
                {
                    Console.WriteLine($"Имя {folder.Name}, дата создания {folder.CreationTime}");
                    TimeSpan timeSpan = DateTime.Now - folder.LastAccessTime;
                    if (timeSpan.Minutes > 30)                       //Проверка интервала времени с последнего обращения
                    {
                        folder.Delete(true);
                        Console.WriteLine($"Папка {folder} удалена");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Файлы");
                foreach (FileInfo file in filesList)                 //проходимся циклом по файлам
                {
                    Console.WriteLine($"Имя {file.Name}, дата создания {file.CreationTime}, размер {file.Length} байт");
                    TimeSpan timeSpan = DateTime.Now - file.LastAccessTime;
                    if (timeSpan.Minutes > 30)                       //Проверка интервала времени с последнего обращения
                    {
                        file.Delete();
                        Console.WriteLine($"Файл {file} удален");
                    }
                }

                long sizeDiskClear = 0;                                        //обнуления счетчика
                sizeDiskClear = SizeDirectory(path, ref sizeDiskClear);

                Console.WriteLine($"\nОсвобождено {sizeDisk - sizeDiskClear} байт");
                Console.WriteLine($"Текущий размер папки {sizeDisk} байт");
            }

            catch (ArgumentException ex)
            {
                Console.WriteLine($"Путь к дирректории не может быть пустым {ex.Message}");  //Отработка исключения "пустого" пути
            }
            catch (DirectoryNotFoundException ex)                     //Отработка исключения в случае отсутствия папки 
            {
                Console.WriteLine($"Папка не найдена {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)                   //отработка исключения в случае отказа доступа
            {
                Console.WriteLine($"Нет доступа к файлам {ex.Message}");
            }
            catch (Exception ex)                                     //отработка любых других исключений
            {
                Console.WriteLine($"Произошла ошибка {ex.Message}");
            }
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
