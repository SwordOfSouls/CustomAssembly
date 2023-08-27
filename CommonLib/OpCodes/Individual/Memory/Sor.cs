namespace CustomAssembly.OpCodes.Individual;

public class Sor : FullHighLow
{
  public Sor() : base("SOR", 0x41)
  {
  }

  public override void Perform(int a, int b)
  {
        getRuntime().setStack(b, getRuntime().getRegister(a));
  }
}