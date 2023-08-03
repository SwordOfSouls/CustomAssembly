namespace CustomAssembly.OpCodes;

public abstract class None : RootCode
{
  public None(string id, byte @internal) : base(id, @internal)
  {
  }

  override 
    public bool Validate(bool a, bool b, bool c)
  {
    if (!a && !b && !c)
    {
      return true;
    }
    return false;
  }

  override
    public void Process(int a, int b, int c)
  {
  }
}