from kivy.app import App
from kivy.uix.button import Button


class AddButton(Button):
    text = 'Add'

    def on_press(self):
        root = App.get_running_app().root
        s = root.ids['ti'].text
        root.ids['rv'].data.append({'key': root.key, 'text': s, 'group': 'view'})
        root.key += 1
        root.ids['ti'].text = ''


class RemoveButton(Button):
    text = 'Remove'

    def on_press(self):
        root = App.get_running_app().root
        V = [v for v in root.ids['box'].children if v.state == 'down']
        if V == []: return
        view = V[0]
        view.state = 'normal'
        D = [d for d in root.ids['rv'].data if d['key'] == view.key]
        root.ids['rv'].data.remove(D[0])


class recycleViewApp(App):
    title = 'RecycleView Test'


recycleViewApp().run()
