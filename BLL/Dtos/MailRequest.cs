namespace BLL.Dtos
{
    public class MailRequest
    {
            public string ToAddress { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
    }
}