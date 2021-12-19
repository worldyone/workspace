#!/bin/bash

. "./atcoder/Scripts/Activate"

problem_name=$1
test_dir=test/${problem_name}
base_url=${problem_name%_*}

# make test directory
if [ ! -e ${test_dir} ]; then
    oj dl -d test/${problem_name} https://atcoder.jp/contests/${base_url}/tasks/${problem_name//-/_}
fi

oj test -e 1e-6 -c "python problems/${problem_name}.py" -d test/${problem_name}
