using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileManager2
{
    /// <summary>
    /// Класс для работы с файловой системой.
    /// </summary>
    class FileManager
    {
        //Создание.
        /// <summary>
        /// Создание файла.
        /// </summary>
        /// <param name="path">Путь по которому вы желаете создать файл.</param>
        /// <param name="name">Имя создаваемого файла.</param>
        /// <returns></returns>
        public static FileInfo CreateFile(string path, string name)
        {
            FileInfo fi = new FileInfo(path + "\\" + name);
            using (FileStream fs = fi.Create()) { };
            return fi;
        }
        /// <summary>
        /// Создание директории.
        /// </summary>
        /// <param name="path">Путь по которому вы желаете создать директорию.</param>
        /// <param name="name">Имя директории.</param>
        /// <returns></returns>
        public static DirectoryInfo CreateDir(string path,string name)
        {
            DirectoryInfo di = new DirectoryInfo(path + "\\" + name);
            di.Create();
            return di;
        }
        //Переименование.
        /// <summary>
        /// Переименование файла.
        /// </summary>
        /// <param name="fi">Файл какоторый вы желаете переименовать.</param>
        /// <param name="newname">Новое имя файла.</param>
        public static void RenameF(FileInfo fi , string newname)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(fi.FullName, newname);
        }
        /// <summary>
        /// Переименование директории.
        /// </summary>
        /// <param name="fi">Директория которую вы желаете переименовать.</param>
        /// <param name="newname">Новое имя директории.</param>
        public static void RenameD(FileSystemInfo fi, string newname)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(fi.FullName, newname);
        }
        /// <summary>
        /// Открытие файла или директории.
        /// </summary>
        /// <param name="fis">Открываемый объект.</param>
        /// <returns></returns>
        public static DirectoryInfo Open(FileSystemInfo fis)
        {
            if (File.Exists(fis.FullName))
            {
                Process.Start(fis.FullName);
                return null;
            }
            else if (Directory.Exists(fis.FullName))
            {
                DirectoryInfo d = (DirectoryInfo)fis;
                return d;
            }
            return null;
        }
        /// <summary>
        /// Возврат к предидущему каталогу.
        /// </summary>
        /// <param name="path">Путь к текущему каталогу.</param>
        /// <returns></returns>
        public static DirectoryInfo Back(string path)
        {
            int end = 0;

            for(int i = path.Length-1; i >= 0; i--)
            {
                if (path[i] == '\\')
                {
                    end = i;
                    break;
                }
            }
            string newpath = path.Remove(end, (path.Length - end));
            DirectoryInfo dir = new DirectoryInfo(newpath);
            return dir;
        }
        //Удаление.
        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="f">Файл который нужно удалить.</param>
        public static void DelF(FileInfo f)
        {
            f.Delete();
        }
        /// <summary>
        /// Удаление директории.
        /// </summary>
        /// <param name="d">Директория которую нужно удалить.</param>
        public static void DelD(DirectoryInfo d)
        {
            int select = 0;
            string[] arr = new string[2] { "Да", "Нет" };
            ConsoleKeyInfo choice;
            if (Directory.EnumerateFiles(d.FullName, "*.*", SearchOption.AllDirectories).Any())
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Папка не пуста, всеравно удалить? ");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i == select)
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
                    if (choice.Key == ConsoleKey.UpArrow && select > 0)
                    {
                        select--;
                    }
                    else if (choice.Key == ConsoleKey.DownArrow && select < (arr.Length - 1))
                    {
                        select++;
                    }
                }while(choice.Key != ConsoleKey.Enter);
                if (select == 0)
                {
                    d.Delete(true);
                }
                else
                {
                    return;
                }

            }
            else
            {
                d.Delete();
            }
        }
    }
}
