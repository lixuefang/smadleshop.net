using System;
using System.Collections.Generic;
using System.Text;

namespace smadleshop.pojo
{
    public class SiteOrder
    {
        public int Id { get; set; }
        public String OrderNum { get; set; }
        public int SiteUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public String ExpressNum { get; set; }
        public String ExpressCompany { get; set; }
        public String OrderState { get; set; }
        public String MobilePhone { get; set; }
        public String ContactPerson { get; set; }
        public String OtherInfo { get; set; }
        public String OrderType { get; set; }
    }
}
