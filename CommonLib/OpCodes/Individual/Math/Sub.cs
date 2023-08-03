namespace CustomAssembly.OpCodes.Individual;

public class Sub : Full
{
  public Sub() : base("SUB", 0x11)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a-b);
  }
}