using NavigationFlowSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Models
{
    public class Package : ISnapshotable<Package>
    {
        public bool IsIncluded { get; set; }

        public string OrderNo { get; set; }

        public int GroupNo { get; set; }

        public string OrderSheetNo => string.IsNullOrEmpty(OrderNo) ? string.Empty : $"{OrderNo}-{GroupNo}";

        public string ProductName { get; set; }

        public int NumOfProducts { get; set; }

        public int NumOfProductsPerBox { get; set; } = 1;

        public List<SerialNo> AllSerialNos { get; set; }

        public List<Box> Boxes { get; set; }

        public Package(bool isIncluded)
        {
            IsIncluded = isIncluded;
        }

        public Package CreateSnapshot()
        {
            return new Package(IsIncluded)
            {
                OrderNo = OrderNo,
                GroupNo = GroupNo,
                ProductName = ProductName,
                NumOfProducts = NumOfProducts,
                AllSerialNos = new List<SerialNo>(AllSerialNos),
                Boxes = new List<Box>(Boxes)
            };
        }

        public void RestoreSnapshot(Package snapshot)
        {
            if (snapshot == null) throw new ArgumentNullException(nameof(snapshot));
            IsIncluded = snapshot.IsIncluded;
            OrderNo = snapshot.OrderNo;
            GroupNo = snapshot.GroupNo;
            ProductName = snapshot.ProductName;
            NumOfProducts = snapshot.NumOfProducts;
            AllSerialNos.Clear();
            foreach (var serialNo in snapshot.AllSerialNos)
            {
                AllSerialNos.Add(serialNo);
            }
            Boxes.Clear();
            foreach (var box in snapshot.Boxes)
            {
                Boxes.Add(box);
            }
        }
    }
}
