namespace CustomAssembly.OpCodes.Individual;

public class Xor : Full
{
  public Xor() : base("XOR", 0x22)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a ^ b);
  }
}