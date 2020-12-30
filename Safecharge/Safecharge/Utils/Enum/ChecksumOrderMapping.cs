namespace Safecharge.Utils.Enum
{
    /// <summary>
    /// Order mappings names used in the checksum calculations.
    /// </summary>
    public enum ChecksumOrderMapping
    {
        ApiGenericChecksumMapping,
        ApiBasicChecksumMapping,
        SettleGwTransactionChecksumMapping,
        VoidGwTransactionChecksumMapping,
        RefundGwTransactionChecksumMapping,
        NoChecksumMapping
    }
}
