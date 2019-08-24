S, T = input(), input()

def func(S, T):
    # tの文字がsになかったら条件を満たさない
    for t in T:
        if t not in S:
            return -1

    ans = 0
    len_s = len(S)
    index = pre_index = -1

    for i in range(len(T)):
        while True:
            index += 1
            if T[i] == S[index%len_s]:
                ans += index - pre_index
                pre_index = index
                break
    return ans

print(func(S,T))
