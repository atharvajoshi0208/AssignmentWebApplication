using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebApplication
{
    public class User
    {
        public User()
        {
            Messages = new HashSet<Messages>();
        }
        public int Id { get; set; }
        [NotMapped]
        public int user { get; set; } = 0;

     

        public string UserName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        [NotMapped]
        public string Date { get; set; } = String.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateLastSeen { get; set; } = DateTime.Now;

        [NotMapped]
        public string PayLoad { get; set; } = String.Empty;

        public  ICollection<Messages>? Messages { get; set; }


    }
}
