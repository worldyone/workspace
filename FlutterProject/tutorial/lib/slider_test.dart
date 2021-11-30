import 'package:flutter/material.dart';

class SliderTest extends StatefulWidget {
  const SliderTest({Key? key}) : super(key: key);

  @override
  _SliderTestState createState() => _SliderTestState();
}

class _SliderTestState extends State<SliderTest> {
  double _discreteValue = 40.0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: ListView(
        children: [
          Slider.adaptive(
            value: _discreteValue,
            min: 0.0,
            max: 200.0,
            divisions: 20,
            label: '${_discreteValue.round()}',
            onChanged: (double value) {
              setState(() {
                _discreteValue = value;
              });
            },
          ),
        ],
      ),
    );
  }
}
