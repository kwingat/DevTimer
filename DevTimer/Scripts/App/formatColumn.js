// See: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Introduction_to_Object-Oriented_JavaScript

var DEVTIMER = DEVTIMER || {};

DEVTIMER.formatColumn = (function () {
    'use strict';

    // [public methods]
    return {
        longDateTimeShortWeekday: function (value, row, index) {
            if (value === null)
                return null;

            return new Date(value).toString("MMM d, yyyy h:mm tt (ddd)");
        },
        dateTime: function (value, row, index) {
            if (value === null)
                return null;

            var date = new Date(value);

            return _.string.sprintf("<nobr>%s</nobr> <nobr>%s</nobr>", date.toLocaleDateString(), date.toLocaleTimeString());
        },
        date: function (value, row, index) {
            if (value === null)
                return null;

            return new Date(value).toLocaleDateString();
        },
        time: function (value, row, index) {
            if (value === null)
                return null;

            return new Date(value).toLocaleTimeString();
        },
        checkbox: function (value, row, index) {
            if (value === null)
                return null;

            var icon = value ? 'fa fa-check-square-o' : 'fa fa-square-o';

            return '<span class="' + icon + '"></span>';
        },
        editActions: function (value, row, index) {
            return [
				"<nobr>",
				"	<a class='action-edit btn btn-default btn-xs' href='javascript:void(0)' title='Edit'>Edit</a>",
				"</nobr>"
            ].join("\n\r");

        },
        editViewActions: function (value, row, index) {
            return [
				"<nobr>",
				"	<a class='action-edit btn btn-default btn-xs' href='javascript:void(0)' title='Edit'>Edit</a>",
				"	<a class='action-details btn btn-default btn-xs' href='javascript:void(0)' title='Details'>Details</a>",
				"</nobr>",
            ].join("\n\r");

        },
        editDeleteActions: function (value, row, index) {
            return [
				"<nobr>",
				"<a class='action-edit btn btn-default btn-xs' href='javascript:void(0)' title='Edit'>Edit</a>",
				"<a class='action-delete btn btn-default btn-xs' href='javascript:void(0)' title='Delete'>Delete</a>",
				"</nobr>"
            ].join("\n\r");
        },
        editRemoveActions: function (value, row, index) {
            return [
				"<nobr>",
				"<a class='action-edit btn btn-default btn-xs' href='javascript:void(0)' title='Edit'>Edit</a>",
				"<a class='action-remove btn btn-default btn-xs' href='javascript:void(0)' title='Remove'>Remove</a>",
				"</nobr>"
            ].join("\n\r");
        },
        editViewDeleteActions: function (value, row, index) {
            return [
				"<nobr>",
				"<a class='action-edit btn btn-default btn-xs' href='javascript:void(0)' title='Edit'>Edit</a>",
				"<a class='action-details btn btn-default btn-xs' href='javascript:void(0)' title='View'>Details</a>",
				"<a class='action-delete btn btn-default btn-xs' href='javascript:void(0)' title='Delete'>Delete</a>",
				"</nobr>"
            ].join("\n\r");
        }
    };
}());

DEVTIMER.columnEvents = (function () {
    'use strict';

    function executeAction(e, row, actionName) {
        if (_.isUndefined(e) || _.isUndefined(row) || _.string.isBlank(actionName))
            return;

        var $table = $(e.currentTarget).closest('table');
        var actionUrl = $table.data(_.string.sprintf('action-%s-url', actionName));

        if (actionUrl !== undefined) {
            // NOTE: This relies on "Named Arguments" in action url (for substitution purposes)
            // See http://www.diveintojavascript.com/projects/javascript-sprintf for more info
            window.location.href = _.string.sprintf(actionUrl, row);
        }
    }

    // [public methods]
    return {
        actions: function () {
            return {
                'click .action-edit': function (e, value, row, index) {
                    executeAction(e, row, 'edit');
                },
                'click .action-details': function (e, value, row, index) {
                    executeAction(e, row, 'details');
                },
                'click .action-delete': function (e, value, row, index) {
                    executeAction(e, row, 'delete');
                },
                'click .action-remove': function (e, value, row, index) {
                    executeAction(e, row, 'remove');
                }
            };
        }
    };
}());