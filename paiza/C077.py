students, questions = list(map(int, input().split()))

for _ in range(students):
    d, a = list(map(int, input().split()))
    score = 100 // questions * a

    if 0 < d < 10:
        score = int(score * 0.8)
    elif d >= 10:
        score = 0

    if score >= 80:
        eval = "A"
    elif score >= 70:
        eval = "B"
    elif score >= 60:
        eval = "C"
    else:
        eval = "D"

    print(eval)
