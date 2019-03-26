def paint_map(li, h, w):
    if(li[h][w] == label_number):
        global count
        count += 1
        return
    if(li[h][w] != -1):
        return

    li[h][w] = label_number

    if(cycles[h][w] == "U" and h != 0):
        h -= 1
    elif(cycles[h][w] == "D" and h != H):
        h += 1
    elif(cycles[h][w] == "L" and w != 0):
        w -= 1
    elif(cycles[h][w] == "R" and w != W):
        w += 1
    else:
        return

    paint_map(li, h, w)



H, W = map(int, input().split())

cycles = []
for h in range(H):
    cycles.append(input())

li = [[-1]*W for h in range(H)]

count = 0
label_number = 0

for h in range(H):
    for w in range(W):
        if(li[h][w] == -1):
            label_number += 1
            paint_map(li, h, w)

print(count)
