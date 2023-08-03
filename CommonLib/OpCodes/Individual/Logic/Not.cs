namespace CustomAssembly.OpCodes.Individual;

public class Not : First
{
  public Not() : base("NOT", 0x28)
  {
  }

  public override ushort Perform(int a)
  {
    return (ushort)~a;
  }
}