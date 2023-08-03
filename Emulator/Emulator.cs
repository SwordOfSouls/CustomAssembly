using System.Numerics;
using System.Text;
using CustomAssembly.OpCodes;
using Pastel;

namespace CustomAssembly;

public class Emulator : Emulation
{
    private long[] registers = new long[32];
    private ushort[] ram = new ushort[65534];
    private Memory<ushort> stack;
    private Memory<ushort> heap;

    private InternalProcessor _processor;
    private byte[] rom;


    private ushort instructionCounter = 31;
    private Memory<ushort> screen;

    public Emulator(String source)
    {
        stack = ram.AsMemory().Slice(0, 4096);
        screen = ram.AsMemory().Slice(4096, 1025);
        heap = ram.AsMemory().Slice(4096+1024, 65534-(4096+1025));
        rom = File.ReadAllBytes("../../../../" + source);
        _processor = new InternalProcessor(this);
        Console.Write("\n");
        Console.SetBufferSize(0x100, 0xFFF);
        var stdout = Console.OpenStandardOutput();
        var con = new StreamWriter(stdout, Encoding.ASCII);
        con.AutoFlush = true;
        Console.SetOut(con);
    }

    public void Run()
    {
        for (ushort i = 0; registers[instructionCounter] < rom.Length; registers[instructionCounter] += 4)
        {
            byte id = rom[registers[instructionCounter]];

            byte arg1 = rom[registers[instructionCounter] + 1];
            byte arg2 = rom[registers[instructionCounter] + 2];
            byte arg3 = rom[registers[instructionCounter] + 3];
            RootCode rootCode = _processor.valueKey[id];
            rootCode.Process(arg1, arg2, arg3);
        }
    }

    public void printScreen()
    {
        Span<ushort> span = screen.Span;
   
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 1);
        StringBuilder screenPrint = new StringBuilder();
        for (int i = 0; i < span.Length-1; i++)
        {
            if (i % 32 == 0) screenPrint.Append("\n".PastelBg(ConsoleColor.Black));
            screenPrint.Append("  ".PastelBg((ConsoleColor) span[i]));
        }
        screenPrint.Append("\n".PastelBg(ConsoleColor.Black));
        Console.Write(screenPrint);
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
        registers[instructionCounter] = addr;
    }
}