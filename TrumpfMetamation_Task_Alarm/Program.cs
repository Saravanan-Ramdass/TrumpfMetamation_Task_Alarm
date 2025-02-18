using AlarmAutomation.Tests;
    
class Program{
    static void Main()
    {
        //For Alarm application
        TestAlarm testAlarm = new TestAlarm();
        testAlarm.RunTest();

        //For finding even or odd number
        var inputs = new int[] { 10, 20, 50, -1, 5, 7 };
        IsEvenArray(inputs);
    }
    public static bool IsEvenNumber(int number)
    {
        if (number % 2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void IsEvenArray(int[] numberArray)
    {
        foreach (int number in numberArray)
        {
            if (IsEvenNumber(number))
            {
                Console.WriteLine(number.ToString() + " is Even");
            }
            else
            {
                Console.WriteLine(number.ToString() + " is Odd");
            }
        }

    }
}
