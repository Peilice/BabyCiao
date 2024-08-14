namespace BabyCiaoAPI.DTO
{
    public class GetSecondHandExchangeDTO
    {//GET要取得所有ID?????未更改
        public int Id { get; set; }

        public string BuyerId { get; set; } = null!;//申請者

        public string SellerId { get; set; } = null!;//賣家

        public int WantGetId { get; set; }//申請者想要的物品
		public string WantName { get; set; } = null!;

		public int GetQuantity { get; set; }//申請者想要的物品數量

        public int WantGiveId { get; set; }//申請者要給賣家的
		public string GiveName { get; set; } = null!;
		public int GiveQuantity { get; set; }//申請者要給賣家的數量

        public DateTime ModifiedTime { get; set; }//異動時間

        public string View { get; set; } = null!;

		public string Statement { get; set; } = null!;//申請交換單狀態(申請中、確認成交、不成立)


    }
    public class SecondHandExchangeDTO
    {
        public int Id { get; set; }

        public string BuyerId { get; set; } = null!;//申請者

        public string SellerId { get; set; } = null!;//賣家

        public int WantGetId { get; set; }//申請者想要的物品

        public int GetQuantity { get; set; }//申請者想要的物品數量

        public int WantGiveId { get; set; }//申請者要給賣家的物品

        public int GiveQuantity { get; set; }//申請者要給賣家的物品數量

        public DateTime ModifiedTime { get; set; }//異動時間

        public string Statement { get; set; } = null!;//申請交換單狀態(申請中、確認成交、不成立)


    }
    public class SecondHandExchangeOrderDTO
    {
        public int IdExchangeOrder { get; set; }

        public bool BuyerOk { get; set; }

        public bool SellerOk { get; set; }
    }
}
