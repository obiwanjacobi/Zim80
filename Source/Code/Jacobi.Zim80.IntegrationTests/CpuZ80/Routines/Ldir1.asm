	org 0h
start:
	ld hl, src
	ld de, trg
	ld bc, 13
	ldir
end:
	nop

	defs 4
src:
	defb "Hello World!"

	defs 4
trg:
	defs 10h
	defb 0