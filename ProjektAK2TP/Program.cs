using ProjektAK2TP;

GetControlNumberForCode.DecideOperation();

switch (GetControlNumberForCode._operationType)
{
    case OperationType.Barcode:
        Console.WriteLine($"Control number for barcode is: {GetControlNumberForCode.GetControlNumberForBarcode()}");
        break;
    case OperationType.Ssn:
        Console.WriteLine($"Control number for ssn is: {GetControlNumberForCode.GetControlNumberForSsn()}");
        break;
    case OperationType.Isbn:
        Console.WriteLine($"Control number for ISBN is: {GetControlNumberForCode.GetControlNumberForISBN()}");
        break;
    case OperationType.Traincode:
        Console.WriteLine($"Control number for TrainCode is: {GetControlNumberForCode.GetControlNumberForTrainCode()}");
        break;
}