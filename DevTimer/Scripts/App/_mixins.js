(function () {
    setTimezoneCookie();

    _.mixin({
        sum: function (value, key) {
            var returnValue = 0;

            returnValue = _.reduce(value,
				function (memo, value) {
				    value = _.isNumber(value) ? value : value[key];
				    return memo + value;
				}, 0)

            return returnValue;
        },
    })
}());

function setTimezoneCookie() {
    var timezone_cookie = "timezoneoffset";

    // if the timezone cookie does not exist, create one.
    if (!$.cookie(timezone_cookie)) {

        // check if the browser supports cookies
        var test_cookie = 'test cookie';
        $.cookie(test_cookie, true);

        // browser supports cookie
        if ($.cookie(test_cookie)) {
            // delete the test cookie
            $.cookie(test_cookie, null);

            // create a new cookie
            $.cookie(timezone_cookie, new Date().getTimezoneOffset());

            // reload the page
            location.reload();
        }
    }
    // if the current timezone and the one stored in the cookie are different
    // then store the new timezone in the cookie and refresh the page.
    else {
        var storedOffset = parseInt($.cookie(timezone_cookie));
        var currentOffset = new Date().getTimezoneOffset();

        // user may have changed the timezone
        if (storedOffset !== currentOffset) {
            $.cookie(timezone_cookie, new Date().getTimezoneOffset());
            location.reload();
        }
    }
}

// alias to underscore.string global 's' object
_.string = s;