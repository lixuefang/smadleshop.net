using System;
using System.Collections.Generic;
using System.Text;

namespace smadleshop.pojo
{
    public class ContactUs
    {
        public int Id { get; set; }
        public int SiteUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public String Title { get; set; }
        public string Summary { get; set; }
    }
}
