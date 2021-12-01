import 'package:flutter/material.dart';

class ShowDatePickerTest extends StatefulWidget {
  const ShowDatePickerTest({Key? key}) : super(key: key);

  @override
  _ShowDatePickerState createState() => _ShowDatePickerState();
}

class _ShowDatePickerState extends State<ShowDatePickerTest> {
  String _showDateTimeValue = '';
  String _showTimeValue = '';

  Future _selectDateTime() async {
    DateTime? selected = await showDatePicker(
      context: context,
      initialDate: DateTime.now(),
      firstDate: DateTime(2018),
      lastDate: DateTime(2022),
      locale: Localizations.localeOf(context),
    );
    if (selected != null) {
      setState(() => _showDateTimeValue = selected.toString());
    }
  }

  Future _selectTimePicker() async {
    TimeOfDay? selected = await showTimePicker(
      context: context,
      initialTime: TimeOfDay.now(),
    );
    if (selected != null) {
      setState(() => _showDateTimeValue = selected.toString());
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('showDatePicker')),
      body: ListView(
        children: [
          Text(_showDateTimeValue),
          ElevatedButton(
            onPressed: _selectDateTime,
            child: const Text('開く'),
          ),
          Text(_showTimeValue),
          ElevatedButton(
            onPressed: _selectTimePicker,
            child: const Text('開く'),
          ),
        ],
      ),
    );
  }
}
