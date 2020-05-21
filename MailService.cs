using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class MailService
{
    private readonly string _host;
    private readonly int _port;
    private readonly string _userName;
    private readonly string _password;

    public MailService(string host, int port, string userName, string password)
    {
        _host = host;
        _port = port;
        _userName = userName;
        _password = password;
    }

    public async Task SendEmail()
    {
        var message = CreateEmailMessage();
        using var client = await CreateSmtpClient();
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

    private MimeMessage CreateEmailMessage()
    {
        var message = new MimeMessage();

        var from = new MailboxAddress("Tom", "tom@gmail.com");
        var to = new MailboxAddress("Alex", "alex@gmail.com");
        var cc = new MailboxAddress("Amy","amy@gmail.com");

        message.From.Add(from);
        message.To.Add(to);
        message.Cc.Add(cc);
        message.Subject = "Test123";
        message.Body = GetEmailBody();

        return message;
    }

    private MimeEntity GetEmailBody()
    {
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
        bodyBuilder.TextBody = "Hello World!";
        AttachFile(bodyBuilder);

        return bodyBuilder.ToMessageBody();
    }

    private void AttachFile(BodyBuilder builder)
    {
        builder.Attachments.Add(@"images\dahyun.jpg");
    }

    private async Task<SmtpClient> CreateSmtpClient()
    {
        var client = new SmtpClient();
        await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_userName, _password);
        return client;
    }
}