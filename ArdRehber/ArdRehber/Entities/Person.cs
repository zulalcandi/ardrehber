using System.ComponentModel.DataAnnotations;

namespace ArdRehber.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string SurName { get; set; }

        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [StringLength(4)]
        public string InternalNumber { get; set; }

       
       public virtual Department Department { get; set; }
        
        
        
    }
}
