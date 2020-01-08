namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public enum InputResponsePackHandlingInput
    {
        Allowed,
        AllowedForFridge,
        Rejected,
        RejectedNoExpiryDate,
        RejectedNoPickingIndicator,
        RejectedNoBatchNumber,
        RejectedNoSerialNumber,
        RejectedNoStockLocation,
        RejectedInvalidStockLocation
    }
}