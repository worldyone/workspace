<?php
    list($H, $W, $X) = explode(" ", trim(fgets(STDIN)));

    $target_strings = [];
    for($i = 0; $i < $H; $i++) {
        $target_strings[] = trim(fgets(STDIN));
    }

    $target_string = implode($target_strings);

    for($i = 0; $i < round($H*$W/$X)+1; $i++) {
        $out = mb_substr($target_string, $i*$X, $X);
        echo $out . "\n";
    }

?>
