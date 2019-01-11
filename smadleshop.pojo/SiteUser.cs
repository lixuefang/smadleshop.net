using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace smadleshop.pojo
{
    public class SiteUser
    {
        public int Id { get; set; }
        public String LoginName { get; set; }
        public String Password { get; set; }
        public String RealName { get; set; }
        [Column(TypeName = "bit")]
        public bool IsDel { get; set; }
        [Column(TypeName = "bit")]
        public bool IsEnabled { get; set; }
    }
}
