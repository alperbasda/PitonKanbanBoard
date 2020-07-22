"use strict";

// Class definition
var kanban2;
var KTKanbanBoardDemo = function () {

    // Private functions

    // Basic demo
    var demos = function () {

        kanban2 = new jKanban({
            element: '#kanban2',
            gutter: '0',
            click: function (el) {
                swal.fire({
                    "title": "Silme Islemi",
                    "text": "Veri Takvimden Silinsin Mi ?",
                    "type": "success",
                    "showCloseButton": true,
                    "showCancelButton": true,
                    "focusConfirm": false,
                    "confirmButtonText":
                        '<i class="fa fa-thumbs-up"></i> Sil !',
                    "confirmButtonAriaLabel": 'Sil',
                    "cancelButtonText":
                        '<i class="fa fa-thumbs-down"></i> Iptal',
                    "cancelButtonAriaLabel": 'Iptal Et'

                }).then((result) => {
                    if (result.value) {

                        $.ajax({
                            dataType: 'JSON',
                            type: 'Get',
                            url: '../../Home/DeleteEvent',
                            data: { id: $(el).data('eid') },
                            contentType: 'application/json;',
                            beforeSend: function () {
                                $('#loading').show();
                            },
                            success: function (data) {
                                ShowToast(data);
                                $('#loading').hide();
                                el.remove();
                            }
                        });

                    }
                });
            },
            dragendEl: function (el) {
                var boardId = $(el).parent('main').parent('div').data('id');

                $(el).data('r-type', boardId);
                $.ajax({
                    dataType: 'JSON',
                    type: 'Get',
                    url: '../../Home/UpdateEvent',
                    data: { id: $(el).data('eid'), RecordType: boardId },
                    contentType: 'application/json;',
                    beforeSend: function () {
                        $('#loading').show();
                    },
                    success: function (data) {
                        $('#loading').hide();
                        ShowToast(data);

                    }
                });

            },
            boards: [
                {
                    'id': '0',
                    'title': 'Bu Gun',
                    'class': 'success',
                    'dragTo': ['1', '2'],
                    'item': [

                    ]
                },
                {
                    'id': '1',
                    'title': 'Bu Hafta',
                    'class': 'success',
                    'dragTo': ['0', '2'],
                    'item': [

                    ]
                },
                {
                    'id': '2',
                    'title': 'Bu Ay',
                    'class': 'success',
                    'dragTo': ['0', '1'],
                    'item': [

                    ]
                },

            ]
        });

        var addTask = document.getElementById('addTask');
        addTask.addEventListener('click', function () {
            var target = $('#kanban-select-task').val();
            var title = $('#kanban-add-task').val();
            var taskColor = "info";

            $.ajax({
                dataType: 'JSON',
                type: 'Get',
                url: '../../Home/InsertEvent',
                data: { Description: title, RecordType: target },
                contentType: 'application/json;',
                beforeSend: function () {
                    $('#loading').show();
                },
                success: function (data) {
                    $('#loading').hide();
                    ShowToast(data);
                    if (data.Success) {
                        kanban2.addElement(
                            target,
                            {
                                'title': data.Data.Description,
                                'class': 'info',
                                'id': data.Data.Id,
                                'r-type': data.Data.RecordTypeInt
                            }
                        );
                        $('#kanban-add-task').val('');
                    }
                }
            });


        });

    }

    return {
        // public functions
        init: function () {
            demos();
        }
    };
}();

jQuery(document).ready(function () {
    KTKanbanBoardDemo.init();
    $.ajax({
        dataType: 'JSON',
        type: 'Get',
        url: '../../Home/Events',
        contentType: 'application/json;',
        beforeSend: function () {
            $('#loading').show();
        },
        success: function (data) {
            $('#loading').hide();

            ShowToast(data);
            if (data.Success) {
                FillData(data.Data);
            }

        }

    });

});

function FillData(data) {
    
    $.each(data, function (index, item) {
        debugger;
        kanban2.addElement(
            item.RecordType,
            {
                'title': item.Description,
                'class': 'info',
                'id': item.Id,
                'r-type': item.RecordTypeInt
            }
        );
    });
}

function ShowToast(data) {
    if (data.Success) {
        toastr.info(data.Message);
    } else {
        toastr.error(data.Message);
    }
}