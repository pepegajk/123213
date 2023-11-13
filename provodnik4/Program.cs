using System;
using System.IO;

class Program
{
    static void Main()
    {

        Console.WriteLine("Enter - Выбрать действие");
        Console.WriteLine("Escape - Выйти из меню");

        ConsoleKey key = ArrowKeys.GetArrowKeyPressed();

        string currentPath = "C:\\";
        int maxMenuSelection = 10000;
        int menuSelection = 0;
        while (key != ConsoleKey.Escape)
        {
            if (key == ConsoleKey.UpArrow)
            {
                if (menuSelection > 0)
                {
                    menuSelection--;
                }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (menuSelection < maxMenuSelection) 
                {
                    menuSelection++;
                }
            }
            else if (key == ConsoleKey.Enter)
            {
                
                if (currentPath == "C:\\")
                {
                    FileExplorer.DisplayDrives();
                    string driveSelection = Console.ReadLine(); 
                    currentPath = currentPath + driveSelection + "\\";
                }
                else
                {
                    FileExplorer.DisplayFoldersAndFiles(currentPath);
                    string fileSelection = Console.ReadLine(); 
                    string newPath = Path.Combine(currentPath, fileSelection);
                    if (Directory.Exists(newPath))
                    {
                        currentPath = newPath + "\\";
                    }
                    else if (File.Exists(newPath))
                    {
                        FileExplorer.OpenFile(newPath);
                    }
                }
            }

            key = ArrowKeys.GetArrowKeyPressed();
        }
    }
}

static class FileExplorer
{
    public static void DisplayDrives()
    {
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        foreach (DriveInfo d in allDrives)
        {
            Console.WriteLine($"Drive {d.Name}");
            Console.WriteLine($"  File type: {d.DriveType}");
            if (d.IsReady)
            {
                Console.WriteLine($"  Available space: {d.TotalFreeSpace / 1024 / 1024 / 1024} GB");
                Console.WriteLine($"  Total space: {d.TotalSize / 1024 / 1024 / 1024} GB");
            }
        }
    }

    public static void DisplayFoldersAndFiles(string path)
    {
        string[] dirs = Directory.GetDirectories(path);
        foreach (string dir in dirs)
        {
            Console.WriteLine($"Folder: {dir}");
        }

        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            Console.WriteLine($"File: {file}");
        }
    }

    public static void OpenFile(string filePath)
    {
       
    }
}

static class ArrowKeys
{
    public static ConsoleKey GetArrowKeyPressed()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        while (keyInfo.Key != ConsoleKey.UpArrow && keyInfo.Key != ConsoleKey.DownArrow && keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape)
        {
            keyInfo = Console.ReadKey();
        }
        return keyInfo.Key;
    }
}