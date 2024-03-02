using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EventManagement.Model;
using EventManagement.Repositories;

namespace EventManagement.Services
{

    public class UploadHandler<T> : IUploadHandler<T> where T : class
    {
     
        public string UploadFile(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);
            if (!ext.Equals(".xlsx"))
                throw new Exception($"{ext} is not a valid extension");

            string uniqueFileName = Guid.NewGuid().ToString() + ext;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Upload", uniqueFileName);
            System.Console.WriteLine(path);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            System.Console.WriteLine("File added successfully");
            return path;
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    System.Console.WriteLine("Deleted Successfully");
                    File.Delete(filePath);
                }
                else
                {
                    Console.WriteLine("File does not exist at the specified path.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public async Task<List<T>> ReadDetailsFromExcel(string filePath)
        {
            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);

            List<T> detailsList = new List<T>();

            var headers = worksheet.FirstRowUsed().CellsUsed(c => !EqualityComparer<XLCellValue>.Default.Equals(c.Value, default(XLCellValue))).Select(c => c.GetString()).ToList();

            foreach (var row in worksheet.RowsUsed().Skip(1))
            {
                var details = Activator.CreateInstance<T>();

                for (int i = 0; i < headers.Count; i++)
                {
                    var propertyName = headers[i];
                    var property = typeof(T).GetProperty(propertyName);
                    System.Console.WriteLine(property);
                    if (property != null)
                    {

                        var cellValue = row.Cell(i + 1).Value;
                        // System.Console.WriteLine(Convert.ToDateTime(cellValue.ToString()));
                        if (property.PropertyType == typeof(int))
                        {
                            if (int.TryParse(cellValue.ToString(), out int intValue))
                            {
                                property.SetValue(details, intValue);
                            }
                        }
                        else if(property.PropertyType==typeof(DateTime)){
                            property.SetValue(details,cellValue.GetDateTime());
                        }
                        else
                        {
                            property.SetValue(details, cellValue.ToString());
                        }
                    }
                }

                detailsList.Add(details);
            }

            return detailsList;
        }


    }
}

