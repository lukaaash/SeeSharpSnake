csc.exe /O /noconfig /nostdlib /runtimemetadataversion:v4.0.30319 MiniRuntime.cs MiniRuntime.Dos.cs MiniBCL.cs Game\FrameBuffer.cs Game\Random.cs Game\Game.cs Game\Snake.cs Pal\Thread.Dos.cs Pal\Environment.Dos.cs Pal\Console.Dos.cs Pal\Console.cs Pal\RuntimeInformation.Dos.cs /out:zerosnake.ilexe /langversion:latest /unsafe

ilc.exe zerosnake.ilexe -o zerosnake.obj --directpinvoke:main --systemmodule:zerosnake --Os -g --targetarch x86 --instruction-set base

tasm32 /ml main.asm

wlink system pmodew name snake.exe file zerosnake.obj,main.obj

@echo Run this in DOS to produce a 14369-byte long executable:
@echo pmwlite /C4 snake.exe
