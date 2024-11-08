namespace Biblioteka.Requests
{
    public class CreateBook
    {
        public string Author { get; set; }
        public int Genre_id { get; set; }
        public int Reader_id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }
    }
}
