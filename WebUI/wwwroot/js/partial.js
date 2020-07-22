$(document).ready(function () {
    
    $('.partial-item').each(function (index, item) {
        var url = getBaseUrl() + $(item).data('url');
        if (url && url.length > 0) {
            $(item).load(url,
                function (response) {
                    $(item).replaceWith(response);

                });
        }
    });
});

function getBaseUrl() {
    var getUrl = window.location;
    return getUrl.protocol + "//" + getUrl.host + "/";
}

function FillBasicModal(item) {
    $('#loading').css('display', 'block');

    $.get($(item).data('fill-url'), function (data) {
        $('#basicmodalcontent').children('div').remove();
        $('#basicmodalcontent').append(data);
    })
        .done(function () {
            $("#basicmodal").modal("show");
        })
        .fail(function () {
            swal.fire({
                "title": "Hata",
                "text": "İşlem Sırasında Hata Oluştu Lütfen Tekrar Deneyin.",
                "type": "error",
                "showCloseButton": true,
                "confirmButtonText":
                    'Kapat',
                "confirmButtonAriaLabel": 'Kapat'
            });
        })
        .always(function () {
            $('#loading').css('display', 'none');
        });


}

function getQueryString(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
