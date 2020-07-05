N = int(input())
S = input()

T = [chr((ord(s)-65 + N)%26 + 65) for s in S]

print("".join(T))
