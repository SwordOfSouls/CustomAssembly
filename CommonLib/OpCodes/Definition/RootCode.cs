namespace CustomAssembly.OpCodes;

public abstract class RootCode : ILayout, IOpCode
{
  public readonly string Id;
  public readonly byte Internal;
  public bool fillIn = false;
  private Emulation runtime;

  public RootCode(string id, byte @internal)
  {
    Id = id;
    Internal = @internal;
  }

  public RootCode(string id, byte @internal, Emulation runtime)
  {
    Id = id;
    Internal = @internal;
    this.runtime = runtime;
  }

  public abstract bool Validate(bool a, bool b, bool c);
  public abstract void Process(int a, int b, int c);

  public Emulation getRuntime()
  {
    return runtime;
  }
  public void setRuntime(Emulation emulation)
  {
    runtime = emulation;
  }
}