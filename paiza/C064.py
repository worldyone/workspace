M, N = list(map(int, input().split()))

menus = [int(input()) for _ in range(M)]
students_menu = [list(map(int, input().split())) for _ in range(N)]

for student_order in range(N):
    calories = 0
    for i in range(M):
        calories += (students_menu[student_order][i] * menus[i]) // 100
    print(calories)
