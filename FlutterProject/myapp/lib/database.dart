import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart';
import 'package:sembast/sembast.dart';
import 'package:sembast/sembast_io.dart';
import 'package:myapp/todo.dart';

class DbHelper {
  // find
  Future<List<ToDoRecord>> find() async {
    final result = await _store.find(
      _database,
      finder: Finder(
        sortOrders: [SortOrder(Field.key, false)],
      ),
    );

    return result
        .map(
          (e) => ToDoRecord(
            e.key,
            ToDo.fromJson(e.value),
          ),
        )
        .toList();
  }

  // add
  Future<ToDoRecord> add(ToDo todo) async {
    final key = await _store.add(_database, todo.toJson());
    return ToDoRecord(key, todo);
  }

  // update
  Future<void> update(int key, ToDo todo) async {
    _store.record(key).put(_database, todo.toJson());
  }

  // delete
  Future<void> delete(int key) async {
    _store.record(key).delete(_database);
  }

  // 通常のコンストラクタを追加
  DbHelper();

  // プライベートな名前付きコンストラクタ
  DbHelper._();

  // このクラスの同一インスタンスを返す
  @Deprecated('not user')
  static DbHelper get instance => _instance;

  // 初回の呼び出しでインスタンスを生成
  static final DbHelper _instance = DbHelper._();

  // プライベートなDatabaseインスタンス
  late final Database _database;

  // 実際にデータを保存するためのインスタンス
  late final StoreRef<int, Map<String, dynamic>> _store;

  Future<void> initialize() async {
    // データベースの保存先の取得
    final appDir = await getApplicationDocumentsDirectory();

    // データベースを開く
    _database = await databaseFactoryIo.openDatabase(
      join(appDir.path, 'todo.db'),
    );

    // データを保存する領域の確保
    _store = intMapStoreFactory.store('todo');
  }
}
