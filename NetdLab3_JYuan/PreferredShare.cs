using System;
using System.Collections.Generic;
using System.Text;

namespace NetdLab3_JYuan
{
    class PreferredShare: Shares
    {
        //constants for preferred shares
        protected const int sharePrice = 100;
        protected const int votePower = 10;
        protected const string shareType = "preferred";

        //constructor for the preferred share
        public PreferredShare(string buyerName, string purchasedDate, int shareNumber) : base(buyerName, purchasedDate, shareNumber)
        {
            this.buyerName = buyerName;
            this.buyDate = purchasedDate;
            this.shareNumber = shareNumber;
        }

        //getters for vote power
        public int VotePower
        {
            get { return votePower; }
        }

        //getters for share price
        public int SharePrice
        {
            get { return sharePrice; }
        }

        //getters for share type
        public string ShareType
        {
            get { return shareType; }
        }
    }
}
