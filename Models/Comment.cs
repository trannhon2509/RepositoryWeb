namespace RevibeCO.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
