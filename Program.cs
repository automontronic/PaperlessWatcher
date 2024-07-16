using PaperlessWatcher;

// Specify the directory to monitor
string path = System.Configuration.ConfigurationManager.AppSettings["WatchFolder"].ToString();

// Create a new FileSystemWatcher instance
FileSystemWatcher watcher = new FileSystemWatcher(path);

// Subscribe to the events you are interested in
watcher.Created += OnFileCreated;

// Enable the watcher
watcher.EnableRaisingEvents = true;

// Send a message to the console
Console.WriteLine($"Watching for new files in: {path}");
Console.WriteLine("Press 'q' to quit.");

// Wait for the user to quit
while (Console.ReadKey().Key != ConsoleKey.Q)
{
    // Do nothing, just keep the application running
}

// When a new file is added to the folder
static async void OnFileCreated(object sender, FileSystemEventArgs e)
{
    try
    {
        // Get the watcher
        FileSystemWatcher fileSystemWatcher = (FileSystemWatcher)sender;

        // Display the file created
        Console.WriteLine($"New file created: {e.FullPath}");

        // Wait a few seconds to ensure any lock is released
        Console.WriteLine("Waiting a couple seconds");
        System.Threading.Thread.Sleep(3000);

        // Create a new Paperless instance
        Paperless paperless = new Paperless();
       
        // Upload the file to Paperless
        await paperless.Upload(e.FullPath);
        
        // The file has been uplaoded
        Console.WriteLine("Uploaded");
    }
    catch (Exception ex) // If there was an error
    {
        // Alert on error
        Console.WriteLine("There was an error. Waiting a couple seconds and trying again.");
        try
        {
            // Try the upload one more time
            System.Threading.Thread.Sleep(3000);

            // Create a new Paperless instance
            Paperless paperless = new Paperless();

            // Upload the file to Paperless
            await paperless.Upload(e.FullPath);

            // The file has been uplaoded
            Console.WriteLine("Uploaded");
        }
        catch (Exception ex2) // The file cannot be uploaded
        {
            Console.WriteLine("There was an error again. The file will be skipped");

        }
    }
}