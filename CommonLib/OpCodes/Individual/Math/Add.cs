namespace CustomAssembly.OpCodes.Individual;

public class Add : Full
{
  public Add() : base("ADD", 0x10)
  {
  }

  public override ushort Perform(int a, int b)
  {
    return (ushort)(a+b);
  }
}