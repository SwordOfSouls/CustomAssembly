using CustomAssembly;

string source = "compiled.bin";
string target = "disasource.asm";

Console.WriteLine($"Disassembling '{source}' to target '{target}'");
var disassembler = new Disassembler(source, target);
disassembler.Process();
disassembler.Complete();
Console.WriteLine("Disassembley Complete");