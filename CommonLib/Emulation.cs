namespace CustomAssembly;

public interface Emulation
{
  public void setScreen(int addr, ushort value);
  public void setHeap(int addr, ushort value);
  public void setStack(int addr, ushort value);
  public void setRegister(int addr, ushort value);
  
  public ushort getRegister(int addr);
  public ushort getStack(int addr);
  public ushort getHeap(int addr);
  
  
  public void Jump(ushort addr);
  public void printScreen();
}