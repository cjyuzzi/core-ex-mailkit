using System;
using System.Threading.Tasks;

namespace core_email_service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mailService = new MailService("smtp.mailtrap.io", 2525, "4cce88d61bd9e0", "5c8f26387d084e");

            await mailService.SendEmail();

            Console.WriteLine("Sent");
            Console.ReadLine();
        }
    }
}
