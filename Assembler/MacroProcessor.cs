using System.Text;
using System.Text.RegularExpressions;
using CustomAssembly.OpCodes.Individual;

namespace CustomAssembly;

public class MacroProcessor
{
  public Dictionary<string, Macro> macros = new();

  public MacroProcessor(string dir)
  {
    List<string[]> files = new List<string[]>();
    foreach (var name in Directory.GetFiles(dir))
    {
      if (name.Contains(".bak")) continue;
      files.Add(File.ReadAllLines(name));
    }
    foreach (var macro in files)
    {
      string top = macro[0];
      string[] splitTop = top.Split(" ");
      string id = splitTop[0];
      var ids = Regex.Matches(splitTop[1], @"([^,\[\]]+)").Select(match => match.Value).ToList();
      List<String> content = new List<string>();
      for (int i = 1; i < macro.Length; i++) content.Add(macro[i]);
      if(splitTop.Length>2) macros.Add(id, new Macro(id, splitTop[3],ids.ToArray(), content.ToArray()));
      else macros.Add(id, new Macro(id, null,ids.ToArray(), content.ToArray()));
    }
  }
}