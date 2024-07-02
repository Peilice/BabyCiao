using System;
using System.Collections.Generic;

namespace BabyCiao.Models
{
    public partial class ExchangeOrder
    {
        public int Id { get; set; }

        public string AccountA_UserAccount { get; set; } = null!;

        public string AccountB_UserAccount { get; set; } = null!;

        public DateTime ModifiedTime { get; set; }

        public string Statement { get; set; } = null!;

        // 導覽屬性
        public virtual UserAccount AccountA { get; set; } = null!;

        public virtual UserAccount AccountB { get; set; } = null!;

        public virtual ICollection<ExchangeOrderDetail> ExchangeOrderDetails { get; set; } = new List<ExchangeOrderDetail>();
    }
}
