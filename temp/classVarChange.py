class Human:
    def __init__(self, color, years):
        self.color = color
        self.years = years

    def __repr__(self):
        return self.__class__.__name__ + " " + self.color + " " + str(self.years)

    def set_person(self, **kwargs):
        print(kwargs)
        for k in kwargs:
            if k in vars(self):
                print("    exec(self." + k + ' = "' + kwargs[k] + '")')
                exec("self." + k + ' = "' + kwargs[k] + '"')
            else:
                print("    dont exec(self." + k + ' = "' + kwargs[k] + '")')


me = Human("red", 12)

print("before:", me)
me.set_person(color="green", hoge="piyo")
print("after:", me)
