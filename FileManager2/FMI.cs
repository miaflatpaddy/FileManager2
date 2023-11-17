using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace FileManager2
{
    class FMI
    {
        /// <summary>
        /// Работа с дисками.
        /// </summary>
        public void selectdrive()
        {

            DriveInfo[] drives_ = DriveInfo.GetDrives();
            int j = 0;
            for (int i = 0; i < drives_.Length; i++)
            {
                if (drives_[i].IsReady)
                {
                    j++;
                }
            }
            DriveInfo[] drives = new DriveInfo[j];
            j = 0;
            for (int i = 0; i < drives_.Length; i++)
            {
                if (drives_[i].IsReady)
                {
                    drives[j] = drives_[i];
                    j++;
                }
            }
            ConsoleKeyInfo choice;
            int select = 0;
            while (true)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Выберите диск: ");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < drives.Length; i++)
                    {
                        if (drives[i].IsReady)
                        {
                            if (i == select)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine($"{drives[i].Name}\t {((drives[i].TotalFreeSpace / 1024) / 1024)} MB/ {((drives[i].TotalSize / 1024) / 1024)} MB");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"{drives[i].Name}\t {((drives[i].TotalFreeSpace / 1024) / 1024)} MB/ {((drives[i].TotalSize / 1024) / 1024)} MB");
                            }

                        }
                    }
                    choice = Console.ReadKey();
                    if (choice.Key == ConsoleKey.UpArrow && select > 0)
                    {
                        select--;
                    }
                    else if (choice.Key == ConsoleKey.DownArrow && select < drives.Length)
                    {
                        select++;
                    }
                    else if (choice.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                } while (choice.Key != ConsoleKey.Enter);
                FMUI(drives[select].RootDirectory);
            }
        }
        /// <summary>
        /// Пользовательский интерфейс.
        /// </summary>
        /// <param name="dir">Директория с которой планируется начать.</param>
        public void FMUI(DirectoryInfo dir)
        {
            ConsoleKeyInfo choice;
            int select, select1 = 0 ,j;
            string name;
            DirectoryInfo dir_ = dir;
            DirectoryInfo test;
            while (true)
            {
                select = 0;
                j = 0;
                DirectoryInfo[] content = dir_.GetDirectories();
                FileInfo[] content_ = dir_.GetFiles();
                string[] paths = new string[content.Length+content_.Length+2];
                paths[0] = "...";
                paths[paths.Length - 1] = "Создать...";
                for(int i = 1; i < paths.Length-1; i++)
                {
                    if(i <= content.Length)
                    {
                        paths[i] = ($"{content[i - 1].Name,-60} {content[i - 1].LastWriteTime,-20} {" ",-10} {content[i - 1].Attributes,-20}");
                    }
                    else
                    {
                        paths[i] = ($"{content_[j].Name,-60} {content_[j].LastWriteTime,-20} {(content_[j].Length/1024)+" KB",-10} {content_[j].Attributes,-20}");
                        j++;
                    }
                }
                do
                {
                    Console.Clear();
                    Console.WriteLine(dir_.FullName);
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"{"Имя файла",-60} {"Последнее изменение",-20} {"Размер",-10} {"Аттрибуты",-20}");
                    for (int i = 0; i < paths.Length; i++)
                    {
                        if (i == select)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(paths[i]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine(paths[i]);
                        }
                    }

                    choice = Console.ReadKey();

                    if (choice.Key == ConsoleKey.Escape)
                    {
                        return;
                    }
                    else if (choice.Key == ConsoleKey.UpArrow && select > 0)
                    {
                        select--;
                    }
                    else if (choice.Key == ConsoleKey.DownArrow && select < paths.Length)
                    {
                        select++;
                    }
                } while (choice.Key != ConsoleKey.Enter);
                if (select != 0 && select != (paths.Length - 1))
                {
                    if (select <= content.Length)
                    {
                        test = Filemenu(content[select - 1]);
                        if (test != null)
                        {
                            dir_ = test;
                        }
                    }
                    else if(select > content.Length)
                    {
                        test = Filemenu(content_[(select-content.Length) - 1]);
                        if (test != null)
                        {
                            dir_ = test;
                        }
                    }
                }
                else if (select == 0)
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    for(int i = 0; i < drives.Length; i++)
                    {
                        if(dir_.FullName == drives[i].Name)
                        {
                            return;
                        }
                    }
                    dir_ = FileManager.Back(dir_.FullName);
                }
                else if (select == (paths.Length - 1))
                {
                    string[] arr = new string[2] { "Папку", "Файл" };
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Создать...");
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (i == select1)
                            {
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.WriteLine(arr[i]);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine(arr[i]);
                            }
                        }
                        choice = Console.ReadKey();
                        if (choice.Key == ConsoleKey.UpArrow && select1 > 0)
                        {
                            select1--;
                        }
                        else if (choice.Key == ConsoleKey.DownArrow && select1 < (arr.Length - 1))
                        {
                            select1++;
                        }
                    } while (choice.Key != ConsoleKey.Enter);
                    Console.Clear();
                    Console.WriteLine("Введите имя: ");
                    name = Console.ReadLine();
                    if (select1 == 0)
                    {
                        FileManager.CreateDir(dir_.FullName, name);
                    }
                    else
                    {
                        FileManager.CreateFile(dir_.FullName, name);
                    }
                }
            }
        }
        /// <summary>
        /// Меню для работы с файлом.
        /// </summary>
        /// <param name="finf">Файл над которым издеваемся.</param>
        /// <returns></returns>
        public DirectoryInfo Filemenu(FileSystemInfo finf)
        {
            string name;
            int select1 = 0;
            ConsoleKeyInfo choice;
            string[] arr = new string[3] { "Открыть", "Удалить", "Переименовать" };
            do
            {
                Console.Clear();
                Console.WriteLine(finf.Name);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == select1)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(arr[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(arr[i]);
                    }
                }
                choice = Console.ReadKey();
                if (choice.Key == ConsoleKey.UpArrow && select1 > 0)
                {
                    select1--;
                }
                else if (choice.Key == ConsoleKey.DownArrow && select1 < (arr.Length - 1))
                {
                    select1++;
                }
            } while (choice.Key != ConsoleKey.Enter);
            if (select1 == 0)
            {
                return FileManager.Open(finf);
            }
            else if (select1 == 1)
            {
                if (File.Exists(finf.FullName))
                {
                    FileManager.DelF((FileInfo)finf);
                }
                else if (Directory.Exists(finf.FullName))
                {
                    FileManager.DelD((DirectoryInfo)finf);
                }
            }
            else if (select1 == 2) {
                Console.Clear();
                Console.WriteLine("Введите новое имя:");
                name = Console.ReadLine();
                if (File.Exists(finf.FullName))
                {
                    FileManager.RenameF((FileInfo)finf, name);
                }
                else if (Directory.Exists(finf.FullName))
                {
                    FileManager.RenameD((DirectoryInfo)finf, name);
                }
            }
            return null;
        }

    }
}
