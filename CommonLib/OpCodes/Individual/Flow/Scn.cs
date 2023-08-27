namespace CustomAssembly.OpCodes.Individual;

public class Scn : RootCode
{
  public Scn() : base("SCN", 0x33)
  {
  }

  public override bool Validate(bool a, bool b, bool c)
  {
    if (a && b && c) return true;
    return false;
  }
  public override void Process(int a, int b, int c)
  {
    int address = 0x0000;
    address |= a << 8;
    address |= b;
    getRuntime().setScreen(address, getRuntime().getRegister(c));
    getRuntime().printScreen();
  }
}