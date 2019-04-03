creating collage object
rows = 4
cols = 5
hSpan = 1
vSpan = 1
if rows%2 == 0:
    vSpan += 1

if cols%2 == 0:
    hSpan += 1

hMid = int(cols/2) - hSpan + 1
vMid = int(rows/2) - vSpan + 1
left = hMid
up = vMid
right = hMid + hSpan - 1
down = vMid + vSpan - 1
growArea = None
#############################
hSpan
vSpan
left
up
right
down
#############################

stitch next images
calculate growth direction
calculate growth area

-----------------------------
growDir = '*'
if growArea is not None:
    if cols-hSpan > rows-vSpan:
        hSpan += 2
        left -= 1
        right += 1
    elif rows-vSpan > cols-hSpan:
        vSpan += 2
        up -= 1
        down += 1
    else:
        if cols >= rows:
            growDir = 'V'
        else:
            growDir = 'H'
        hSpan += 2
        vSpan += 2
        left -= 1
        right += 1
        up -= 1
        down += 1

growArea = hSpan*vSpan
-----------------------------