using System;
using System.Text.RegularExpressions;
namespace Ahmetet
{
    public static class Araçlar
    {

        public static bool VarMı(string sorgulanan_sey, string sorgulanan_yer)
        {

            bool iceriyormu = Regex.IsMatch(sorgulanan_yer.ToLower(), string.Format(@"\b{0}\b", Regex.Escape(sorgulanan_sey.ToLower())), RegexOptions.IgnoreCase);
            return iceriyormu;
        }

        public static string ParametreBul(string girdi)
        {
            Regex ParametreBul = new Regex("\\((.*?)\\)");
            return ParametreBul.Match(girdi).ToString().Replace('(', ' ').Replace(')', ' ').ToString();
        }
        public static string StringBul(string girdi)
        {
            Regex StringBul = new Regex("\"(.*?)\"");
            return StringBul.Match(girdi).ToString().Replace('"', ' ').ToString();
        }
        public static int SayıBul(string girdi)
        {
            Regex SayıBul = new Regex("[0-9]+");
            return Convert.ToInt32(SayıBul.Match(girdi).ToString());
        }
        public static string PragmaBul(string girdi)
        {
            Regex PragmaBul = new Regex("^#program");
            return PragmaBul.Match(girdi).ToString();
        }
        




    }
}
