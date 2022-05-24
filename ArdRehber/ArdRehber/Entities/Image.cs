using System.ComponentModel.DataAnnotations.Schema;

namespace ArdRehber.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] ImageBase64 { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Id")]
        public virtual User User { get; set; }


    }
}
