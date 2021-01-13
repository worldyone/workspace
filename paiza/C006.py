from decimal import Decimal, ROUND_HALF_UP

N, M, K = map(int, input().split())
items_value = list(map(Decimal, input().split()))

ms = []
for i in range(M):
    items = list(map(int, input().split()))
    value = sum([x * y for (x, y) in zip(items, items_value)])
    ms.append(value)

ms = sorted(ms, reverse=True)
ms = list(map(lambda x: str(x.quantize(Decimal('0'), rounding=ROUND_HALF_UP)), ms))
print("\n".join(ms[:K]))
