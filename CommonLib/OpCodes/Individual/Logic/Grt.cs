namespace CustomAssembly.OpCodes.Individual;

public class Grt : Full
{
  public Grt() : base("GRT", 0x23)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)Convert.ToInt32(a > b);
  }
}