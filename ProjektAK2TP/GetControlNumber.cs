namespace ProjektAK2TP;

public static class GetControlNumberForCode
{
    private static List<int> Code = new();
    private static List<string> Barcode = new();
    private static List<int> ListOddNumbers = new();
    private static List<int> ListEvenNumbers = new();
    public static OperationType _operationType;
    public static string ResultBarcodeWithControlNumber { get; set; }

    //Read user's input and store it into _operationType 
    public static void DecideOperation()
    {
        Console.WriteLine(
            $"Zvolte operaci. Zvolete {OperationType.Barcode} nebo {OperationType.Ssn} nebo {OperationType.Isbn} nebo {OperationType.Traincode}");

        var operationAsString = Console.ReadLine();

        var isDefined = Enum.IsDefined(typeof(OperationType), operationAsString!);
        if (!isDefined)
        {
            throw new Exception("Volba uzivatele je nevalidni");
        }

        Enum.TryParse(operationAsString, out _operationType);
    }

//Set CodeLenght for code  
    public static int? CodeLenght()
    {
        return _operationType switch
        {
            OperationType.Barcode => 12,
            OperationType.Ssn => 9,
            OperationType.Isbn => 12,
            OperationType.Traincode => 11,
            _ => null
        };
    }

//Put user input to list
    private static void GetNumberInputToList(List<int> codeList)
    {
        for (int i = 0; i < CodeLenght(); i++)
        {
            switch (_operationType)
            {
                case OperationType.Barcode:
                    Console.WriteLine($"Zadejte {i + 1}. cislo caroveho kodu");
                    codeList.Add(Convert.ToInt32(Console.ReadLine()));
                    break;
                case OperationType.Ssn:
                    Console.WriteLine($"Zadejte {i + 1}. cislo rodneho cisla");
                    codeList.Add(Convert.ToInt32(Console.ReadLine()));
                    break;
                case OperationType.Isbn:
                    Console.WriteLine($"Zadejte {i + 1}. cislo ISBN kodu");
                    codeList.Add(Convert.ToInt32(Console.ReadLine()));
                    break;
                case OperationType.Traincode:
                    Console.WriteLine($"Zadejte {i + 1}. cislo kodu pro zeleznicni vozidla");
                    codeList.Add(Convert.ToInt32(Console.ReadLine()));
                    break;
            }
        }
    }

    //Read user input and add to listOfOddNUmbers and listOfEvenNumbers
    private static void GetOddAndEvenNumbersFromList(List<int> list)
    {
        switch (_operationType)
        {
            case OperationType.Barcode:
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        ListOddNumbers.Add(list[i]);
                        continue;
                    }

                    ListEvenNumbers.Add(list[i]);
                }

                break;

            case OperationType.Ssn:
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        ListOddNumbers.Add(list[i]);
                        continue;
                    }

                    ListEvenNumbers.Add(list[i]);
                }

                break;

            case OperationType.Isbn:
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        ListOddNumbers.Add(list[i]);
                        continue;
                    }

                    var res = list[i] * 3;
                    ListEvenNumbers.Add(res);
                }

                break;

            case OperationType.Traincode:
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        var res = list[i] * 2;
                        if (res >= 10)
                        {
                            var resString = res.ToString();
                            var intList = resString.Select(x => Convert.ToInt32(x.ToString())).ToList();
                            var result = intList[0] + intList[1];
                            ListOddNumbers.Add(result);
                        }
                        else
                        {
                            ListOddNumbers.Add(res);
                            continue;
                        }
                    }

                    ListEvenNumbers.Add(list[i]);
                }

                break;
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
            case OperationType.Isbn:
                return listOdd.Sum() + listEven.Sum();
            case OperationType.Traincode:
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

    //return control number for traincode using find closest nurmber divisible by 10
    private static int GetControlNumberForTrainCodeCode()
    {
        var sumRes = SumOddAndEvenList(ListOddNumbers, ListEvenNumbers);
        int closestHigherNumberDivisibleByTen = (sumRes + 10) - (sumRes % 10);
        return closestHigherNumberDivisibleByTen - sumRes;
    }


//returns controlnumber for ISBN code
    private static int GetControlNumberForISBNCode()
    {
        var sumOddAndEvenListModuloTen = SumOddAndEvenList(ListOddNumbers, ListEvenNumbers) % 10;
        return sumOddAndEvenListModuloTen != 0 ? 10 - sumOddAndEvenListModuloTen : 0;
    }

//returns roundUpResult substract by sum of odd and even lists
    private static int? GetControlNumber()
    {
        switch (_operationType)
        {
            case OperationType.Barcode:
                return RoundUpResult() - SumOddAndEvenList(ListOddNumbers, ListEvenNumbers);
            case OperationType.Ssn:
                var result = SumOddAndEvenList(ListOddNumbers, ListEvenNumbers) % 11;
                if (result == 10)
                {
                    return 0;
                }

                return result;
            default:
                return null;
        }
    }

//return ControlNumber
    public static int GetControlNumberForBarcode()
    {
        DecideOperation();
        GetNumberInputToList(Code);
        GetOddAndEvenNumbersFromList(Code);
        GetBarcodeToString();
        return (int) GetControlNumber()!;
    }

    //return ControlNumber for UI
    public static int GetControlNumberForBarcodeUI()
    {
        GetOddAndEvenNumbersFromList(Code);
        return (int) GetControlNumber()!;
    }

//return ControlNumber for Ssn
    public static int GetControlNumberForSsn()
    {
        DecideOperation();
        GetNumberInputToList(Code);
        GetOddAndEvenNumbersFromList(Code);
        return (int) GetControlNumber()!;
    }

//return ControlNumber for ISBN
    public static int GetControlNumberForISBN()
    {
        DecideOperation();
        GetNumberInputToList(Code);
        GetOddAndEvenNumbersFromList(Code);
        return GetControlNumberForISBNCode();
    }

//return ControlNumber for TrainCode
    public static int GetControlNumberForTrainCode()
    {
        DecideOperation();
        GetNumberInputToList(Code);
        GetOddAndEvenNumbersFromList(Code);
        return GetControlNumberForTrainCodeCode();
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

    public static void GetBarcodeToString()
    {
        DecideOperation();
        GetNumberInputToList(Code);
        GetOddAndEvenNumbersFromList(Code);
        Barcode = Code.ConvertAll(x => x.ToString());
        ResultBarcodeWithControlNumber = string.Join(",", Barcode).Replace(",", "");
    }
}