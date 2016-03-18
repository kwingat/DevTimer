$(function () {
    $('#Password').keyup(function (e) {
        if (e.keyCode == 13) {
            $(".login-form").submit();
        }
    });
})