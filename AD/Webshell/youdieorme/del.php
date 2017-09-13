<?php

    set_time_limit(0);

    ignore_user_abort(1);

    array_map('unlink', glob("some/dir/*.php"));

?>