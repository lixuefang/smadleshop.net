using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace smadleshop.pojo
{
    public class UserAddress
    {
        public int Id { get; set; }
        public String Address { get; set; }
        [Column(TypeName = "bit")]
        public bool IsDefault { get; set; }
        public int SiteUserId { get; set; }
        public int AddressOrder { get; set; }
        public String MobilePhone { get; set; }
        public String ContactPerson { get; set; }
    }
}
