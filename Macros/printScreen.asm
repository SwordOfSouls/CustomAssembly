printScreen [color]

SET R0 4 00
SET R19 0 0
SET R1E 00 color
loop:
SCNR R19 R1E
INC R19

EQL R1 R0 R19
JPT R1 end
JMP loop

end: