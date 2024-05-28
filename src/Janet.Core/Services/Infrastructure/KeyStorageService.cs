using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Janet.Core.Services.Infrastructure;


public class KeyStorageService : IKeyStorageService
{
    private const int CRED_TYPE_GENERIC = 1;

    public bool SaveKey(string name, string key)
    {
        var credential = new CREDENTIAL
        {
            TargetName = name,
            UserName = Environment.UserName,
            CredentialBlob = Marshal.StringToCoTaskMemUni(key),
            CredentialBlobSize = (uint)Encoding.Unicode.GetByteCount(key),
            Persist = (uint)CRED_PERSIST.LOCAL_MACHINE,
            Type = CRED_TYPE_GENERIC
        };

        bool result = CredWrite(ref credential, 0);
        Marshal.FreeCoTaskMem(credential.CredentialBlob);
        return result;
    }

    public string RetrieveKey(string name)
    {
        if (CredRead(name, CRED_TYPE_GENERIC, 0, out IntPtr credentialPtr))
        {
            var credential = (CREDENTIAL)Marshal.PtrToStructure(credentialPtr, typeof(CREDENTIAL));
            string key = Marshal.PtrToStringUni(credential.CredentialBlob, (int)credential.CredentialBlobSize / 2);
            CredFree(credentialPtr);
            return key;
        }
        return null;
    }

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredWrite([In] ref CREDENTIAL userCredential, [In] uint flags);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool CredRead(string target, int type, int reservedFlag, out IntPtr credentialPtr);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern void CredFree([In] IntPtr cred);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct CREDENTIAL
{
    public uint Flags;
    public uint Type;
    public string TargetName;
    public string Comment;
    public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
    public uint CredentialBlobSize;
    public IntPtr CredentialBlob;
    public uint Persist;
    public uint AttributeCount;
    public IntPtr Attributes;
    public string TargetAlias;
    public string UserName;
}

public enum CRED_PERSIST : uint
{
    SESSION = 1,
    LOCAL_MACHINE = 2,
    ENTERPRISE = 3
}

// Usage example in comments:
// IKeyStorageService keyStorageService = new KeyStorageService();
// keyStorageService.SaveKey("APIKey", "your-api-key");
// string apiKey = keyStorageService.RetrieveKey("APIKey");