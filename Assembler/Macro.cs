using System.Security;
using System.Text.RegularExpressions;

namespace CustomAssembly;

public class Macro
{
  public readonly string Id;
  public readonly string[] Params;
  public readonly string[] Content;
  public readonly string? Return;

  public Macro(string id, string? @return, string[] @params, string[] content)
  {
    this.Id = id;
    this.Params = @params;
    this.Content = content;
    this.Return = @return;
  }

  public string[] Process(string[] @params)
  {
    if (Params.Length != @params.Length) throw new SecurityException("Invalid Macro Input!");

    string[] output = (string[])Content.Clone();
    for (int i = 0; i < @params.Length; i++)
    {
      for (int j = 0; j < output.Length; j++)
      {
        output[j] = output[j].Replace(Params[i], @params[i]);
      }
    }
    Labels(output);
    return output;
  }

  public void Labels(string[] output)
  {
    Dictionary<string, string> rebuiltLabels = new();
    for (int i = 0; i < output.Length; i++)
    {
      if (regex(Content[i], "^[a-zA-z0-9]{1,}[:]$"))
      {
        rebuiltLabels.Add(Content[i].Split(":")[0], Guid.NewGuid().ToString());
      }
    }
    foreach (String key in rebuiltLabels.Keys)
    {
      for (int j = 0; j < output.Length; j++)
      {
        output[j] = output[j].Replace(key, rebuiltLabels[key]);
      }
    }
  }
  
  public bool regex(string input, string regex)
  {
    var match = Regex.Match(input, regex);
    return match.Success;
  }
}