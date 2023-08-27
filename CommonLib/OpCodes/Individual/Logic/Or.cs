namespace CustomAssembly.OpCodes.Individual;

public class Or : Full
{
  public Or() : base("OR", 0x21)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a | b);
  }
}