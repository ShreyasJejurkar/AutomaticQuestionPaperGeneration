using System.Web;
using Spire.Doc;

// ReSharper disable InconsistentNaming

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class InSemQuestionPaperGenerator
    {
        public string Department_Name;
        public string Subject_Name;
        
        public string Question1_A;
        public string Question1_B;

        public string Question2_A;
        public string Question2_B;
        
        public string Question3_A;
        public string Question3_B;
        
        public string Question4_A;
        public string Question4_B;
        
        public string Question5_A;
        public string Question5_B;
        
        public string Question6_A;
        public string Question6_B;


        public void GenerateQuestionPaper()
        {
            var questionPaperFormatFilePath = HttpContext.Current.Server.MapPath("~/App_Data/QuestionPapersFormat/insem.doc");
            var questionPaperPath = HttpContext.Current.Server.MapPath("~/App_Data/GeneratedQuestionPaper/sample.doc");
            
            var doc = new Document();
            doc.LoadFromFile(questionPaperFormatFilePath);

            doc.Replace("QUESTION1_A", Question1_A, true, true);
            doc.Replace("QUESTION1_B", Question1_B, true, true);
            doc.Replace("QUESTION2_A", Question2_A, true, true);
            doc.Replace("QUESTION2_B", Question2_B, true, true);

            doc.Replace("QUESTION3_A", Question3_A, true, true);
            doc.Replace("QUESTION3_B", Question3_B, true, true);
            doc.Replace("QUESTION4_A", Question4_A, true, true);
            doc.Replace("QUESTION4_B", Question4_B, true, true);
            doc.Replace("QUESTION5_A", Question5_A, true, true);
            doc.Replace("QUESTION5_B", Question5_B, true, true);
            doc.Replace("QUESTION6_A", Question6_A, true, true);
            doc.Replace("QUESTION6_B", Question6_B, true, true);

            doc.SaveToFile(questionPaperPath);
            doc.Dispose();

            PdfHandler.ConvertToPdf(questionPaperPath);
        }

        
    }
}