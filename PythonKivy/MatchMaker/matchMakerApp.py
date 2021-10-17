# -*- coding: utf-8 -*-
from kivy.app import App
from kivy.graphics import Color, Line
from kivy.uix.widget import Widget
from kivy.properties import BooleanProperty


class Vertex(Widget):
    def on_center(self, *args):
        if self.parent is None:
            return
        for e in self.parent.E:
            if self in e.end:
                e.update()


class Edge(Widget):
    match = BooleanProperty(False)
    col_default = [0, 0, 1, 0.5]
    col_match = [1, 0, 0, 0.5]
    col = col_default
    end = []
    wid_default = 3
    wid_match = 6
    wid = wid_default

    def __init__(self, v, w, **kwargs):
        super(Edge, self).__init__(**kwargs)
        self.end = [v, w]
        self.update()

    def on_match(self, *args):
        if self.match:
            print(1)
            self.col = Edge.col_match
            self.wid = Edge.wid_match
        else:
            self.col = Edge.col_default
            self.wid = Edge.wid_default
        self.update()

    def update(self):
        self.canvas.clear()
        [v, w] = self.end
        self.x = min(v.x, w.x)
        self.y = min(v.y, w.y)
        self.width = max(v.x, w.x) - self.x
        self.height = max(v.y, w.y) - self.y
        with self.canvas:
            Color(rgba=self.col)
            Line(points=(v.center_x, v.center_y, w.center_x, w.center_y), width=self.wid)


class DrawField(Widget):
    V = []
    E = []
    focus = None
    free = None
    col_free = [0, 0, 1, 0.25]
    wid_free = 2

    def clear(self):
        self.clear_widgets()
        self.free = None
        self.V = []
        self.E = []

    def get_touched_widget(self, touch, array):
        for widget in array:
            if widget.collide_point(*touch.pos):
                return widget
        return None

    def get_touched_vertex(self, touch):
        return self.get_touched_widget(touch, self.V)

    def get_touched_edge(self, touch):
        return self.get_touched_widget(touch, self.E)

    def get_edge(self, v, w):
        for e in self.E:
            if v in e.end and w in e.end:
                return e
        return None

    def on_touch_down(self, touch):
        v = self.get_touched_vertex(touch)
        if self.parent.mode == 'vertex':
            if v is None:
                v = Vertex(center=touch.pos)
                self.V.insert(0, v)
                self.add_widget(v)
            self.focus = v
            self.parent.mode = 'move'
        elif self.parent.mode == 'edge':
            if v is None:
                return
            with self.canvas.after:
                Color(rgba=self.col_free)
                self.free = Line(points=(touch.x, touch.y), width=self.wid_free)
            self.focus = v
            self.parent.mode = 'free'
        elif self.parent.mode == 'erase':
            if v is not None:
                self.V.remove(v)
                self.remove_widget(v)
                delE = [e for e in self.E if v in e.end]
                for e in delE:
                    self.E.remove(e)
                    self.remove_widget(e)
                    return
                e = self.get_touched_edge(touch)
                if e is not None:
                    self.E.remove(e)
                    self.remove_widget(e)

    def on_touch_move(self, touch):
        if self.parent.mode == 'move':
            self.focus.center = touch.pos
        elif self.parent.mode == 'free':
            self.free.points += [touch.x, touch.y]

    def on_touch_up(self, touch):
        if self.parent.mode == 'move':
            self.focus = None
            self.parent.mode = 'vertex'
        elif self.parent.mode == 'free':
            self.canvas.after.clear()
            v = self.focus
            w = self.get_touched_vertex(touch)
            self.focus = None
            if v is None or w is None or v == w or self.get_edge(v, w) is not None:
                return
            e = Edge(v, w)
            self.E.insert(0, e)
            self.add_widget(e)
            self.parent.mode = 'edge'

    def match(self):
        import networkx as nx
        G = nx.Graph()
        G.add_nodes_from(self.V)
        for e in self.E:
            e.match = False
            [v, w] = e.end
            G.add_edge(v, w)
        M = nx.max_weight_matching(G, maxcardinality=True)
        print(G)
        print(M)
        for (v, w) in M:
            e = self.get_edge(v, w)
            print("\nv:", v, "\nw:", w, "\ne:", e)
            if e is not None:
                e.match = True


class MatchMakerApp(App):
    title = 'マッチメイカー'


MatchMakerApp().run()
