using System.Data;
using System.Security.AccessControl;

namespace ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello, World!");

        var process = new OrderProcess();
        process.OrderEvent += (message) => Console.WriteLine($"Notifivation: {message}");
        process.OrderEvent += AddEvent;
        
        process.ProcessOrder("test");

        List<int> list = new List<int> { 1, 2, 3, 4, 5 };
        List<string> strList = new List<string> { "apple", "banana", "orange", "grape" };
        IEnumerable<int> enumerable = from num in list where num % 2 == 0 select num;
        Console.WriteLine(enumerable.Count());

        IOrderedEnumerable<int> orderByDescending = list.Where(x=> x%2 != 0).OrderByDescending(x => x);
        foreach (int i in orderByDescending)
        {
            Console.WriteLine(i);
        }
        
        var fruitQuery = strList.Where(f => f.Length > 5)
            .Select(f => new { Name = f, Length = f.Length })
            .OrderBy(f => f.Name);

        foreach (var fruit in fruitQuery)
        {
            Console.WriteLine(fruit.Name);
        }
    }
    
    

    static void AddEvent(string message)
    {
        Console.WriteLine($"Sending event: {message}");
    }
}

public class OrderProcess
{
    public delegate void OrderEventHandler(string messae);

    public event OrderEventHandler OrderEvent;

    public void ProcessOrder(string orderNumber)
    {
        Console.WriteLine($"Process order: {orderNumber}");

        OnOrderProcess($"Order {orderNumber} has been processed");
    }

    private void OnOrderProcess(string message)
    {
        OrderEvent?.Invoke(message);
    }
    
}