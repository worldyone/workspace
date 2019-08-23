N = int(input())
S = input()
ans = ""

for i, s in enumerate(S):
    a = ord(s) + N if i % 2 else ord(s) - N
    if a < ord('A'):
        a += 26
    if a > ord('Z'):
        a -= 26

    ans += chr(a)

print(ans)
