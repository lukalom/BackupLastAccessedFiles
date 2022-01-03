using System;
using System.IO;

namespace FileBackup
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string pathDir = Directory.GetCurrentDirectory();
            pathDir = Path.Combine(pathDir, "test");

            //DateTime time = DateTime.Now.AddHours(-10);
            DateTime time = DateTime.Now;

            string backup = Path.Combine(Directory.GetCurrentDirectory(), "backup");

            BackupFiles(pathDir, time, backup);

        }

        public static void BackupFiles(string pathDir, DateTime inputTime, string backupPath)
        {
            try
            {
                if (Directory.Exists(pathDir))
                {
                    string[] dirFiles = Directory.GetFiles(pathDir);

                    if (dirFiles.Length == 0)
                    {
                        throw new FileNotFoundException("File not found");
                    }
                    else
                    {

                        int filesMoved = 0;

                        if (!Directory.Exists(backupPath))
                        {
                            Directory.CreateDirectory(backupPath);
                        }

                        foreach (var file in dirFiles)
                        {

                            DateTime fileAccessTime = File.GetLastAccessTime(file);

                            if (DateTime.Compare(fileAccessTime, inputTime) < 0)
                            {
                                string backupPathFile = Path.Combine(backupPath, Path.GetFileName(file));

                                File.Move(file, backupPathFile);


                                filesMoved++;
                            }

                        }

                        if (filesMoved > 0)
                        {
                            Console.WriteLine($"Moved {filesMoved} files");
                        }

                    }

                }
                else
                {
                    throw new DirectoryNotFoundException("Dir not found");
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.Message);
            }

        }
    }
}
