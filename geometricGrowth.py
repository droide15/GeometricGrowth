#%%
rows = 5
cols = 6
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
#%%
print('hSpan: ', hSpan)
print('vSpan: ', vSpan)
print('left: ', left)
print('up: ', up)
print('right: ', right)
print('down: ', down)
print('growArea: ' , growArea)
#%%
hStart = hMid
hEnd = hMid
vStart = vMid
vEnd = vMid
if cols%2 == 0:
    hEnd += 1

if rows%2 == 0:
    vEnd += 1

growDir = '*'
if cols >= rows:
    growDir = 'V'
else:
    growDir = 'H'
#%%
print('growDir: ', growDir)
print('hStart: ', hStart)
print('hEnd: ', hEnd)
print('vStart: ', vStart)
print('vEnd: ', vEnd)
#%%
if growDir == '*':
    hStart -= 1
    hEnd += 1
    vStart -= 1
    vEnd += 1

if cols >= rows:
    if growDir == 'V':
        growDir = 'H'
    elif growDir == 'H':
        growDir = '*'
        if hStart - 1 > left:
            growDir = 'V'
        
        hStart -= 1
        hEnd += 1
        vStart -= 1
        vEnd += 1
    else:
        growDir = 'V'
else:
    if growDir == 'V':
        growDir = '*'
        if vStart - 1 > up:
            growDir = 'H'
        
        hStart -= 1
        hEnd += 1
        vStart -= 1
        vEnd += 1
    elif growDir == 'H':
        growDir = 'V'
    else:
        growDir = 'H'
#%%
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
