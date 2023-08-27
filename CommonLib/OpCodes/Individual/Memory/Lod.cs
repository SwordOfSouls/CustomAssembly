namespace CustomAssembly.OpCodes.Individual;

public class Cop : FullHighLow
{
  public Cop() : base("LOD", 0x42)
  {
  }

  public override void Perform(int a, int b)
  {
    getRuntime().setRegister(a, getRuntime().getStack(b));
  }
}