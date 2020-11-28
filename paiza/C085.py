alphabet_count = list(map(int, input().split()))
string = input()

for s in string:
    s_order = ord(s) - ord('a')
    if alphabet_count[s_order] > 0:
        alphabet_count[s_order] -= 1
        print(s, end="")
