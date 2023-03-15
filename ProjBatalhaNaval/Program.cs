using ProjBatalhaNaval;

internal class Program
{
    private static void Main(string[] args)
    {
        Submarine submarine = new();
        Destroyer destroyer = new();
        Carrier carrier = new();
         
        Console.Clear();

        char a;
        Console.WriteLine("Informe o alinhamento do Submarino: ");
        a = char.Parse(Console.ReadLine());
        submarine.ChooseAlignment(a);

        Console.WriteLine("Informe o alinhamento do Destroyer: ");
        a = char.Parse(Console.ReadLine());
        destroyer.ChooseAlignment(a);
        
        Console.WriteLine("Informe o alinhamento do Porta-Aviões: ");
        a = char.Parse(Console.ReadLine());
        carrier.ChooseAlignment(a);

        Console.WriteLine(submarine.ToString());
        Console.WriteLine(destroyer.ToString());
        Console.WriteLine(carrier.ToString());

    }
}