SET R4 0 F
SET R3 0 0

start:
EQL R5 R3 R4
JPT R5 end
printScreenR[R3]
INC R3
JMP start

end:



//printScreen[A]