namespace CustomAssembly.OpCodes;

public abstract class FullHighLow : RootCode
{
  public FullHighLow(string id, byte @internal) : base(id, @internal)
  {
  }

  public abstract void Perform(int a, int b);

  override
    public bool Validate(bool a, bool b, bool c)
  {
    if (a && b && c)
    {
      return true;
    }
    return false;
  }

  override
    public void Process(int a, int b, int c)
  {
    int output = 0x0000;
    output |= b << 8;
    output |= c;
    Perform(a, output);
  }
}