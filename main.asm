.386p

_TEXT   segment use32 dword public 'CODE'
        assume cs:_TEXT

stub:

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


end stub
