using System;
using System.Collections.Generic;
using System.Text;

namespace NetdLab3_JYuan
{
    class CommonShare: Shares
    {
        //constants for common shares
        protected const int sharePrice = 42;
        protected const int votePower = 1;
        protected const string shareType = "Common";

        //constructor for the common share
        public CommonShare(string buyerName, string purchasedDate, int shareNumber) : base(buyerName, purchasedDate, shareNumber)
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
