using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Controllers
{
    public class BaseController : Controller
    {
        public void Alert(string title,string message, Enums.NotificationType notificationType)
        {
            var msg = $"<script language='javascript'>swal('{title}','{message}','{notificationType}')" + "</script>";
            TempData["notification"] = msg;
        }

        public void Alert(string title, string message)
        {
            var msg = $"<script language='javascript'>swal('{title}','{message}')" + "</script>";
            TempData["notification"] = msg;
        }


        public void Alert(string message)
        {
            var msg = $"<script language='javascript'>swal('{message}')" + "</script>";
            TempData["notification"] = msg;
        }

        //public void Alert()
        //{
        //    var ww = "swal({title: \"Are you sure?\",text: \"Once deleted, you will not be able to recover this imaginary file!\",icon: \"warning\",buttons: true,dangerMode: true,}).then((willDelete) => {if (willDelete) {swal(\"Poof! Your imaginary file has been deleted!\", {icon: \"success\",});} else {swal(\"Your imaginary file is safe!\");}});";

        //    var msg = $"<script language='javascript'>{ww}" + "</script>";
        //    TempData["notification"] = msg;
        //}




        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Message(string message, Enums.NotificationType notifyType)
        {
            TempData["Notification2"] = message;

            switch (notifyType)
            {
                case Enums.NotificationType.success:
                    TempData["NotificationCSS"] = "alert-box success";
                    break;
                case Enums.NotificationType.error:
                    TempData["NotificationCSS"] = "alert-box errors";
                    break;
                case Enums.NotificationType.warning:
                    TempData["NotificationCSS"] = "alert-box warning";
                    break;

                case Enums.NotificationType.info:
                    TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
    }
}