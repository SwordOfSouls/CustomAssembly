namespace CustomAssembly.OpCodes.Individual;

public class Gte : Full
{
  public Gte() : base("GTE", 0x24)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)Convert.ToInt32(a >= b);
  }
}