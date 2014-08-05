using System;
using System.IO;

namespace GistClient.FileSystem
{
    public static class FileReader
    {
        public static String GetContent(String filepath){
            using (var fileStream = new FileStream(filepath, FileMode.Open)){
                using (var reader = new StreamReader(fileStream)){
                    String content = reader.ReadToEnd();
                    fileStream.Dispose();
                    return content;
                }
            }
        }

        public static String GetFileName(String filepath){
            return Path.GetFileName(filepath);
        }

        public static string GetFileDescription(String filepath){
            return "File " + GetFileName(filepath) + ", uploaded by WinGists.";
        }
    }
}