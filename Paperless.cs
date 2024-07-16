using System.Net.Http.Headers;

namespace PaperlessWatcher
{
    public class Paperless
    {
        /// <summary>
        ///  Upload a file to Paperless
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task Upload(string path)
        {
            // Create a new HTTP Client with credentials
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", System.Configuration.ConfigurationManager.AppSettings["AuthorizationToken"].ToString());
          
            // Create a new form
            using (var form = new MultipartFormDataContent())
            {
                /// Open the file
                using (var fs = File.OpenRead(path))
                {
                    // Open teh stream
                    using (var streamContent = new StreamContent(fs))
                    {
                        // Write the content
                        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        {
                            // Add the headers
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            
                            // Add the file
                            form.Add(fileContent, "document", Path.GetFileName(path));

                            // Send the file
                            var response = await client.PostAsync($"{System.Configuration.ConfigurationManager.AppSettings["PaperlessURL"].ToString()}/api/documents/post_document/", form);
                         }
                    }
                }
            }
        }
    }
}
