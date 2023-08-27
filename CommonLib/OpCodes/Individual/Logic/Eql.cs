namespace CustomAssembly.OpCodes.Individual;

public class Eql : Full
{
  public Eql() : base("EQL", 0x27)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)Convert.ToInt32(a == b);
  }
}