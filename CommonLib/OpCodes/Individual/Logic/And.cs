namespace CustomAssembly.OpCodes.Individual;

public class And : Full
{
  public And() : base("AND", 0x20)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a & b);
  }
}