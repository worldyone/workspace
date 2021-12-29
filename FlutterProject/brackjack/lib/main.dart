import 'package:flutter/material.dart';

import 'card_page.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'black jack',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const CardPage(title: 'black jack'),
    );
  }
}
