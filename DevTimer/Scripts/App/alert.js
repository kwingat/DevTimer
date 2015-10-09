var DEVTIMER = DEVTIMER || {};

DEVTIMER.alerts = (function () {

    // [ private variables ]
    var alertContainer = $(".alert-container"),
		template = _.template(
			"<div class='alert <%=alertClass%> alert-dismissable'>" +
				"<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" +
				 "<%= message %>" +
			"</div>"
		);

    // [ private methods ]
    function render(alert) {
        var alertElement = $(template(alert));
        alertContainer.append(alertElement);

        window.setTimeout(function () { alertElement.fadeOut(); }, 3000);
    }

    function showSuccess(message) {
        render({ alertClass: "alert-success", message: message });
    }

    function showInfo(message) {
        render({ alertClass: "alert-info", message: message });
    }

    function showWarning(message) {
        render({ alertClass: "alert-warning", message: message });
    }

    function showError(message) {
        render({ alertClass: "alert-danger", message: message });
    }

    // [ public methods ]
    return {
        showSuccess: showSuccess,
        showInfo: showInfo,
        showWarning: showWarning,
        showError: showError,
        render: render
    }
}());