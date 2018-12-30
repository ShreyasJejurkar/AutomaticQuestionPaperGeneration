using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class WordHandler
    {
        public static FileStreamResult DownloadWordFile(int id)
        {
            var context = new DatabaseContext();
            var fileByteData = context.ExamPapers.FirstOrDefault(i => i.Id == id);

            var wordStream = new MemoryStream();
            wordStream.Write(fileByteData.PaperValueWord, 0, fileByteData.PaperValueWord.Length);
            wordStream.Position = 0;
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment; filename={fileByteData.PaperName}.doc");
            return new FileStreamResult(wordStream, "application/msword");
        }
    }
}