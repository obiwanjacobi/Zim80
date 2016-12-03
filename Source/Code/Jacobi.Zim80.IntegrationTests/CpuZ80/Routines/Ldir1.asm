	org 0h
	ld hl, src
	ld de, trg
	ld bc, 13
	ldir

	defs 5
src:
	defb "Hello World!"

	defs 4
trg:
	defs 10h