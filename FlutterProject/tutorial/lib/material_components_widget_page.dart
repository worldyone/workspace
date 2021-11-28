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
      body: const Center(
        child: Text('test',
            style: TextStyle(
              fontSize: 24,
            )),
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
