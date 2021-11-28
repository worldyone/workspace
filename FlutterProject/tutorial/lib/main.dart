import 'package:flutter/material.dart';
import 'package:tutorial/dropdownbutton.dart';
import 'package:tutorial/material_components_widget_page.dart';
import 'bottom_navigation.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const DefaultTabController(
        length: 3,
        // child: MaterialComponentsWidgetPage(),
        // child: BottomNavigationPage(title: 'bottom navigation'),
        child: DropdownButtonTest(),
      ),
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
    );
  }
}
