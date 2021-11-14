import 'package:freezed_annotation/freezed_annotation.dart';

part 'todo.freezed.dart';
part 'todo.g.dart';

@freezed
class ToDo with _$ToDo {
  factory ToDo({
    required String title,
    @Default(false) bool archived,
  }) = _ToDo;

  factory ToDo.fromJson(Map<String, dynamic> json) => _$ToDoFromJson(json);
}

@freezed
class ToDoRecord with _$ToDoRecord {
  factory ToDoRecord(
    int key,
    ToDo value,
  ) = _ToDoRecord;
}
