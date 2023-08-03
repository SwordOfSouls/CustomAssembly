namespace CustomAssembly.OpCodes.Individual;

public class Lst : Full
{
  public Lst() : base("LST", 0x25)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)Convert.ToInt32(a < b);
  }
}