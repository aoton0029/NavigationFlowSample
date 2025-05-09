using NavigationFlowSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Models
{
    public class Box : ISnapshotable<Box>
    {
        public int BoxNumber { get; set; }

        public int? ParentBoxNumber { get; set; }

        public List<SerialNo> SerialNos { get; set; }

        public Box CreateSnapshot()
        {
            return new Box()
            {
                BoxNumber = BoxNumber,
                ParentBoxNumber = ParentBoxNumber,
                SerialNos = new List<SerialNo>(SerialNos)
            };
        }

        public void RestoreSnapshot(Box snapshot)
        {
            if (snapshot == null) throw new ArgumentNullException(nameof(snapshot));
            BoxNumber = snapshot.BoxNumber;
            ParentBoxNumber = snapshot.ParentBoxNumber;
            SerialNos.Clear();
            foreach (var serialNo in snapshot.SerialNos)
            {
                SerialNos.Add(serialNo);
            }
        }
    }
}
