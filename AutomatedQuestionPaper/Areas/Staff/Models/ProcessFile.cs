using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CsvHelper;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class ProcessFile
    {
        public static bool SaveFile(HttpPostedFileBase file, int type)
        {
            string path;
            string targetPath;

            if (type == 0)
            {
                path = HttpContext.Current.Server.MapPath("~/Areas/Staff/Uploads/Word");
                targetPath = Path.Combine(path, file.FileName);
                file.SaveAs(targetPath);
                return true;
            }

            if (type == 1)
            {
                path = HttpContext.Current.Server.MapPath("~/Areas/Staff/Uploads/CSV");
                targetPath = Path.Combine(path, file.FileName);
                file.SaveAs(targetPath);
                return true;
            }

            if (type == 2)
            {
                path = HttpContext.Current.Server.MapPath("~/Areas/Staff/Uploads/Excel");
                targetPath = Path.Combine(path, file.FileName);
                file.SaveAs(targetPath);
                return true;
            }

            return false;
        }

        public static bool ValidateCsvFile(HttpPostedFileBase file)
        {
            var csv = new CsvReader(
                File.OpenText(HttpContext.Current.Server.MapPath($"~/Areas/Staff/Uploads/CSV/{file.FileName}")));
            csv.Read();

            var headers = csv.FieldHeaders.ToList();

            if (headers.Count != 2)
            {
                csv.Dispose();
                return false;
            }

            if (!(headers[0] == "Question" && headers[1] == "Level"))
            {
                csv.Dispose();
                return false;
            }

            csv.Dispose();
            return true;
        }


        public static List<QuestionFormat> WordFile(HttpPostedFileBase file)
        {
            SaveFile(file, 0);
            return null;
        }

        public static (List<QuestionFormatCsv> questions, string error) CsvFile(HttpPostedFileBase file)
        {
            if (SaveFile(file, 1))
            {
                if (ValidateCsvFile(file))
                {
                    var csv = new CsvReader(
                        File.OpenText(
                            HttpContext.Current.Server.MapPath($"~/Areas/Staff/Uploads/CSV/{file.FileName}")));
                    var records = csv.GetRecords<QuestionFormatCsv>().ToList();
                    csv.Dispose();
                    DeleteFile(file, 1);
                    return (records, "Success");
                }

                DeleteFile(file, 1);
                return (null, "The file is not in correct format. Please check uploaded file");
            }

            return (null, "Something went wrong. File was not save successfully. Try again later");
        }

        public static List<QuestionFormat> ExcelFile(HttpPostedFileBase file)
        {
            if (SaveFile(file, 2))
            {
                // TODO read excel file here
            }

            return null;
        }

        public static void DeleteFile(HttpPostedFileBase file, int type)
        {
            string path;

            switch (type)
            {
                case 0:
                    path = HttpContext.Current.Server.MapPath($"~/Areas/Staff/Uploads/Word/{file.FileName}");
                    File.Delete(path);
                    return;
                case 1:
                    path = HttpContext.Current.Server.MapPath($"~/Areas/Staff/Uploads/CSV/{file.FileName}");
                    File.Delete(path);
                    return;
                case 2:
                    path = HttpContext.Current.Server.MapPath($"~/Areas/Staff/Uploads/Excel/{file.FileName}");
                    File.Delete(path);
                    break;
            }
        }
    }
}