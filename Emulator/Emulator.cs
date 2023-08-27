using System.Numerics;
using System.Text;
using CustomAssembly.OpCodes;
using Pastel;

namespace CustomAssembly;

public class Emulator : Emulation
{
    private readonly long[] registers = new long[32];
    private readonly ushort[] ram = new ushort[65534];
    private readonly Memory<ushort> stack;
    private readonly Memory<ushort> heap;

    private readonly InternalProcessor processor;
    private readonly byte[] rom;


    private const ushort InstructionCounter = 31;
    private readonly Memory<ushort> screen;

    public Emulator(String source)
    {
        stack = ram.AsMemory().Slice(0, 4096);
        screen = ram.AsMemory().Slice(4096, 1025);
        heap = ram.AsMemory().Slice(4096+1024, 65534-(4096+1025));
        rom = File.ReadAllBytes("../../../../" + source);
        processor = new InternalProcessor(this);
        Console.Write("\n");
        try
        {
            Console.SetBufferSize(0x100, 0xFFF);
        }
        catch (Exception ignored)
        {
        }
        var stdout = Console.OpenStandardOutput();
        var con = new StreamWriter(stdout, Encoding.ASCII);
        con.AutoFlush = true;
        Console.SetOut(con);
        Console.CursorVisible = false;
    }

    public void Run()
    {
        for (byte i = 0; registers[InstructionCounter] < rom.Length; registers[InstructionCounter] += 4)
        {
            byte id = rom[registers[InstructionCounter]];

            byte arg1 = rom[registers[InstructionCounter] + 1];
            byte arg2 = rom[registers[InstructionCounter] + 2];
            byte arg3 = rom[registers[InstructionCounter] + 3];
            RootCode rootCode = processor.valueKey[id];
            rootCode.Process(arg1, arg2, arg3);
        }
    }

    public void printScreen()
    {
        Span<ushort> span = screen.Span;
        
        Console.SetCursorPosition(0, 1);
        StringBuilder screenPrint = new StringBuilder();
        for (int i = 0; i < span.Length-1; i++)
        {
            if (i % 32 == 0) screenPrint.Append("\n".PastelBg(ConsoleColor.Black));
            screenPrint.Append("  ".PastelBg((ConsoleColor) span[i]));
        }
        screenPrint.Append("\n".PastelBg(ConsoleColor.Black));
        Console.Write(screenPrint);
        screenPrint = null;
    }

    public void setScreen(int addr, ushort value)
    {
        screen.Span[addr] = value;
    }

    public void setHeap(int addr, ushort value)
    {
        heap.Span[addr] = value;
    }

    public void setStack(int addr, ushort value)
    {
        stack.Span[addr] = value;
    }

    public void setRegister(int addr, ushort value)
    {
        registers[addr] = value;
    }

    public ushort getRegister(int addr)
    {
        return (ushort)registers[addr];
    }

    public ushort getStack(int addr)
    {
        return stack.Span[addr];
    }

    public ushort getHeap(int addr)
    {
        return heap.Span[addr];
    }

    public void Jump(ushort addr)
    {
        registers[InstructionCounter] = addr;
    }
}