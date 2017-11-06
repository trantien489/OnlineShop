var table;
var editor;
function LoadModalAddProduct() {
    // Load select category
    $.ajax({
        type: "GET",
        url: '../Admin/category/GetCategories/',
        contentType: false,
        processData: false,
        method: 'GET',
        success: function (data) {
            if (data != null) {
                var html = "";
                $.each(data, function (key, value) {
                    html += "<option value='"+ value.Id +"'>" + value.Name + "</option>";
                });
                $("#SelectCategory").html(html);
            }
        }
    });

    // Load select producer
    $.ajax({
        type: "GET",
        url: '../Admin/producer/GetProducers/',
        contentType: false,
        processData: false,
        method: 'GET',
        success: function (data) {
            if (data != null) {
                var html = "";
                $.each(data, function (key, value) {
                    html += "<option value='" + value.Id + "'>" + value.Name + "</option>";
                });
                $("#SelectProducer").html(html);
            }
        }
    });


    // load Ckeditor
    //editor = ClassicEditor
    // .create(document.querySelector('#Deatail'))
    // .catch(error => {
    //     console.error(error);
    // });
}
function Add() {
    var name = $('#Name').val();
    var quantity = $('#Quantity').val();
    var price = $('#Price').val();
    var information = $('#Information').val();
    var categoryId = $("#SelectCategory option:selected").val();
    var producerId = $("#SelectProducer option:selected").val();
    

    if (name == '' || quantity == '' || price == '' || information == '' ) {
        $('#AddModalMessage').html("Hãy điền đầy đủ thông tin");
    } else {
        var formdata = new FormData();
        formdata.append('ProductName', name);
        formdata.append('Quantity', quantity);
        formdata.append('Price', price);
        formdata.append('Information', information);
        formdata.append('CategoryId', categoryId);
        formdata.append('ProducerId', producerId);

        var data = document.getElementById('Image').files[0];
        if (data != null) {
            formdata.append('image', data);
        }
        $.ajax({
            type: "POST",
            url: '../admin/product/add',
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
$(document).ready(function () {
    table = $('#myGrid').DataTable({
        dom: 'l<"br">Bfrtip',

        "ajax": {
            "url": "/Admin/product/GetProducts/",
            "dataSrc": ""
        },
        "columns": [

            { "data": "Id" },
            { "data": "Name" },
            { "data": "Producer" },
            { "data": "Quantity" },
            { "data": "Price" },
            {
                data: null, 
                className: "center",
                defaultContent: '<button class="btn btn-primary btn-xs"><i class="fa fa-folder"></i> Chi tiết </button> <button class="btn btn-info btn-xs"><i class="fa fa-pencil"></i> Sửa </button> <button class="btn btn-danger btn-xs"><i class="fa fa-trash-o"></i> Xóa </button>'
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

    LoadModalAddProduct();
    // editor.setData('<p>Some text.</p>');

});