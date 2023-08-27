namespace CustomAssembly.OpCodes.Individual;

public class Mul : Full
{
  public Mul() : base("MUL", 0x12)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a * b);
  }
}