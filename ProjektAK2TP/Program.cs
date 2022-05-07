List<int> barCode = new List<int>();
List<int> listOddNumbers = new List<int>();
List<int> listEvenNumbers = new List<int>();

barCode.Add(7);
barCode.Add(8);
barCode.Add(0);
barCode.Add(8);
barCode.Add(6);
barCode.Add(3);
barCode.Add(1);
barCode.Add(8);
barCode.Add(5);
barCode.Add(7);
barCode.Add(7);
barCode.Add(9);

GetOddAndEvenNumbersFromList(barCode);
SumOddAndEvenList(listOddNumbers, listEvenNumbers);
RoundUpResult();

void GetOddAndEvenNumbersFromList(List<int> barCode)
{
    for (int i = 0; i < barCode.Count; i++)
    {
        if (i % 2 == 0)
        {
            listOddNumbers.Add(barCode[i]);
            continue;
        }
        listEvenNumbers.Add(barCode[i]);
    }
}

float SumOddAndEvenList(List<int> listOdd, List<int> listEven)
{
    var sumOdd = listOdd.Sum();
    var sumEven = listEven.Sum() * 3;
    return sumEven + sumOdd;
}

int RoundUpResult()
{
    var decimalResult = SumOddAndEvenList(listOddNumbers, listEvenNumbers) /10;
    var result = Math.Ceiling(decimalResult);
    return (int)result * 10;
}

