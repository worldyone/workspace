<?php
    list($N, $R) = explode(" ", trim(fgets(STDIN)));

    $max_term = 0;
    for($i = 0; $i < $N; $i++){
        $term = trim(fgets(STDIN));
        $terms[] = $term;

        if($term > $max_term){
            $max_term = $term;
        }
    }

    $length = $max_term / $R;

    for($i = 0; $i < $N; $i++){
        echo ($i+1) . ":";

        $term_length = $terms[$i] / $R;
        for($j = 0; $j < $term_length; $j++){
            echo "*";
        }
        for($j = 0; $j < $length - $term_length; $j++){
            echo ".";
        }
        echo "\n";
    }
?>
