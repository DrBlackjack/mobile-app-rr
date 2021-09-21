using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace APIFilR.Helpers
{
    public class EmailHelper
    {
        public void EmailSend(string mailTo, string prenom, string token)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("ressourcesrelationnelles.mailer@gmail.com");
                    mail.To.Add(mailTo);
                    mail.Subject = "Vérification du compte";
                    mail.Body = $"Bonjour {prenom}, veuillez vérifier votre compte \n https://localhost:44333/Utilisateur/VerifyAccount/{token}";
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("ressourcesrelationnelles.mailer@gmail.com", "ressourcesrelationnelles.mailer@gmail.coressourcesrelationnelles.mailer@gmail.comm");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
    }
}
