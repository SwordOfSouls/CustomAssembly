printScreenR [color]

SET R0 04 00
SET R19 0 0
loop:
SCNR R19 color
INC R19

EQL R1 R0 R19
JPT R1 end
JMP loop

end: