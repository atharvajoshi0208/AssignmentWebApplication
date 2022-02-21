using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebApplication
{
    public class Messages
    {
        public int ID { get; set; }
       
        public string Payload { get; set; } = string.Empty;

        public DateTime ReceivedAt { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get; set; }

    }
}
