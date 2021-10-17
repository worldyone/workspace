# -*- coding: utf-8 -*-
from kivy.app import App
from kivy.factory import Factory
from kivy.uix.boxlayout import BoxLayout
from kivy.uix.floatlayout import FloatLayout
from kivy.properties import ObjectProperty


class Board(BoxLayout):
    puzzle = ((16, 3, 10, 5, 9, 6, 15, 4, 7, 12, 1, 14, 2, 13, 8, 11),
              (6, 12, 7, 9, 16, 5, 10, 3, 1, 4, 15, 14, 11, 13, 2, 8),
              (16, 7, 2, 9, 14, 4, 11, 5, 3, 13, 6, 12, 1, 10, 15, 8))
    mask = ((0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0),
            (1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1),
            (0, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0))

    def __init__(self, no, **kwargs):
        super(Board, self).__init__(**kwargs)
        self.no = no
        self.W = []
        for k in range(16):
            if self.mask[no - 1][k] == 0:
                c = Factory.NumInput()
            else:
                c = Factory.Const(text=str(self.puzzle[no - 1][k]))
            self.W.append(c)
            self.ids['board'].add_widget(c)

    def check(self):
        view = Factory.CheckView()
        for k in range(16):
            if self.mask[self.no - 1][k] == 0 and \
                    self.puzzle[self.no - 1][k] != self.W[k].value:
                view.is_correct = False
                break
        view.open()


class Root(FloatLayout):
    board = ObjectProperty(None)

    def gotoTitle(self):
        self.clear_widgets()
        self.add_widget(Factory.Title())

    def gotoBoard(self, no):
        self.clear_widgets()
        self.board = Board(no)
        self.add_widget(self.board)


class Main(App):
    title = '魔方陣パズル'

    def build(self):
        self.root.gotoTitle()


Main().run()
