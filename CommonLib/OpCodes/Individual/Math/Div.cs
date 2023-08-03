namespace CustomAssembly.OpCodes.Individual;

public class Div : Full
{
  public Div() : base("DIV", 0x13)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a / b);
  }
}