using CustomAssembly;

string source = "source.asm";
string target = "compiled.bin";

Console.WriteLine($"Assembling '{source}' to target '{target}'");
var assembler = new Assembler(source, target);
assembler.Multiplier();
assembler.MacroProcessing();
assembler.PreProcessing();
assembler.Process();
assembler.Complete();
Console.WriteLine("Assembley Completed");