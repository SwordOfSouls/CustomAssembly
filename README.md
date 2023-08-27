<div align="center">

[![Language][language-dom]][github-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![GPL License][license-shield]][license-url]


<h1>Custom Assembly</h1>
  <p>
    A project built for fun implementing a rudimentary Custom ISA (also built for fun) within a Virtual C# Emulator based on RISC hardware. <br />
    <a href="https://docs.google.com/document/d/1I8XQ1ReggOCb6f_LnGRW6zIg9mHQOgjT-SO7UEkCNIM/edit">ISA</a>
  </p>
</div>

## Tests
Running tested on Windows using the following:
- Rider
- Visual Studio

## Prerequisites 
Your Windows Console settings must have the Console Buffer Rows to 50 (Launch Size)

## Examples
#### Set Screen to all Possible Colors
```
SET R4 0 F
SET R3 0 0

start:
EQL R5 R3 R4
JPT R5 end
printScreenR[R3]
INC R3
JMP start

end:
```

#### Set Screen to Given Color
```
printScreen[A]
```

<!-- LINK DUMP -->
[language-dom]: https://img.shields.io/github/languages/top/SwordOfSouls/CustomAssembly?style=for-the-badge
[stars-shield]: https://img.shields.io/github/stars/SwordOfSouls/CustomAssembly?style=for-the-badge
[stars-url]: https://github.com/SwordOfSouls/CustomAssembly/stargazers
[issues-shield]: https://img.shields.io/github/issues/SwordOfSouls/CustomAssembly?style=for-the-badge
[issues-url]: https://github.com/SwordOfSouls/CustomAssembly/issues
[license-shield]: https://img.shields.io/github/license/SwordOfSouls/CustomAssembly?style=for-the-badge
[license-url]: https://github.com/SwordOfSouls/CustomAssembly/blob/master/LICENSE
[github-url]: https://github.com/SwordOfSouls/CustomAssembly/
