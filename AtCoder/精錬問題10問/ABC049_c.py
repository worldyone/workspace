S = input()
c = ["dream","dreamer","erase","eraser"]

S = S[::-1]
c = [cc[::-1] for cc in c]

i=0
while i < len(c):
    if S.startswith(c[i]):
        S = S[len(c[i]):]
        i = 0
        continue
    i = -~i

print("NYOE S"[S==""::2])
