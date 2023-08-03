namespace CustomAssembly.OpCodes.Individual;

public class Inc : First
{
  public Inc() : base("INC", 0x15)
  {
  }

  public override ushort Perform(int a)
  {
    return (ushort)(a+1);
  }
}