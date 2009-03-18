using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace PlaylistPanda.Properties
{
    internal sealed partial class Settings
    {
        private static readonly byte[] _entropy = new byte[] { 0x01, 0x02, 0x03, 0x05, 0x07, 0x11 };

        private string readPassword(string field)
        {
            try
            {
                byte[] protectedSecret = Convert.FromBase64String((string)this[field]);
                byte[] secret = ProtectedData.Unprotect(protectedSecret, _entropy, DataProtectionScope.CurrentUser);

                return Encoding.Unicode.GetString(secret);
            }
            catch (CryptographicException cryptographicException)
            {
                Console.WriteLine(cryptographicException);

                return null;
            }
        }

        private void writePassword(string field, string password)
        {
            byte[] secret = Encoding.Unicode.GetBytes(password);
            byte[] protectedSecret = ProtectedData.Protect(secret, _entropy, DataProtectionScope.CurrentUser);

            this[field] = Convert.ToBase64String(protectedSecret, Base64FormattingOptions.None);
        }

        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.String)]
        [DefaultSettingValue(@"")]
        public string LastFmPassword
        {
            get
            {
                return readPassword("LastFmPassword");
            }
            set
            {
                writePassword("LastFmPassword", value);
            }
        }

        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.String)]
        [DefaultSettingValue(@"")]
        public string ProxyPassword
        {
            get
            {
                return readPassword("ProxyPassword");
            }
            set
            {
                writePassword("ProxyPassword", value);
            }
        }
    }
}