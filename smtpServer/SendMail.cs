using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SPMS.smtpServer
{
    class SendMail
    {

        public void sentMail(string email, string subject, string content)
        {
            int flag = 0;
           
            if (email.Trim().Length == 0)
            {
                flag = 0;
                email = "Required";
                
            }
            else if (!Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"))
            {
                flag = 0;
                
            }
            else
            {
                flag = 1;

            }

            if (flag == 1)
            {
                var smtpServerName = "smtp.gmail.com";
                var port = 587;
                var senderEmailId = "xawbeenregmi@gmail.com";
                var senderPassword = "get2sabin";

                var smptClient = new SmtpClient(smtpServerName, Convert.ToInt32(port))
                {
                    Credentials = new NetworkCredential(senderEmailId, senderPassword),
                    EnableSsl = true
                };


                try
                {
                    smptClient.Send(senderEmailId, email, subject, content);

                    MessageBox.Show("Email Sent Successfully");

                }
                catch (Exception ex) {
                    MessageBox.Show("No internet connection to send mail. ");
                }




            }
        }
    }
}
