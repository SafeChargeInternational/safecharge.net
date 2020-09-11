namespace Safecharge.Utils.Enum
{
    /// <summary>
    /// This is the hash algorithm which is used to calculate the checksum. It's configured in the server per merchant site.
    /// </summary>
    public enum HashAlgorithmType
    {
        MD5,
        SHA256
    }
}
