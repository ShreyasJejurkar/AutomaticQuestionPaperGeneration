using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public static class PdfHandler
    {
        public static void SavePdf()
        {
            DatabaseContext context = new DatabaseContext();

            using (var con = new SqlConnection(context.Database.Connection.ConnectionString))
            {
                con.Open();
                var fileStream = File.OpenRead(HttpContext.Current.Server.MapPath($"~/Areas/Staff/Uploads/sample.pdf"));
                byte[] content = new byte[fileStream.Length];
                fileStream.Read(content, 0, (int)fileStream.Length);
                fileStream.Close();

                context.ExamPapers.Add(new ExamPaper
                {
                    PaperName = "sample.pdf",
                    StaffId = 2,
                    PaperValue = content
                });

                context.SaveChanges();

                Debug.Write("Done");

            }
        }

        public static FileStreamResult DisplayPdf(int id)
        {
            DatabaseContext context = new DatabaseContext();
            var fileByteData = context.ExamPapers.FirstOrDefault(i => i.Id == id).PaperValue;

            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(fileByteData, 0, fileByteData.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }

    }
}