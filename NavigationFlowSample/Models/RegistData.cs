using NavigationFlowSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NavigationFlowSample.Models
{
    public class RegistData : ISnapshotable<RegistData>
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public int NumOfAllBoxes { get; set; }

        public List<Package> Packages { get; set; }

        public Package? Editing => Packages.LastOrDefault(p => p.IsIncluded);

        public Package? MainPackage => Packages.FirstOrDefault(p => !p.IsIncluded);


        public RegistData()
        {
            Packages = new List<Package>()
            {
                new Package(false)
            };
        }

        public void AddPackage()
        {
            Packages.Add(new Package(true));
        }

        // 次の受注情報入力に移行するメソッド
        public void MoveToNextOrder()
        {
            AddPackage();
        }

        // 受注情報を設定するメソッド
        public void SetOrderInfo(string orderNo, int groupNo, string productName, int numOfProducts, List<SerialNo> serialNos)
        {
            if (Editing == null) AddPackage();

            var package = Editing;
            package.OrderNo = orderNo;
            package.GroupNo = groupNo;
            package.ProductName = productName;
            package.NumOfProducts = numOfProducts;
            package.AllSerialNos = serialNos;
        }

        public RegistData CreateSnapshot()
        {
            return new RegistData()
            {
                UserId = UserId,
                UserName = UserName,
                NumOfAllBoxes = NumOfAllBoxes,
                Packages = new List<Package>(Packages.Select(p => p.CreateSnapshot()))
            };
        }

        public void RestoreSnapshot(RegistData snapshot)
        {
            if (snapshot == null) throw new ArgumentNullException(nameof(snapshot));
            UserId = snapshot.UserId;
            UserName = snapshot.UserName;
            NumOfAllBoxes = snapshot.NumOfAllBoxes;
            Packages.Clear();
            foreach (var package in snapshot.Packages)
            {
                Packages.Add(package);
            }
        }
    }
}
