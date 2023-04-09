.386p

_TEXT   segment use32 dword public 'CODE'
        assume cs:_TEXT

extrn   __managed__Main

stub:
        jmp     main
        db      'WATCOM' ; needed by DOS/4GW, but not by DOS/32A or PMODE/W
main:
        mov     ax, ds
        mov     es, ax

        push    0
        push    0
        call    near ptr __managed__Main
        mov     ah, 4Ch
        int     21h

public _exit
_exit proc
        pop eax
        pop eax
        mov ah, 4Ch
        int 21h
endp _exit

public _halt
_halt proc
        hlt
        ret
endp _halt

public _keyavail
_keyavail proc
        mov ah, 0Bh
        int 21h
        and eax, 0FFh
        ret
endp _keyavail

public _readkey
_readkey proc
        mov ah, 08h
        int 21h
        and eax, 0FFh
        ret
endp _readkey

public  _call
        _call proc
        pop     ecx
        pop     eax
        push    ecx
        jmp     eax
endp    _call

_TEXT   ends


_DATA   segment use32 dword public 'DATA'

public  __security_cookie
        __security_cookie dd 0

_DATA   ends


_STACK  segment para stack 'STACK'
        db 40000h dup(?)
_STACK  ends


end stub
