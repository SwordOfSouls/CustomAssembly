namespace CustomAssembly.OpCodes.Individual;

public class Set : FullHighLow
{
  public Set() : base("SET", 0x40)
  {
  }

  public override void Perform(int a, int b)
  {
    getRuntime().setRegister(a, (ushort)b);
  }
}