from kivy.app import App
from kivy.uix.button import Button
from kivy.uix.dropdown import DropDown


class MyDropDown(DropDown):
    def on_select(self, data):
        self.attach_to.text = data


class MyButton(Button):
    dropdown = None

    def __init__(self, **kwargs):
        super(MyButton, self).__init__(**kwargs)
        self.dropdown = MyDropDown()

    def on_release(self):
        self.dropdown.open(self)


class dropdownApp(App):
    title = 'DropDown Test'


dropdownApp().run()
