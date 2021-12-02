import 'package:flutter/material.dart';

class MaterialComponentsWidgetPage extends StatelessWidget {
  const MaterialComponentsWidgetPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Scaffoldの勉強',
            style: TextStyle(
              fontSize: 24,
            )),

        /// appBarの左側
        // leading: IconButton(
        //   icon: Icon(Icons.send_and_archive),
        //   onPressed: () {
        //     print("Here is leading.");
        //   },
        // ),

        /// appBarの右側
        actions: <Widget>[
          IconButton(
            icon: Icon(Icons.group),
            onPressed: () {
              print("Here is actions.");
            },
          ),
          PopupMenuButton(
            itemBuilder: (BuildContext context) => <PopupMenuItem<String>>[
              const PopupMenuItem<String>(
                value: 'Toolbar menu',
                child: Text('Toolbar menu'),
              ),
              const PopupMenuItem<String>(
                value: 'Right here',
                child: Text('Right here'),
              ),
            ],
          ),
        ],
        // bottom: const PreferredSize(
        //   preferredSize: Size.fromHeight(50),
        //   child: Padding(
        //     padding: EdgeInsets.all(8.0),
        //     child: Text(
        //       'Bottom',
        //       style: TextStyle(fontSize: 32),
        //     ),
        //   ),
        // ),
        bottom: const TabBar(tabs: [
          Tab(icon: Icon(Icons.directions_car)),
          Tab(icon: Icon(Icons.directions_transit)),
          Tab(icon: Icon(Icons.directions_bike)),
        ]),
        elevation: 20,
      ),
      body: ListView(
        children: [
          const Text(
            'test',
            style: TextStyle(
              fontSize: 24,
              backgroundColor: Colors.lightGreenAccent,
            ),
          ),
          InkWell(
            onTap: () {
              ScaffoldMessenger.of(context).showSnackBar(
                const SnackBar(
                  content: Text('Tap'),
                ),
              );
            },
            child: Container(
              padding: const EdgeInsets.all(12.0),
              child: const Text('Flat Button'),
            ),
          ),
          RaisedButton(
            child: const Text(
              'RAISED BUTTON',
              semanticsLabel: 'RAISED BUTTON 1',
            ),
            onPressed: () {
              print('Here is RaisedButton.');
            },
          ),
          ElevatedButton(
            child: const Text(
              'ELEVATE BUTTON',
              semanticsLabel: 'ELEVATE BUTTON 1',
            ),
            onPressed: () {
              print('Here is ElevateButton. You can "Long Press".');
            },
            onLongPress: () {
              print('You tapped the ElevateButton for a long time.');
            },
            clipBehavior: Clip.antiAlias,
          ),
          IconButton(
            onPressed: () {
              print('Here is IconButton.');
            },
            icon: const Icon(Icons.thumb_up),
          ),
          ButtonBar(children: [
            ElevatedButton(
              child: const Text(
                'Button 1',
              ),
              onPressed: () {},
            ),
            ElevatedButton(
              child: const Text(
                'Button 2',
              ),
              onPressed: () {},
            ),
          ]),
          Chip(
            avatar: const CircleAvatar(
              child: Text('AAAA'),
            ),
            backgroundColor: Colors.green.shade100,
            label: const Text('Aaaron Aurr'),
            onDeleted: () {},
          )
        ],
      ),
      floatingActionButton: FloatingActionButton(
        child: const Icon(Icons.add),
        onPressed: () => {print("floatingActionButton is pressed!")},
      ),
      drawer: Drawer(
        child: ListView(
          children: const <Widget>[
            DrawerHeader(
              child: Text('ヘッダー'),
              decoration: BoxDecoration(
                color: Colors.blue,
              ),
            ),
            ListTile(
              title: Text("ボタン1"),
              trailing: Icon(Icons.arrow_forward),
            ),
            ListTile(
              title: Text("ボタン2"),
              trailing: Icon(Icons.arrow_forward),
            ),
            ListTile(
              title: Text("ボタン3"),
              trailing: Icon(Icons.arrow_forward),
            ),
          ],
        ),
      ),
    );
  }
}
