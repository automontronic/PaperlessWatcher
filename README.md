# Paperless-ngx Watcher
Paperless-ngx is document management system and can be found at https://github.com/paperless-ngx/paperless-ngx. 

The Paperless-ngx Watcher is a file system watcher given the ability to automatically upload files into Paperlesss. The user configures the location to being watched. This can be useful when setting up a scanner to store documents in a folder. Once the document is scanned, it is automatically archived and then becomes searchable within Paperless. 

## Installation Instructions
1. Download the latest release 
2. Unzip the package
3. Modify the .config file to include
   - Your authorization token
   - The watch folder location
   - The URL to your Paperless-ngx installation
4. Run PaperlessWatcher.exe
