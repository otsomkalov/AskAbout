namespace AskAbout.Models
{
    public class Like : BaseEntity
    {
        public bool? IsLiked { get; set; }

        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public int ReplyId { get; set; }
        public int CommentId { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
        public virtual Reply Reply { get; set; }
        public virtual Comment Comment { get; set; }
    }
}