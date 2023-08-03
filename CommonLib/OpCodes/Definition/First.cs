namespace CustomAssembly.OpCodes;

public abstract class First : RootCode
{
  public First(string id, byte @internal) : base(id, @internal)
  {
  }

  public abstract ushort Perform(int a);

  override
    public bool Validate(bool a, bool b, bool c)
  {
    if (a && !b && !c)
    {
      return true;
    }
    return false;
  }

  override
    public void Process(int a, int b, int c)
  {
    getRuntime().setRegister(a, Perform(getRuntime().getRegister(a)));
  }
}