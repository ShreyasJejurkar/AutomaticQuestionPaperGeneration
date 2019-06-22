using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedQuestionPaper.ApplicationLogic
{
    public class AcademicData
    {
        public static List<string> GetListOfYears()
        {
            return new List<string>
            {
                "Second year",
                "Third year",
                "Fourth year"
            };
        }
    }
}