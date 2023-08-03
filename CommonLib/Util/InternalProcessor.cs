using System.Reflection;
using CustomAssembly.OpCodes;

namespace CustomAssembly;

public class InternalProcessor
{
  public Dictionary<string, RootCode> keyValue = new();
  public Dictionary<byte, RootCode> valueKey = new();

  public InternalProcessor()
  {
    var types = Assembly.GetExecutingAssembly().GetTypes();
    foreach (var type in types)
      if (type.IsAssignableTo(typeof(RootCode)) && !type.IsInterface && !type.IsAbstract)
      {
        var code = (RootCode)Activator.CreateInstance(type);
        keyValue.Add(code.Id, code);
        valueKey.Add(code.Internal, code);
      }
  }
  public InternalProcessor(Emulation runtime)
  {
    var types = Assembly.GetExecutingAssembly().GetTypes();
    foreach (var type in types)
      if (type.IsAssignableTo(typeof(RootCode)) && !type.IsInterface && !type.IsAbstract)
      {
        var code = (RootCode)Activator.CreateInstance(type);
        code.setRuntime(runtime);
        keyValue.Add(code.Id, code);
        valueKey.Add(code.Internal, code);
      }
  }
}