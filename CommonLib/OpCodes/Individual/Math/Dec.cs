namespace CustomAssembly.OpCodes.Individual;

public class Dec : First
{
  public Dec() : base("DEC", 0x16)
  {
  }

  public override ushort Perform(int a)
  {
    return (ushort)(a-1);
  }
}