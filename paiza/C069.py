y, m, d = map(int, input().split())
a, b = map(int, input().split())
target_year = (y + 3) // 4 * 4 + 1
rem_month = 14 - m
days = 0

days += (target_year - y - 1) * (15 * 6 + 13 * 7)  # 年単位の計算
days += rem_month // 2 * 15 + (rem_month + 1) // 2 * 13 - d  # 年末までの日数を計算
days += (a - 1) // 2 * 15 + a // 2 * 13 + b  # 開催日までの日数を計算

print(days)
