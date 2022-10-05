using System;
using System.IO;

namespace FSUnit8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                DirectoryInfo dir = new DirectoryInfo(@"");
                    DirectoryInfo[] foldersList = dir.GetDirectories();   //получаем список подпапок в директории
                    FileInfo[] filesList = dir.GetFiles();                //получаем список файлов в директории

                    Console.WriteLine("Папки");
                    foreach (DirectoryInfo folder in foldersList)         //Проходим циклом по подепапкам
                    {
                        Console.WriteLine($"Имя {folder.Name}, Дата создания {folder.CreationTime}");
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
                        Console.WriteLine($"Имя {file.Name}, Дата создания {file.CreationTime} Дата последнего изменения {file.Length}");
                        TimeSpan timeSpan = DateTime.Now - file.LastAccessTime;
                        if (timeSpan.Minutes > 30)                       //Проверка интервала времени с последнего обращения
                        {
                            file.Delete();
                            Console.WriteLine($"Файл {file} удален");
                        }
                    }

            }
            catch(ArgumentException ex) 
            {
                Console.WriteLine($"Путь к дирректории не может быть пустым {ex.Message}");
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
    }
}
