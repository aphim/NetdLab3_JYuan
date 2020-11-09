//Project Name: Net3202 Communication Activity 3
//Author: Jacky Yuan
//Date: Nov 09, 2020
//Description: Makes a database software program for purchasing shares using classes.
//Change log: N/A

using System;
using System.Collections.Generic;
using System.Text;

namespace NetdLab3_JYuan
{
    class Shares
    {
        //private variables
        protected string buyerName;
        protected string buyDate;
        protected int shareNumber;

        //public constructor for creating a project class object
        public Shares(string buyerName, string purchasedDate, int numShares)
        {
            this.buyerName = buyerName;
            this.buyDate = purchasedDate;
            this.shareNumber = numShares;
        }
        
        // Getters and Setters for buyerName
        public string BuyerName
        {
            get { return this.buyerName; }
            set
            {
                this.buyerName = value;
            }
        }

        //Getters and Setters for purchased date
        public string PurchasedDate
        {
            get { return this.buyDate; }
            set
            {
                this.buyDate = value;
            }
        }

        // Getters and Setters for shareNumber
        public int ShareNumber
        {
            get { return this.shareNumber; }
            set
            {
                this.shareNumber = value;
            }
        }
    }
}
