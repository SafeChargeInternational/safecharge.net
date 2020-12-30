namespace Safecharge.Utils.Enum
{
    /// <summary>
    /// All server error types.
    /// </summary>
    public enum ErrorType
    {
        NoError, 
        GenericError, 
        InvalidChecksum, 
        GeneralValidation, 
        CommunicationError, 
        ErrCodeInvalidMerchantSiteId, 
        ErrCodeInvalidOrderId, 
        ErrCodeInvalidOrderState, 
        ErrCodeInvalidAmount, 
        ErrCodeInvalidCurrency,
        ErrCodeInvalidTrxType,
        ErrCodeUsedSessionToken, 
        SessionExpired, 
        InvalidToken,
        InvalidRequest,
        ErrCodeInvalidUserToken,
        ErrCodeInvalidCardData,
        ErrCodeMissingPaymentData,
        ErrCodeAmbiguousPaymentData,
        ErrCodeInvalidUpoData,
        ErrCodeUserManagementOff,
        ErrCodeInvalidCardIssuer,
        ErrCodeInvalidCardToken
    }
}
