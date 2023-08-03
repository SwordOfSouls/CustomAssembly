using System.Text;

namespace CustomAssembly;

public class Disassembler
{
  private readonly string destination;
  private readonly List<string> output;

  private readonly byte[] source;
  private readonly InternalProcessor _internalProcessor = new();

  public Disassembler(string source, string destination)
  {
    this.destination = destination;
    this.source = File.ReadAllBytes("../../../../"+source);

    output = new List<string>();
  }

  public void Process()
  {
    for (int i = 0; i < source.Length; i++)
    {
      var line = new StringBuilder();
      var asm = _internalProcessor.valueKey[source[i]];
      line.Append(asm.Id).Append(" ");
      line.Append(source[i+1].ToString("X")).Append(" ");
      line.Append(source[i+2].ToString("X")).Append(" ");
      line.Append(source[i+3].ToString("X"));
      i += 3;
      output.Add(line.ToString());
    }
  }

  public void Complete()
  {
    File.WriteAllLines("../../../../"+destination, output);
  }
}