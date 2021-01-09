num_students, lower_rank = map(int, input().split())

students = []
for _ in range(num_students):
    students.append(list(map(int, input().split())))

englishs, japanese, maths = zip(*students)

english_lower_score = sorted(englishs)[lower_rank - 1]
japanese_lower_score = sorted(japanese)[lower_rank - 1]
maths_lower_score = sorted(maths)[lower_rank - 1]

for eng, jap, mat in students:
    ans = (eng <= english_lower_score) + \
        (jap <= japanese_lower_score) + (mat <= maths_lower_score)
    print(ans)
