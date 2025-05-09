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
