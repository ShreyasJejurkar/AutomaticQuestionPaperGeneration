using System.Web;
using Spire.Doc;
// ReSharper disable InconsistentNaming

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class EndSemQuestionPaperGenerator
    {
        public string Department_Name;

        public string Question1_A;
        public string Question1_B;

        public string Question2_A;
        public string Question2_B;

        public string Question3_A;
        public string Question3_B;
        public string Question3_C;

        public string Question4_A;
        public string Question4_B;
        public string Question4_C;

        public string Question5_A;
        public string Question5_B;

        public string Question6_A;
        public string Question6_B;

        public string Question7_A;
        public string Question7_B;

        public string Question8_A;
        public string Question8_B;

        public string Question9_A;
        public string Question9_B;
        public string Question9_C;

        public string Question10_A;
        public string Question10_B;
        public string Question10_C;

        public string Subject_Name;


        public void GenerateQuestionPaper(string name)
        {
            var questionPaperFormatFilePath =
                HttpContext.Current.Server.MapPath("~/App_Data/QuestionPapersFormat/endsem.docx");
            var questionPaperPath = HttpContext.Current.Server.MapPath($"~/App_Data/GeneratedQuestionPaper/{name}.docx");

            var doc = new Document();
            doc.LoadFromFile(questionPaperFormatFilePath);

            doc.Replace("QUESTION1_A", Question1_A, true, true);
            doc.Replace("QUESTION1_B", Question1_B, true, true);

            doc.Replace("QUESTION2_A", Question2_A, true, true);
            doc.Replace("QUESTION2_B", Question2_B, true, true);

            doc.Replace("QUESTION3_A", Question3_A, true, true);
            doc.Replace("QUESTION3_B", Question3_B, true, true);
            doc.Replace("QUESTION3_C", Question3_C, true, true);

            doc.Replace("QUESTION4_A", Question4_A, true, true);
            doc.Replace("QUESTION4_B", Question4_B, true, true);
            doc.Replace("QUESTION4_C", Question4_C, true, true);

            doc.Replace("QUESTION5_A", Question5_A, true, true);
            doc.Replace("QUESTION5_B", Question5_B, true, true);

            doc.Replace("QUESTION6_A", Question6_A, true, true);
            doc.Replace("QUESTION6_B", Question6_B, true, true);

            doc.Replace("QUESTION7_A", Question7_A, true, true);
            doc.Replace("QUESTION7_B", Question7_B, true, true);

            doc.Replace("QUESTION8_A", Question8_A, true, true);
            doc.Replace("QUESTION8_B", Question8_B, true, true);

            doc.Replace("QUESTION9_A", Question9_A, true, true);
            doc.Replace("QUESTION9_B", Question9_B, true, true);
            doc.Replace("QUESTION9_C", Question9_C, true, true);

            doc.Replace("QUESTION10_A", Question10_A, true, true);
            doc.Replace("QUESTION10_B", Question10_B, true, true);
            doc.Replace("QUESTION10_C", Question10_C, true, true);

            doc.Replace("DEPARTMENT_NAME", Department_Name, true, true);
            doc.Replace("SUBJECT_NAME", Subject_Name, true, true);

            doc.SaveToFile(questionPaperPath);
            doc.Dispose();

            PdfHandler.ConvertToPdf(questionPaperPath);
        }

    }
}