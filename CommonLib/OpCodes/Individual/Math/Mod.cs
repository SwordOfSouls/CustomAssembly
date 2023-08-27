namespace CustomAssembly.OpCodes.Individual;

public class Mod : Full
{
  public Mod() : base("MOD", 0x14)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a % b);
  }
}