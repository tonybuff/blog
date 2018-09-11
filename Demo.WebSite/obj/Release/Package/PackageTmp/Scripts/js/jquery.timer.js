﻿jQuery.fn.extend({ everyTime: function (c, d, b, e, a) { return this.each(function () { jQuery.timer.add(this, c, d, b, e, a) }) }, oneTime: function (b, c, a) { return this.each(function () { jQuery.timer.add(this, b, c, a, 1) }) }, stopTime: function (b, a) { return this.each(function () { jQuery.timer.remove(this, b, a) }) } }); jQuery.extend({ timer: { guid: 1, global: {}, regex: /^([0-9]+)\s*(.*s)?$/, powers: { ms: 1, cs: 10, ds: 100, s: 1000, das: 10000, hs: 100000, ks: 1000000 }, timeParse: function (d) { if (d == undefined || d == null) { return null } var c = this.regex.exec(jQuery.trim(d.toString())); if (c[2]) { var b = parseInt(c[1], 10); var a = this.powers[c[2]] || 1; return b * a } else { return d } }, add: function (c, f, g, d, h, a) { var b = 0; if (jQuery.isFunction(g)) { if (!h) { h = d } d = g; g = f } f = jQuery.timer.timeParse(f); if (typeof f != "number" || isNaN(f) || f <= 0) { return } if (h && h.constructor != Number) { a = !!h; h = 0 } h = h || 0; a = a || false; if (!c.$timers) { c.$timers = {} } if (!c.$timers[g]) { c.$timers[g] = {} } d.$timerID = d.$timerID || this.guid++; var e = function () { if (a && this.inProgress) { return } this.inProgress = true; if ((++b > h && h !== 0) || d.call(c, b) === false) { jQuery.timer.remove(c, g, d) } this.inProgress = false }; e.$timerID = d.$timerID; if (!c.$timers[g][d.$timerID]) { c.$timers[g][d.$timerID] = window.setInterval(e, f) } if (!this.global[g]) { this.global[g] = [] } this.global[g].push(c) }, remove: function (a, c, b) { var e = a.$timers, d; if (e) { if (!c) { for (c in e) { this.remove(a, c, b) } } else { if (e[c]) { if (b) { if (b.$timerID) { window.clearInterval(e[c][b.$timerID]); delete e[c][b.$timerID] } } else { for (var b in e[c]) { window.clearInterval(e[c][b]); delete e[c][b] } } for (d in e[c]) { break } if (!d) { d = null; delete e[c] } } } for (d in e) { break } if (!d) { a.$timers = null } } } } }); if ($.browser.msie) { jQuery(window).one("unload", function () { var b = jQuery.timer.global; for (var d in b) { var a = b[d], c = a.length; while (--c) { jQuery.timer.remove(a[c], d) } } }) };