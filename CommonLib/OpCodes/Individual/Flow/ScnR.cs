namespace CustomAssembly.OpCodes.Individual;

public class ScnR : RootCode
{
  public ScnR() : base("SCNR", 0x34)
  {
  }

  public override bool Validate(bool a, bool b, bool c)
  {
    if (a && b && !c) return true;
    return false;
  }
  public override void Process(int a, int b, int c)
  {
    getRuntime().setScreen(getRuntime().getRegister(a), getRuntime().getRegister(b));
    getRuntime().printScreen();
  }
}