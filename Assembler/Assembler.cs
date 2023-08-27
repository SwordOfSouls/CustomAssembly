using System.Globalization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using CustomAssembly.OpCodes.Individual;

namespace CustomAssembly;

public class Assembler
{
  private readonly string destination;
  private readonly List<byte> output;

  private readonly List<string> source;
  private readonly InternalProcessor _internalProcessor = new();
  private readonly MacroProcessor _macroProcessor = new("../../../../Macros");

  private int instructionCount;
  private readonly Dictionary<string, int> labelMap = new();

  public Assembler(string source, string destination)
  {
    this.destination = destination;
    this.source = new List<string>(File.ReadAllLines("../../../../"+source));

    output = new List<byte>();
  }

  public void MacroProcessing()
  {
    for (int i = 0; i < source.Count; i++)
    {
      source[i] = source[i].Trim();
      //comments
      if (regex(source[i], "^([/][/])"))
      {
        source.RemoveAt(i);
        i--;
        continue;
      }
      string[] split = source[i].Split("*");
      if (split.Length == 2)
      {
        int multiplyBy = Int32.Parse(split[1]);
        source[i] = split[0];
        string iReplacement = Regex.Match(source[i], @"@i[|]\d").Value;
        int sizing = Convert.ToInt32(iReplacement.Split("|")[1]);
        for (int a = 0; a < multiplyBy; a++)
        {
          string replacement = (a+1).ToString("X");
          if (sizing == 2)
          {
            if (replacement.Length < 3) replacement = "0 "+replacement;
            else if (replacement.Length == 3)
            {
              char[] chars = replacement.ToCharArray();
              replacement = chars[0]+" "+chars[1]+chars[2];
            }
            else if (replacement.Length == 4)
            {
              char[] chars = replacement.ToCharArray();
              replacement = chars[0]+chars[1] + " "+chars[2]+chars[3];
            }
          }
          source.Insert(i+1+a, source[i].Replace(iReplacement, replacement));
        }
        if(sizing==1) source[i] = source[i].Replace(iReplacement, "0").Trim();
        else source[i] = source[i].Replace(iReplacement, "0 0").Trim();
      }
      
      string? macroSearch = findMacro(source[i]);
      if (macroSearch!=null)
      {
        string targetMacro = macroSearch;
        var parts = Regex.Matches(targetMacro.Replace("_", " "), @"([^,\[\]]+)").Select(match => match.Value).ToList();
        string id = parts[0];
        if (!_macroProcessor.macros.ContainsKey(parts[0])) throw new FormatException($"Macro '{id}' doesn't exist!");
        parts.RemoveAt(0);

        Macro macro = _macroProcessor.macros[id];
        bool inline = source[i] != targetMacro;
        if (inline && macro.Return == null) throw new SecurityException($"Inline Macro '{id}' has no return!");
        if (inline) source[i] = source[i].Replace(targetMacro, macro.Return);
        else source.RemoveAt(i);
        string[] content = macro.Process(parts.ToArray());
        source.InsertRange(i, content);
        continue;
      }
    }
  }

  public string? findMacro(string input)
  {
    var matches = Regex.Matches(input, @"[a-zA-Z0-9]+[\[](.+)[\]]").Select(match => match.Groups[1].Value).ToList();
    foreach (var innerMatch in matches)
    {
      var internalMatches = Regex.Matches(innerMatch, @"[a-zA-Z0-9]+[\[](.+)[\]]").Select(match => match.Value).ToList();
      if (internalMatches.Count == 0) return input;
      return findMacro(innerMatch);
    }
    return null;
  }
  public void PreProcessing()
  {
    for (int i = 0; i < source.Count; i++)
    {
      if (source[i].Length == 0)
      {
        source.RemoveAt(i);
        i--;
        continue;
      }
      //comments
      if (regex(source[i], "^([/][/])"))
      {
        source.RemoveAt(i);
        i--;
        continue;
      }
      //labels
      if (regex(source[i], "^.{1,}[:]$"))
      {
        labelMap.Add(source[i].Split(":")[0], (instructionCount-1)*4);
        source.RemoveAt(i);
        i--;
        continue;
      }

      //word processing
      string[] args = source[i].Split();
      var fullArgumentBuilder = new StringBuilder();
      for (int a = 0; a < args.Length; a++)
      {
        string arg = args[a];
        // registers ex: R5
        if (regex(arg, "^[R]([a-zA-Z0-9])+$"))
        {
          arg = arg.Replace("R", "");
        }

        fullArgumentBuilder.Append(arg);
        if (a+1 < args.Length)
        {
          fullArgumentBuilder.Append(" ");
        }
      }
      source[i] = fullArgumentBuilder.ToString();
      instructionCount++;
    }
  }
  public void Process()
  {
    for (int i = 0; i < source.Count; i++)
    {
      string str = source[i];
      if (str.Length == 0)
      {
        continue;
      }
      string[] args = str.Split(" ");
      bool[] rawArgs = new bool[3];
      byte[] localOut = new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };

      for (int a = 1; a < 4; a++)
      {
        bool exists = Contains(args, a);
        if (exists)
        {
          if (labelMap.ContainsKey(args[a]))
          {
            localOut[a] = (byte)(labelMap[args[a]] >> 8);
            localOut[a+1] = (byte)labelMap[args[a]];
            rawArgs[a] = true;
            rawArgs[a-1] = true;
            a++;
          }
          else
          {
            localOut[a] = (byte)Parse(args, a);
            rawArgs[a-1] = true;
          }
        }
        else
        {
          localOut[a] = 0x00;
          rawArgs[a-1] = false;
        }
      }
      string opCode = args[0];
      var codes = _internalProcessor.keyValue;
      if (!codes.ContainsKey(opCode))
      {
        throw new SecurityException("OpCode '"+opCode+"' Doesn't Exist!");
      }
      var code = codes[opCode];
      if (!code.Validate(rawArgs[0], rawArgs[1], rawArgs[2]))
      {
        throw new FormatException("Invalid Layout for OpCode '"+opCode+"'!");
      }
      localOut[0] = code.Internal;
      output.AddRange(localOut);
    }
  }
  public void Complete()
  {
    File.WriteAllBytes("../../../../"+destination, output.ToArray());
  }

  public int? Parse(string[] args, int index)
  {
    if (args.Length <= index)
    {
      return null;
    }
    return int.Parse(args[index], NumberStyles.HexNumber);
  }
  public bool Contains(string[] args, int index)
  {
    if (args.Length <= index)
    {
      return false;
    }
    return true;
  }
  public bool regex(string input, string regex)
  {
    var match = Regex.Match(input, regex);
    return match.Success;
  }
}