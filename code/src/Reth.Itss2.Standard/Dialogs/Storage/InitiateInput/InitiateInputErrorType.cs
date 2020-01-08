namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public enum InitiateInputErrorType
    {
        Rejected,
        RejectedNoExpiryDate,
        RejectedInvalidExpiryDate,
        RejectedNoPickingIndicateor,
        RejectedNoBatchNumber,
        RejectedNoSerialNumber,
        RejectedNoStockLocation,
        RejectedInvalidStockLocation,
        QueueFull,
        FridgeMissing,
        UnknownPackDimensions,
        MeasurementEror,
        PackAcknowledged,
        InputBroken,
        NoSpaceInMachine,
        NoPackDetected
    }
}