1. VS Code上でCtrl + Shift + Bでサンプルテストケースでのテストを自動実行
2. VS Code上でF5で手入力値でのテスト実行

ファイル名はproblems/ABC123_a.pyというように作成する

AtCoder
├── cptest.sh #今回メインとなるシェルスクリプト
│
├── input.txt #ゴール2の手入力の値を入れるファイル
│
├── problems #コンテストの問題を格納するディレクトリ
│
├── test #コンテストのサンプルケースを格納するディレクトリ
│
└── .vscode #VS Codeの設定ファイルを格納するディレクトリ
    ├── launch.json #ゴール2の設定ファイル
    └── tasks.json #ゴール1の設定ファイル
