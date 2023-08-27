namespace CustomAssembly.OpCodes.Individual;

public class Jpt : FullHighLow
{
  public Jpt() : base("JPT", 0x31)
  {
  }

  public override void Perform(int a, int b)
  {
    if (a != 0)
    {
      getRuntime().Jump((ushort)b);
    }
  }

    override
  public void Process(int a, int b, int c)
  {
    int output = 0x0000;
    output |= b << 8;
    output |= c;
    Perform(getRuntime().getRegister(a), output);
  }
}