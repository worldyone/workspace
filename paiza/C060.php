<?php
    list($N, $page_units, $target_page) = explode(" ", trim(fgets(STDIN)));

    $words = explode(" ", trim(fgets(STDIN)));
    sort($words);

    for($i = 0; $i < $page_units; $i++){
        if($i < $N){
            echo $words[($target_page-1) * $page_units + $i] . "\n";
        }
    }
?>
