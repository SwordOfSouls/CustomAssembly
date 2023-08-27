namespace CustomAssembly.OpCodes.Individual;

public class Stl : Full
{
  public Stl() : base("STL", 0x29)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a << b);
  }
}