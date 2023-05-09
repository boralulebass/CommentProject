using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.EntityLayer.Concrete
{
    public class Contact
    {
        [Key]
        public int MessageId { get; set; }

        public int? AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public string? Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }
}
