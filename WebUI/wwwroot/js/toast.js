
$(document).ready(function () {

    notificationAlert();

});

function notificationAlert() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    var result = "";
    
    result = $('#success').val();
    if (result != 'undefined' && result != "" && result != null) {
        toastr.info(result);
        
    }
    result = $('#error').val();
    if (result != 'undefined' && result != "" && result != null) {
        toastr.error(result);   
    }

}
