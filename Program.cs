using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine(GerarCEPVal());
        }
    }
    public static string GerarCEPVal()
    {
        System.Threading.Thread.Sleep(300);
        Random randNum = new();
        string firstNumber;
        string secondNumber;
        string validationRequest;
        for (; ; )
        {
            firstNumber = string.Concat(randNum.Next(1, 9),
                                        randNum.Next(1, 6),
                                        randNum.Next(1, 6),
                                        randNum.Next(1, 5),
                                        randNum.Next(0, 0));

            secondNumber = "0" + randNum.Next(1,9) + "0";
            validationRequest = string.Concat("https://viacep.com.br/ws/", firstNumber, secondNumber, "/json/");
            try
            {
                using (Task<string> sendRequest = new HttpClient().GetStringAsync(validationRequest))
                {
                    sendRequest.Wait();
                    if (!sendRequest.Result.Contains("\"erro\""))
                    {
                        return firstNumber + "-" + secondNumber;
                    }
                }
            }
            catch
            {
                continue;
            }
            System.Threading.Thread.Sleep(300);
        }
    }
}
