	org 0h
start:
	jp init
	
loop:
	call copy
	cp 0
	jp z, end
	jp next
end:
	nop

init:
	ld hl, src
	ld de, trg
	jp loop

copy:
	ld a, (hl)
	ld (de), a
	ret

next:
	inc hl
	inc de
	jp loop

	defs 5
src:
	defb "Hello World!", 0

	defs 3
trg:
	defs 10h
	defb 0