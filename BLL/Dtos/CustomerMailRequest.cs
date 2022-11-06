namespace BLL.Dtos
{
    public class CustomerMailRequest
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
    }
}