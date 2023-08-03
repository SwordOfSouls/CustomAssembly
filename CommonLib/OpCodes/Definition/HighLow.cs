namespace CustomAssembly.OpCodes;

public abstract class HighLow : RootCode
{
  public HighLow(string id, byte @internal) : base(id, @internal)
  {
  }

  public abstract void Perform(int a);

  override
    public bool Validate(bool a, bool b, bool c)
  {
    if (a && b && !c)
    {
      return true;
    }
    return false;
  }

  override
    public void Process(int a, int b, int c)
  {
    int output = 0x0000;
    output |= a << 8;
    output |= b;
    Perform(output);
  }
}