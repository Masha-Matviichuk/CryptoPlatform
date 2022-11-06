namespace DAL.Entities
{
    public class Picture : BaseEntity
    {
        public string URL { get; set; }
        public byte[]? Data { get; set; }
    }
}