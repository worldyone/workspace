from kivy.app import App
from kivy.clock import Clock
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.label import Label
from kivy.uix.button import Button
from kivy.properties import NumericProperty


class MyLabel(Label):

    font_size = 48

    cnt = NumericProperty(0)

    def on_cnt(self, *args):
        self.text = str(self.cnt % 10)


class MyButton(Button):

    font_size = 32

    evt1 = None
    evt2 = None
    evt3 = None

    def on_press(self):
        if self.text == 'start':
            self.evt1 = Clock.schedule_interval(self.cb1, 0.2)
            self.evt2 = Clock.schedule_interval(self.cb2, 0.2)
            self.evt3 = Clock.schedule_interval(self.cb3, 0.2)
            self.text = 'stop'
        else:
            if self.evt1:
                print("ok1")
                self.evt1.cancel()
                self.evt1 = None
            elif self.evt2:
                print("ok2")
                self.evt2.cancel()
                self.evt2 = None
            elif self.evt3:
                print("ok3")
                self.evt3.cancel()
                self.evt3 = None
                self.text = 'start'
            else:
                assert False, "未到達エラー"

    def cb1(self, dt):
        self.parent.parent.bts.lbl1.cnt = round(self.parent.parent.bts.lbl1.cnt + 1, 1)

    def cb2(self, dt):
        self.parent.parent.bts.lbl2.cnt = round(self.parent.parent.bts.lbl2.cnt + 1, 1)

    def cb3(self, dt):
        self.parent.parent.bts.lbl3.cnt = round(self.parent.parent.bts.lbl3.cnt + 1, 1)


class SlotMachineApp(App):
    def build(self):
        layoutRoot = BoxLayout(orientation='vertical')
        layoutRoot.bts = BoxLayout(orientation='horizontal')
        layoutRoot.bts.lbl1 = MyLabel(text='0')
        layoutRoot.bts.lbl2 = MyLabel(text='0')
        layoutRoot.bts.lbl3 = MyLabel(text='0')
        layoutRoot.layoutStop = BoxLayout(orientation='vertical')
        layoutRoot.layoutStop.btn = MyButton(text='start')
        layoutRoot.bts.add_widget(layoutRoot.bts.lbl1)
        layoutRoot.bts.add_widget(layoutRoot.bts.lbl2)
        layoutRoot.bts.add_widget(layoutRoot.bts.lbl3)
        layoutRoot.layoutStop.add_widget(layoutRoot.layoutStop.btn)
        layoutRoot.add_widget(layoutRoot.bts)
        layoutRoot.add_widget(layoutRoot.layoutStop)
        return layoutRoot


SlotMachineApp().run()
