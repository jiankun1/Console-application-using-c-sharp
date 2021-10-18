using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace SimpleBankSystem
{
    class SendEmail
    {
        //reference to https://www.codeproject.com/tips/301836/simple-smtp-e-mail-sender-in-csharp-console-applic and
        //https://stackoverflow.com/questions/449887/sending-e-mail-using-c-sharp

        //Method to send account statement
        public void SendAccountStatement(string mailBody, string emailAddress, string receiverName)
        {
            try
            {
                //SmtpClient client = new SmtpClient("smtp.gamil.com");
                SmtpClient client = new SmtpClient();

                //set smtp client with basic authentication
                //client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("mystudy1@mail.com", "mystudy123456");

                //mail address add from, to
                MailAddress from = new MailAddress("mystudy1@mail.com", "SimpleBankSystem");
                MailAddress to = new MailAddress(emailAddress, receiverName);
                MailMessage mymail = new MailMessage(from, to);

                //add reply
                MailAddress replyTo = new MailAddress("mystudy1@mail.com");
                mymail.ReplyToList.Add(replyTo);

                //set subject and encoding
                mymail.Subject = "Account Statement";
                //mymail.SubjectEncoding = System.Text.Encoding.UTF8;

                //set body message and encoding
                mymail.Body = "Account Statement \n" + mailBody;
                //mymail.BodyEncoding = Encoding.UTF8;
                //text
                //mymail.IsBodyHtml = false;
                client.Host = "smtp.mail.com";
                client.Port = 587;
                client.EnableSsl = true;

                client.Send(mymail);

            }
            catch(SmtpException e)
            {
                throw new ApplicationException
                ("SmtpException has occured: " + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        //Method to send new created account
        public void SendNewAccount(string[] mailBody, string emailAddress, string receiverName, int accountNo)
        {
            try
            {
                //SmtpClient client = new SmtpClient("smtp.gamil.com");
                SmtpClient client = new SmtpClient();

                //set smtp client with basic authentication
                //client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("mystudy1@mail.com", "mystudy123456");

                //mail address add from, to
                MailAddress from = new MailAddress("mystudy1@mail.com", "SimpleBankSystem");
                MailAddress to = new MailAddress(emailAddress, receiverName);
                MailMessage mymail = new MailMessage(from, to);

                //add reply
                MailAddress replyTo = new MailAddress("mystudy1@mail.com");
                mymail.ReplyToList.Add(replyTo);

                //set subject and encoding
                mymail.Subject = "New Account";
                //mymail.SubjectEncoding = System.Text.Encoding.UTF8;

                //set body message and encoding
                mymail.Body = "New Account Details \n" ;
                mymail.Body += "First Name: " + mailBody[0] + "\n";
                mymail.Body += "Last Name: " + mailBody[1] + "\n";
                mymail.Body += "Address: " + mailBody[2] + "\n";
                mymail.Body += "Phone: " + mailBody[3] + "\n";
                mymail.Body += "Email: " + mailBody[4] + "\n";
                mymail.Body += "AccountNo: " + accountNo.ToString() + "\n";
                mymail.Body += "Balance: $0 \n";
                //mymail.BodyEncoding = Encoding.UTF8;
                //text
                //mymail.IsBodyHtml = false;
                client.Host = "smtp.mail.com";
                client.Port = 587;
                client.EnableSsl = true;

                client.Send(mymail);

            }
            catch (SmtpException e)
            {
                throw new ApplicationException
                ("SmtpException has occured: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
