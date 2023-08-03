using CustomAssembly;

string source = "compiled.bin";

Console.WriteLine($"Beginning Emulation of '{source}'");
var emulator = new Emulator(source);
emulator.Run();
Console.WriteLine("Emulation Complete");