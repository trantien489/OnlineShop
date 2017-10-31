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
                $("#CheckboxProducer").html(html);
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
    if (name == '' || arr.length == 0) {
        $('#AddModalMessage').html("Hãy nhập đầy đủ thông tin");
    } else {
        $.ajax({
            type: "POST",
            url: '../admin/category/add/',
            data: { "name": name, "producersId": arr },
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
}

function Update() {
    var name = $('#UpdateName').val();
    var i = 0;
    var arr = [];
    $('#ProducerUpdateCheckbox:checked').each(function () {
        arr[i++] = $(this).val();
    });
    if (name == '' || arr.length == 0) {
        $('#UpdateModalMessage').html("Hãy nhập đầy đủ thông tin");
    } else {
        $('#UpdateModal').modal('hide');
        $.ajax({
            type: "POST",
            url: '../admin/category/update/',
            data: {"id":id, "name": name, "producersId": arr },
            success: function (data) {
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
}
function Delete() {
    $('#DeleteAlertModal').modal('hide');
    $.ajax({
        type: "GET",
        url: '../admin/category/delete/',
        data: { "id": id},
        success: function (data) {
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
                    $('#AddModalMessage').html("");
                    $('#AddModal').modal('show');
                }
            }
        ],
        initComplete: function () {
            $("div.br").html('<br></br>');

        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

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
                
                if (data != null) {
                    var html = "";
                    $.each(data, function (key, value) {
                        var check;
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
        $('#UpdateModalMessage').html("");
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