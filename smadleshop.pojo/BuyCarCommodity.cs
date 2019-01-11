using System;
using System.Collections.Generic;
using System.Text;

namespace smadleshop.pojo
{
    public class BuyCarCommodity
    {
        public int Id { get; set; }
        public int SiteUserId { get; set; }
        public int CommodityId { get; set; }
        public DateTime CreateTime { get; set; }
        public int BuyCount { get; set; }
    }
}
