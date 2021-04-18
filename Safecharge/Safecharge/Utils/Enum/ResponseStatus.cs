namespace Safecharge.Utils.Enum
{
    /// <summary>
    /// Response statuses Approved, Declined and Error are used by PhoneBet API and should not be changed, because they are in production use.
    /// </summary>
    public enum ResponseStatus
    {
        Success,
        Error,
        Approved,
        Declined,
        Redirect
    }
}
