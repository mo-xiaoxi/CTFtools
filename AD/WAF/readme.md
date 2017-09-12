这个WAF在线下攻防赛中，绝对是一个大杀器。

不仅拦截了大多数非法语句，还能记录所有的攻击流量，轻松得到别人的payload。

不知道主办方要如何解决这个WAF所存在的问题。

当然，这个WAF应该也不是完美的，还可以添加更多的规则，让他变得更强！

接下来再说说，在实战中如何加载这个WAF。

根据权限不同，就有不同的加载方式。

有root权限

那麽，这样就简单了，直接写在配置中。

vim php.ini

auto_append_file = “/dir/path/phpwaf.php”

重启Apache或者php-fpm就能生效了。

当然也可以写在 .user.ini 或者 .htaccess 中。

php_value auto_prepend_file “/dir/path/phpwaf.php”

只有user权限

没写系统权限就只能在代码上面下手了，也就是文件包含。

这钟情况又可以用不同的方式包含。

如果是框架型应用，那麽就可以添加在入口文件，例如index.php，

如果不是框架应用，那麽可以在公共配置文件config.php等相关文件中包含。

include(‘phpwaf.php’);

还有一种是替换index.php，也就是讲index.php改名为index2.php，然后讲phpwaf.php改成index.php。

当然还没完，还要在原phpwaf.php中包含原来的index.php。

index.php -> index2.php

phpwaf.php -> index.php

include(‘index2.php’);

至于你想用哪种方式，看你心情咯，你开心就好。