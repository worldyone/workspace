from kivy.app import App
from kivy.core.window import Window
from kivy.properties import NumericProperty
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.label import Label
from kivy.uix.button import Button
from kivy.uix.widget import Widget


def my_on_touch_down(*args):
    print('You touched the window.')


Window.bind(on_touch_down=my_on_touch_down)


class MyWidget(Widget):
    no = NumericProperty(0)

    def on_touch_down(self, touch):
        if self.collide_point(*touch.pos):
            print('You touched the widget ' + str(self.no) + '.')
        return super(MyWidget, self).on_touch_down(touch)


class MyBoxLayout(MyWidget, BoxLayout):
    pass


class MyButton(MyWidget, Button):
    pass


class MyLabel(MyWidget, Label):
    pass


class TouchEventTestApp(App):
    def build(self):
        root = MyBoxLayout(no=1)
        w2 = MyBoxLayout(no=2, orientation='vertical')
        w3 = MyLabel(no=3, text='3')
        w4 = MyButton(no=4, text='4')
        w5 = MyBoxLayout(no=5)
        w6 = MyLabel(no=6, text='6')
        w7 = MyButton(no=7, text='7')
        w8 = MyBoxLayout(no=8, orientation='vertical')
        w9 = MyLabel(no=9, text='9')
        w5.add_widget(w6)
        w5.add_widget(w7)
        w2.add_widget(w3)
        w2.add_widget(w4)
        w2.add_widget(w5)
        w8.add_widget(w9)
        root.add_widget(w2)
        root.add_widget(w8)
        return root


TouchEventTestApp().run()
