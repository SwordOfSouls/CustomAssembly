namespace CustomAssembly.OpCodes.Individual;

public class Lte : Full
{
  public Lte() : base("LTE", 0x26)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)Convert.ToInt32(a <= b);
  }
}