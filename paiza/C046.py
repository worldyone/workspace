N = int(input())
persons = input().split()
person_purchases = {person: 0 for person in persons}
M = int(input())

for m in range(M):
    line = input().split()
    person, purchase_amount = line[0], int(line[1])
    person_purchases[person] += purchase_amount

ranking = sorted(person_purchases.items(), key=lambda x: x[1], reverse=True)
for k, v in ranking:
    print(k)
