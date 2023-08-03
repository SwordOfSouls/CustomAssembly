namespace CustomAssembly.OpCodes.Individual;

public class Jmp : HighLow
{
  public Jmp() : base("JMP", 0x30)
  {
  }

  public override void Perform(int a)
  {
    Console.WriteLine("Jumping to " + a);
    getRuntime().Jump((ushort)a);
  }
}