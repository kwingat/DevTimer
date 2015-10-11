(function () {
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

// alias to underscore.string global 's' object
_.string = s;