N = int(input())
points = 0

for _ in range(N):
    day, price = input().split()
    price = int(price)

    if "3" in day:
        points += int(price * 0.03)
    elif "5" in day:
        points += int(price * 0.05)
    else:
        points += int(price * 0.01)

print(points)
