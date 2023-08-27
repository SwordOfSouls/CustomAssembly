namespace CustomAssembly.OpCodes;

public interface IOpCode
{
  public void Process(int a, int b, int c);
}