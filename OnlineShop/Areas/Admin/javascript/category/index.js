var table;
var id;
var currentData;
function LoadCheckboxProducer() {
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
                    html += "<input type='checkbox' id ='ProducerAddCheckbox' value = '" + value.Id + "'> " + value.Name;
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

    var formdata = new FormData();
    formdata.append('name', name);
    for (var i = 0; i < arr.length; i++) {
        formdata.append('producersId[]', arr[i]);
    }
    var image = document.getElementById('image').files[0];
    if (image != null) {
        formdata.append('image', image);
    }

    if (name == '' || arr.length == 0 || $('#image').val() == '') {
        $('#AddModalMessage').html("Hãy nhập đầy đủ thông tin");
    } else {
        $.ajax({
            type: "POST",
            url: '../admin/category/CheckCategoryExist',
            data: { "name": name },
            success: function (data) {
                if (data) {
                    $('#AddModalMessage').html("Danh mục đã tồn tại, không thể thêm mới");
                } else {
                    $.ajax({
                        type: "POST",
                        url: '../admin/category/add/',
                        data: formdata,
                        contentType: false,
                        processData: false,
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
        })

    }
}

function Update() {
    var name = $('#UpdateName').val();
    var i = 0;
    var arr = [];
    $('#ProducerUpdateCheckbox:checked').each(function () {
        arr[i++] = $(this).val();
    });

    var formdata = new FormData();
    for (var i = 0; i < arr.length; i++) {
        formdata.append('producersId[]', arr[i]);
    }
    var image = document.getElementById('Updateimage').files[0];
    if (image != null) {
        formdata.append('image', image);
    }
    formdata.append('name', name);
    formdata.append('id', id);


    if (name == '' || arr.length == 0) {
        $('#UpdateModalMessage').html("Hãy nhập đầy đủ thông tin");
    } else {
        if (currentData.Name != name) {
            $.ajax({
                type: "POST",
                url: '../admin/category/CheckCategoryExist',
                data: { "name": name },
                success: function (data) {
                    if (data) {
                        $('#UpdateModalMessage').html("Danh mục đã tồn tại, hãy chọn tên khác");
                    } else {
                        $('#UpdateModal').modal('hide');
                        $.ajax({
                            type: "POST",
                            url: '../admin/category/update/',
                            data: formdata,
                            contentType: false,
                            processData: false,
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
            })
        } else {
            $('#UpdateModal').modal('hide');
            $.ajax({
                type: "POST",
                url: '../admin/category/update/',
                data: formdata,
                contentType: false,
                processData: false,
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
}
function Delete() {
    $('#DeleteAlertModal').modal('hide');
    $.ajax({
        type: "GET",
        url: '../admin/category/delete/',
        data: { "id": id },
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
                defaultContent: '<button class="btn btn-info btn-xs"><i class="fa fa-pencil"></i> Sửa </button> <button class="btn btn-danger btn-xs"><i class="fa fa-trash-o"></i> Xóa </button>'
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
        currentData = data;
        id = data.Id;
        $('#UpdateForm')[0].reset();
        $('#UpdateName').val(data.Name);
        $('#UpdateImageView').removeAttr("src");
        data.Image != null ? $('#UpdateImageView').attr("src", "../Photos/Category/" + data.Image) : "";

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