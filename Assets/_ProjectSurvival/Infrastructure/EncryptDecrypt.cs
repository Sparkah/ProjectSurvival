using System.Text;

namespace _ProjectSurvival.Infrastructure
{
    public static class EncryptDecrypt
    {
        private const int Key = 129;

        public static string EncryptDecryptData(string textToEncrypt)
        {            
            var inSb = new StringBuilder(textToEncrypt);
            var outSb = new StringBuilder(textToEncrypt.Length);
            char c;
            for (int i = 0; i < textToEncrypt.Length; i++)
            {
                c = inSb[i];
                c = (char)(c ^ Key);
                outSb.Append(c);
            }
            return outSb.ToString();
        }   
    }
}