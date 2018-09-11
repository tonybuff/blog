(function ($) {
    var options = {
        x: 1,
        y: 2,
        offsetH: 0,
        maxWidth: 0,
        minWidth: 0,
        className: 'demo1'
    };
    function standard(picW, picH) {
        var w = picW,
            h = picH,
            bodyH = $("body").height();
        if (options.maxWidth != "" && w > options.maxWidth) {
            h = Math.ceil(options.maxWidth / w * h);
            w = options.maxWidth;
            return standard(w, h);
        } else if (h > bodyH - options.offsetH) {
            w = Math.ceil(bodyH / h * w);
            h = bodyH - options.offsetH;
            return standard(w, h);
        } else if (options.minWidth != "") {
            h = Math.ceil(w / options.minWidth * h);
            w = options.minWidth;
            return standard(w, h);
        } else {
            return w + "|" + h;
        }
    };

    $.fn.AddMyFunction = function (opt) {
        options = $.extend(options, opt);
        var $self = this,
            $dialog = $("#js_picShowBox").size() > 0 ? $("#js_picShowBox") : $('<div id="js_picShowBox" class="' + options.className + '">' +
                '<div class="box f-cb">' +
                '<a href="javascript:;" class="pre"></a>' +
                '<div class="content"></div>' +
                '<a href="javascript:;" class="next"></a>' +
                '</div>' +
                '</div>'),
            $box = $dialog.find(".box").eq(0),
            $content = $dialog.find(".content").eq(0),
            $pre = $dialog.find(".pre").eq(0),
            $next = $dialog.find(".next").eq(0);
       
        $('body').append($dialog);
        $self.off('click').on('click', function () {
            $dialog.fadeIn();

        });
        $box.off('outerClick').on('outerClick', function () {
            $dialog.fadeOut('slow');
        });
       
       
        
        
       
    };
})(jQuery);