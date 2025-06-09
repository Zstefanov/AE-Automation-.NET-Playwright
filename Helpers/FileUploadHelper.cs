using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_extensive_project.Helpers
{
    //helper file for uploading products (Test_Search_Products) 
    //separate class for scalability in case of project growth
    public static class FileUploadHelper
    {
        public static async Task UploadFileAsynch(IPage page, string selector, string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found at path: {filePath}");

            await page.SetInputFilesAsync(selector, filePath);
        }
    }
}
