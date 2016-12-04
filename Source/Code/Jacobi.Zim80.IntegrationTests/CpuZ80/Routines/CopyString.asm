	org 0h
start:
	ld hl, src
	ld de, trg
loop:
	ld a, (hl)
	ld (de), a
	cp 0
	jr z, end
	inc hl
	inc de
	jr loop
end:
	nop

	defs 4
src:
	defb "Hello World!", 0

	defs 3
trg:
	defs 10h
	defb 0