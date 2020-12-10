def calc_salary(time):
    salary = 0
    if 0 <= time <= 9:
        salary = time * t_other
    elif 9 < time <= 17:
        salary = 9 * t_other + (time - 9) * t9_17
    elif 17 < time <= 22:
        salary = 9 * t_other + 8 * t9_17 + (time - 17) * t17_22
    else:
        salary = 9 * t_other + 8 * t9_17 + 5 * t17_22 + (time - 22) * t_other
    return salary


t9_17, t17_22, t_other = map(int, input().split())
N = int(input())
income = 0

for _ in range(N):
    start, end = map(int, input().split())
    income += calc_salary(end) - calc_salary(start)

print(income)
