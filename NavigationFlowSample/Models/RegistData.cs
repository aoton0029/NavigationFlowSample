using NavigationFlowSample.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
