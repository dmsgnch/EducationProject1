namespace EducationProject1.Components.Helpers;

public static class RandomHelper
{
    private static Random RandomObj { get; set; } = new Random();

    private const int MinMoveValue = -4;
    private const int MaxMoveValue = 4;
    
    private const int MinSizeValue = 20;
    private const int MaxSizeValue = 40;

    public static (int, int) GetMoveVectorRandomNumbers()
    {
        return (GetRandomNumberExceptZero(MinMoveValue, MaxMoveValue),
            GetRandomNumberExceptZero(MinMoveValue, MaxMoveValue));
    }
    
    public static (int, int) GetSizeVectorRandomNumbers()
    {
        return (GetNaturalRandomNumberInDiapason(MinSizeValue, MaxSizeValue),
            GetNaturalRandomNumberInDiapason(MinSizeValue, MaxSizeValue));
    }

    private static int GetRandomNumberExceptZero(int minValue, int maxValue)
    {
        if (minValue > maxValue || (minValue <= 0 && maxValue >= 0 && maxValue - minValue == 0))
            throw new ArgumentException("Invalid range. No non-zero values possible in the given range.");

        int number = RandomObj.Next(minValue, maxValue + 1);
        return number == 0 ? GetRandomNumberExceptZero(minValue, maxValue) : number;
    }

    public static int GetNaturalRandomNumberInDiapason(ushort minValue, uint maxValue)
    {
        return RandomObj.Next(minValue, (int)maxValue + 1);
    }
}