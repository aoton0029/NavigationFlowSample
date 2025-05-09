using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NavigationFlowSample.Models
{
    public class SerialNo : IComparable<SerialNo>
    {
        public string Prefix { get; set; }
        public int Number { get; set; }
        public string Suffix { get; set; }

        public SerialNo(string prefix, int number, string suffix)
        {
            Prefix = prefix;
            Number = number;
            Suffix = suffix;
        }

        private string buildSerialNo()
        {
            return $"{Prefix}{Number.ToString("D6")}{Suffix}";
        }

        public override string ToString()
        {
            return buildSerialNo();
        }

        public int CompareTo(SerialNo other)
        {
            if (other == null)
                return 1;

            // まずプレフィックスで比較
            int prefixComparison = string.Compare(Prefix, other.Prefix, StringComparison.Ordinal);
            if (prefixComparison != 0)
                return prefixComparison;

            // 次に番号で比較
            int numberComparison = Number.CompareTo(other.Number);
            if (numberComparison != 0)
                return numberComparison;

            // 最後にサフィックスで比較
            return string.Compare(Suffix, other.Suffix, StringComparison.Ordinal);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            SerialNo other = (SerialNo)obj;
            return string.Equals(Prefix, other.Prefix, StringComparison.Ordinal) &&
                   Number == other.Number &&
                   string.Equals(Suffix, other.Suffix, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Prefix?.GetHashCode() ?? 0,
                Number,
                Suffix?.GetHashCode() ?? 0);
        }
    }
}
