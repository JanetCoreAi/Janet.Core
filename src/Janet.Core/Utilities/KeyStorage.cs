using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Janet.Core.Services.Security
{
    public static class KeyStorage
    {
        private const string CredentialTarget = "Janet.Core";

        [DllImport("advapi32.dll", SetLastError = true, EntryPoint = "CredWriteW", CharSet = CharSet.Unicode)]
        private static extern bool CredWrite(IntPtr credential, uint flags);

        [DllImport("advapi32.dll", SetLastError = true, EntryPoint = "CredReadW", CharSet = CharSet.Unicode)]
        private static extern bool CredRead(string target, CRED_TYPE type, int reservedValue, out IntPtr credentialPtr);

        [DllImport("advapi32.dll", SetLastError = true, EntryPoint = "CredFree")]
        private static extern bool CredFree(IntPtr buffer);

        private static bool SaveCredential(string name, string secret)
        {
            CREDENTIAL credential = new CREDENTIAL
            {
                TargetName = CredentialTarget,
                Comment = name,
                CredentialBlobSize = (uint)Encoding.Unicode.GetByteCount(secret),
                Type = CRED_TYPE.GENERIC,
                Persist = CRED_PERSIST.ENTERPRISE
            };

            IntPtr credentialPtr = IntPtr.Zero;
            try
            {
                byte[] credentialBlob = Encoding.Unicode.GetBytes(secret);
                credential.CredentialBlob = Marshal.AllocHGlobal(credentialBlob.Length);
                Marshal.Copy(credentialBlob, 0, credential.CredentialBlob, credentialBlob.Length);

                credentialPtr = Marshal.AllocHGlobal(Marshal.SizeOf(credential));
                Marshal.StructureToPtr(credential, credentialPtr, true);

                bool result = CredWrite(credentialPtr, 0);
                return result;
            }
            finally
            {
                if (credential.CredentialBlob != IntPtr.Zero)
                    Marshal.FreeHGlobal(credential.CredentialBlob);

                if (credentialPtr != IntPtr.Zero)
                    Marshal.FreeHGlobal(credentialPtr);
            }
        }

        private static string RetrieveCredential(string name)
        {
            IntPtr credentialPtr;
            if (CredRead($"{CredentialTarget}\\{name}", CRED_TYPE.GENERIC, 0, out credentialPtr))
            {
                try
                {
                    CREDENTIAL credential = (CREDENTIAL)Marshal.PtrToStructure(credentialPtr, typeof(CREDENTIAL));

                    byte[] credentialBlob = new byte[credential.CredentialBlobSize];
                    Marshal.Copy(credential.CredentialBlob, credentialBlob, 0, (int)credential.CredentialBlobSize);

                    return Encoding.Unicode.GetString(credentialBlob);
                }
                finally
                {
                    CredFree(credentialPtr);
                }
            }

            return null;
        }

        public static bool SaveKey(string name, string key)
        {
            return SaveCredential(name, key);
        }

        public static string RetrieveKey(string name)
        {
            return RetrieveCredential(name);
        }

        public static bool KeyExists(string name)
        {
            IntPtr credentialPtr;
            bool credExists = CredRead($"{CredentialTarget}\\{name}", CRED_TYPE.GENERIC, 0, out credentialPtr);

            if (credExists)
                CredFree(credentialPtr);

            return credExists;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CREDENTIAL
        {
            public CRED_PERSIST Persist;
            public CRED_TYPE Type;
            public string TargetName;
            public string Comment;
            public long LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public uint Flags;
        }

        private enum CRED_PERSIST : uint
        {
            SESSION = 1,
            LOCAL_MACHINE = 2,
            ENTERPRISE = 3
        }

        private enum CRED_TYPE : uint
        {
            GENERIC = 1,
            DOMAIN_PASSWORD = 2,
            DOMAIN_CERTIFICATE = 3,
            DOMAIN_VISIBLE_PASSWORD = 4
        }
    }
}