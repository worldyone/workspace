recipe_cnt = int(input())
recipes = {}
for _ in range(recipe_cnt):
    lines = input().split()
    a, b = lines[0], int(lines[1])
    recipes[a] = b

m_cnt = int(input())
m = {}
for _ in range(m_cnt):
    lines = input().split()
    a, b = lines[0], int(lines[1])
    m[a] = b

ans = 0
flg = True
while(flg):
    for k, v in recipes.items():
        if k in m.keys() and m[k] - v >= 0:
            m[k] -= v
        else:
            flg = False
            break
    else:
        ans += 1

print(ans)
