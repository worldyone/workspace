
f = open("in.txt", 'r', encoding="utf-8_sig")

lst = f.readlines()

l = lst[::4]
print(l)

for a in l:
    print(a, end="")
