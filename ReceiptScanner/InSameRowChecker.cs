using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptScanner
{
    public class InSameRowChecker : IEqualityComparer<ReceiptItem>  // Decides the whether two receiptItems are in the same row or not
    {
        /* 
         * Below function decides whether two receiptItems are on the same row or not.
         * First, It calculates the midpoint of the height of one receiptItem and check if it is within the height of the other receiptItem
         * If this condition holds true for the second receiptItem as well, Both of them are on the same row
         * The Y values were not compared directly because the Y values of the receipt items on the same line are not the same.
         */
        public bool Equals(ReceiptItem item1, ReceiptItem item2)    
        {
            var item1MaxY = item1.BoundingPoly.Vertices.Max(x => x.Y);
            var item1MinY = item1.BoundingPoly.Vertices.Min(x => x.Y);
            var item1CenterPointY = (item1MaxY + item1MinY) / 2;


            var item2MaxY = item2.BoundingPoly.Vertices.Max(x => x.Y);
            var item2MinY = item2.BoundingPoly.Vertices.Min(x => x.Y);
            var item2CenterPointY = (item2MaxY + item2MinY) / 2;

            if (item1CenterPointY > item2MinY && item1CenterPointY < item2MaxY &&
                item2CenterPointY > item1MinY && item2CenterPointY < item1MaxY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(ReceiptItem obj)
        {
            return 0;
        }
    }
}
