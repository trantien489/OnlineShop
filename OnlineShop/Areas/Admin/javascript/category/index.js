var table;
var id;
function LoadCheckboxProducer(){
    $.ajax({
        type: "GET",
        url: '../Admin/Producer/GetProducers/',
        contentType: false,
        processData: false,
        method: 'GET',
        success: function (data) {
            if (data != null) {
                var html = "";
                $.each(data, function (key, value) {
                    html += "<div style='margin-bottom:5px'>";
                    html +=      "<input type='checkbox' id ='ProducerAddCheckbox' value = '"+value.Id+"'> " + value.Name;
                    html += "</div>";
                });
                $("#CheckboxProducer").append(html);
            }
        }
    });
}

function Add() {
    var i = 0;
    var arr = [];
    $('#ProducerAddCheckbox:checked').each(function () {
        arr[i++] = $(this).val();
    });
    var name = $('#name').val();
    $.ajax({
        type: "POST",
        url: '../admin/category/add/',
        data: { "name" :name, "producersId" : arr},
        success: function (data) {
            $('#AddModal').modal('hide');
            if (data.result == "Success") {
                $('#message').html(data.result);
                $('#alerticon').attr("src", "../Asset/Common/Photos/Success.png");
            }
            else {
                $('#message').html(data.error);
                $('#alerticon').attr("src", "../Asset/Common/Photos/Fail.png");
            }
            $('#AlertModal').modal('show');
            table.ajax.reload();
        }
    });
    
}
$(document).ready(function () {
    table = $('#myGrid').DataTable({
        dom: 'l<"br">Bfrtip',

        "ajax": {
            "url": "/Admin/Category/GetCategories/",
            "dataSrc": ""
        },
        "columns": [

            { "data": "Id" },
            { "data": "Name" },
            { "data": "Producers[, ].Name" },
            {
                data: null,
                className: "center",
                defaultContent: '<button class="btn btn-info btn-xs"><i class="fa fa-pencil"></i> Edit </button> <button class="btn btn-danger btn-xs"><i class="fa fa-trash-o"></i> Delete </button>'
            },
        ],
        buttons: [
            {
                text: 'Thêm <i class="fa fa fa-plus"></i>',
                action: function (e, dt, node, config) {
                    $('#AddForm')[0].reset();
                    $('#AddModal').modal('show');
                }
            }
        ],
        initComplete: function () {
            $("div.br").html('<br></br>');

        }
    });

    // Edit record
    $('#myGrid').on('click', '.btn-info', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        id = data.Id;
        $('#UpdateName').val(data.Name);
        Producers = data.Producers;
        $.ajax({
            type: "GET",
            url: '../Admin/Producer/GetProducers/',
            method: 'GET',
            success: function (data) {
                var check;
                if (data != null) {
                    var html = "";
                    $.each(data, function (key, value) {
                        $.each(Producers, function (i, value2) {
                            if (value2.Id == value.Id) {
                                check = true;
                                return false;
                            }
                        });
                        html += "<div style='margin-bottom:5px'>";
                        if (check == true) {
                            html += "<input type='checkbox'  id ='ProducerUpdateCheckbox' value = '" + value.Id + "' checked> " + value.Name;
                        } else {
                            html += "<input type='checkbox' id ='ProducerUpdateCheckbox' value = '" + value.Id + "'> " + value.Name;
                        }
                        html += "</div>";
                    });
                    $("#UpdateCheckboxProducer").html(html);
                }
            }
        });
        $('#UpdateModal').modal('show');
    });

    // Delete a record
    $('#myGrid').on('click', '.btn-danger', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        id = data.Id;
        $('#DeleteAlertModal').modal('show');


    });
    LoadCheckboxProducer();
});