// Event page scripts

// Disable submitting form on Enter key
$(document).on('keypress', 'form', function (e) {
    var code = e.keyCode || e.which;
    if (code == 13) {
        e.preventDefault();
        return false;
    }
});