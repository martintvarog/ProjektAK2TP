using System.Diagnostics;

namespace ProjektAK2TP;

public static class GetControlNumberForCode
{
    private static List<int> Barcode = new();
    private static List<int> ListOddNumbers = new();
    private static List<int> ListEvenNumbers = new();
    public static OperationType _operationType;

    private static int? BarCodeLenght()
    {
        return _operationType switch
        {
            OperationType.Barcode => 12,
            OperationType.Ssn => 9,
            _ => null
        };
    }

    //Read user's input and store it into _operationType 
    public static void DecideOperation()
    {
        Console.WriteLine($"Zvolte operaci. Zvolete {OperationType.Barcode}, nebo {OperationType.Ssn}");

        var operationAsString = Console.ReadLine();

        var isDefined = Enum.IsDefined(typeof(OperationType), operationAsString!);
        if (!isDefined)
        {
            throw new Exception("Volba uzivatele je nevalidni");
        }

        Enum.TryParse(operationAsString, out _operationType);
    }

    //Read user input and add to listOfOddNUmbers and listOfEvenNumbers
    private static void GetOddAndEvenNumbersFromList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i % 2 == 0)
            {
                ListOddNumbers.Add(list[i]);
                continue;
            }

            ListEvenNumbers.Add(list[i]);
        }
    }

    //Return sum of even and odd list
    private static int SumOddAndEvenList(List<int> listOdd, List<int> listEven)
    {
        switch (_operationType)
        {
            case OperationType.Barcode:
                return listOdd.Sum() + listEven.Sum() * 3;
            case OperationType.Ssn:
                return listOdd.Sum() + listEven.Sum();
            default:
                return 0;
        }
    }

    //returns rounded up result of adding odd and even lists
    private static int RoundUpResult()
    {
        var decimalResult = (float) SumOddAndEvenList(ListOddNumbers, ListEvenNumbers) / 10;
        var result = Math.Ceiling(decimalResult);
        return (int) result * 10;
    }

    //returns roundUpResult substract by sum of odd and even lists
    private static int? GetControlNumber()
    {
        switch (_operationType)
        {
            case OperationType.Barcode:
                return RoundUpResult() - SumOddAndEvenList(ListOddNumbers, ListEvenNumbers);
            case OperationType.Ssn:
                var result = SumOddAndEvenList(ListOddNumbers,ListEvenNumbers) % 11;
                if (result == 10)
                {
                    return 0;
                }

                return result;
        }

        return null;
    }

    //return ControlNumber
    public static int GetControlNumberForBarcode()
    {
        DecideOperation();
        GetBarcodeListFromInput(Barcode);
        GetOddAndEvenNumbersFromList(Barcode);
        return (int) GetControlNumber()!;
    }

    //return ControlNumber for Ssn
    public static int GetControlNumberForSsn()
    {
        DecideOperation();
        GetBarcodeListFromInput(Barcode);
        GetOddAndEvenNumbersFromList(Barcode);
        return (int) GetControlNumber()!;
    }
    //Check validity of ControlNumber
    public static void CheckControlNumberForSsn()
    {
        var res = ListOddNumbers.Sum() - (ListEvenNumbers.Sum() + GetControlNumber());
        if (res == 0 || res % 11 == 0)
        {
            Console.WriteLine("Kontrola uspesna");
        }
        else
        { 
            Console.WriteLine("Kontrola neuspesna");
        }
    }

    //Put user input to list
    private static void GetBarcodeListFromInput(List<int> barcode)
    {
        for (int i = 0; i < BarCodeLenght(); i++)
        {
            switch (_operationType)
            {
                case OperationType.Barcode:
                    Console.WriteLine($"Zadejte {i + 1}. cislo caroveho kodu");
                    barcode.Add(Convert.ToInt32(Console.ReadLine()));
                    break;
                case OperationType.Ssn:
                    Console.WriteLine($"Zadejte {i + 1}. cislo rodneho cisla");
                    barcode.Add(Convert.ToInt32(Console.ReadLine()));
                    break;
            }
        }
    }
}