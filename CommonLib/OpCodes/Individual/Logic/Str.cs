namespace CustomAssembly.OpCodes.Individual;

public class Str : Full
{
  public Str() : base("STR", 0x2A)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a >> b);
  }
}