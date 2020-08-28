<?php
    $N = trim(fgets(STDIN));
    $cards = explode(" ", trim(fgets(STDIN)));

    $ans = 0;
    $max_num = 0;
    for($i = 0; $i < $N; $i++) {
        if($cards[$i] == 0){
            $zero_flg = true;
        }
        if("x10" === $cards[$i]){
            $x10_flg = true;
        }else{
            $ans += $cards[$i];
            if($cards[$i] > $max_num){
                $max_num = $cards[$i];
            }
        }
    }

    if($zero_flg){
        $ans -= $max_num;
    }
    if($x10_flg){
        $ans *= 10;
    }

    echo $ans;

?>
