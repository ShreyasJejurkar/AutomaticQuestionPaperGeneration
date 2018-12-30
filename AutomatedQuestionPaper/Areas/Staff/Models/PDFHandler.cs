using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;
using Spire.Doc;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public static class PdfHandler
    {
        public static void ConvertToPdf(string questionPaperPath)
        {
            var d = new Document();
            d.LoadFromFile(questionPaperPath);

            var pathToSavePdf = HttpContext.Current.Server.MapPath("~/App_Data/PdfQuestionPapers/sample.pdf");

            d.SaveToFile(pathToSavePdf, FileFormat.PDF);

            SaveQuestionPaper(pathToSavePdf, questionPaperPath);
        }

        private static void SaveQuestionPaper(string pathPdf, string pathWord)
        {
            var context = new DatabaseContext();

            using (var con = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                con.Open();
                var fileStream = File.OpenRead(pathPdf);
                var contentPdf = new byte[fileStream.Length];
                fileStream.Read(contentPdf, 0, (int) fileStream.Length);
                fileStream.Close();

                var fileStream1 = File.OpenRead(pathWord);
                var contentWord = new byte[fileStream1.Length];
                fileStream1.Read(contentWord, 0, (int) fileStream1.Length);
                fileStream1.Close();

                context.ExamPapers.Add(new ExamPaper
                {
                    // TODO Create the name of paper as per semester department and subject
                    PaperName = "test",

                    // TODO Get the logged in staff id and assign it here
                    StaffId = 10,

                    PaperValue = contentPdf,

                    PaperValueWord = contentWord
                });

                context.SaveChanges();
            }
        }

        public static FileStreamResult DisplayPdf(int id)
        {
            var context = new DatabaseContext();
            var fileByteData = context.ExamPapers.FirstOrDefault(i => i.Id == id);

            var pdfStream = new MemoryStream();
            pdfStream.Write(fileByteData.PaperValue, 0, fileByteData.PaperValue.Length);
            pdfStream.Position = 0;
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=form.pdf");

            return new FileStreamResult(pdfStream, "application/pdf");
        }

        // TODO make a method for displaying word file same as DisplayPDf
    }
}