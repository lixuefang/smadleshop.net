using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace smadleshop.pojo
{
    public class ScoreCommodity
    {
        public int Id { get; set; }
        public String CommodityName { get; set; }
        public int Score { get; set; }
        public DateTime CreateTime { get; set; }
        [Column(TypeName = "bit")]
        public bool IsDel { get; set; }
        [Column(TypeName = "bit")]
        public bool IsDown { get; set; }
        public string Summary { get; set; }
    }
}
