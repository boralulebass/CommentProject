using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommentProject.EntityLayer.Concrete
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        public int? AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public int? CommentID { get; set; }
        public Comment Comment { get; set; }
    }
}
