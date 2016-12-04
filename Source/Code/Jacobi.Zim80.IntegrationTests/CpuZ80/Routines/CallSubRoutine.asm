	org 0h
start:
	ld sp, stack
	ld hl, src
	ld de, trg
loop:
	call copy
	cp 0
	jr z, end
	call next
	jr loop
end:
	nop

copy:
	ld a, (hl)
	ld (de), a
	ret

next:
	inc hl
	inc de
	ret

	defs 4
src:
	defb "Hello World!", 0

	defs 3
trg:
	defs 10h
	defb 0

	defs 10h
stack:
	defb 0