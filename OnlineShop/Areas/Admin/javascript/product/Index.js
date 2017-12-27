var table;
var editor;
var currentData;
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
                    html += "<option value='" + value.Id + "'>" + value.Name + "</option>";
                });
                $("#SelectCategory").html(html);
            }
        }
    });

    // Load select producer
    $.ajax({
        type: "GET",
        url: '../Admin/producer/GetProducersbyCategory/5',
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

    CKEDITOR.config.allowedContent = true;
    CKEDITOR.replace('Detail');
    CKEDITOR.replace('Information');
    CKEDITOR.replace('UpdateDetail');
    CKEDITOR.replace('UpdateInformation');

}
function Add() {
    var name = $('#Name').val();
    var quantity = $('#Quantity').val();
    var price = $('#Price').val();
    var categoryId = $("#SelectCategory option:selected").val();
    var producerId = $("#SelectProducer option:selected").val();
    var image = $('#Image').val();
    var detail = CKEDITOR.instances.Detail.getData();
    var information = CKEDITOR.instances.Information.getData();

    if (name == '' || quantity == '' || price == '' || information == '' || image == '') {
        $('#AddModalMessage').html("Hãy điền đầy đủ thông tin");
    } else {
        var formData = new FormData();
        formData.append('ProductName', name);
        formData.append('Quantity', quantity);
        formData.append('Price', price);
        formData.append('Information', information);
        formData.append('CategoryId', categoryId);
        formData.append('ProducerId', producerId);
        formData.append('Detail', detail);


        var data = document.getElementById('Image').files[0];
        if (data != null) {
            formData.append('image', data);
        }
        $.ajax({
            type: "POST",
            url: '../admin/product/CheckProductExist',
            data: { "name": name },
            success: function (data) {
                if (data) {
                    $('#AddModalMessage').html("Sản phẩm đã tồn tại, không thể thêm mới");
                } else {
                    // Display the key/value pairs
                    //for (var pair of formData.entries())
                    //{
                    //    console.log(pair[0] + ', ' + pair[1]);
                    //}
                    $.ajax({
                        type: "POST",
                        url: '../admin/product/add',
                        data: formData,
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
        });

    }
}
function Update() {
    var name = $('#UpdateName').val();
    var quantity = $('#UpdateQuantity').val();
    var price = $('#UpdatePrice').val();
    var categoryId = $("#UpdateSelectCategory option:selected").val();
    var producerId = $("#UpdateSelectProducer option:selected").val();
    var image = $('#UpdateImage').val();
    var detail = CKEDITOR.instances.UpdateDetail.getData();
    var information = CKEDITOR.instances.UpdateInformation.getData();

    if (name == '' || quantity == '' || price == '' || information == '') {
        $('#UpdateModalMessage').html("Hãy điền đầy đủ thông tin");
    } else {
        var formData = new FormData();
        formData.append('ProductName', name);
        formData.append('Quantity', quantity);
        formData.append('Price', price);
        formData.append('Information', information);
        formData.append('CategoryId', categoryId);
        formData.append('ProducerId', producerId);
        formData.append('Detail', detail);
        formData.append('Id', currentData.Id);


        var data = document.getElementById('UpdateImage').files[0];
        if (data != null) {
            formData.append('image', data);
        }
        //Have update Product name
        if (currentData.Name != name) {
            $.ajax({
                type: "POST",
                url: '../admin/product/CheckProductExist',
                data: { "name": name },
                success: function (data) {
                    if (data) {
                        $('#UpdateModalMessage').html("Sản phẩm đã tồn tại, hãy chọn tên khác");
                    } else {
                        $.ajax({
                            type: "POST",
                            url: '../admin/product/update',
                            data: formData,
                            contentType: false,
                            processData: false,
                            success: function (data) {
                                $('#UpdateModal').modal('hide');
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
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: '../admin/product/update',
                data: formData,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#UpdateModal').modal('hide');
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
    $.ajax({
        url: '../admin/product/delete/' + currentData.Id,
        method: 'GET',
        success: function (data) {
            $('#DeleteAlertModal').modal('hide');
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

function LoadSelectProducer (categoryId,  htmlId)
{
    $.ajax({
        type: "GET",
        url: '../Admin/producer/GetProducersbyCategory/' + categoryId,
        contentType: false,
        processData: false,
        method: 'GET',
        success: function (data) {
            if (data != null) {
                var html = "";
                $.each(data, function (key, value) {
            
                    html += "<option value='" + value.Id + "'>" + value.Name + "</option>";
                });
                $("#" + htmlId).html(html);
            }
        }
    });
}

$(document).ready(function () {
    table = $('#myGrid').DataTable({
        dom: 'l<"br">Bfrtip',

        "ajax": {
            "url": "/Admin/product/GetProducts/",
            "dataSrc": ""
        },
        "order": [[0, "desc"]],
        "columns": [

            { "data": "Id" },
            { "data": "Name" },
            { "data": "Category" },
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
                    CKEDITOR.instances.Information.setData("");
                    CKEDITOR.instances.Detail.setData("");
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
        $('#UpdateForm')[0].reset();
        var data = table.row($(this).parents('tr')).data();
        currentData = data;
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
                        if (value.Id == currentData.CategoryId) {
                            html += "<option value='" + value.Id + "' selected>" + value.Name + "</option>";
                        } else {
                            html += "<option value='" + value.Id + "'>" + value.Name + "</option>";
                        }
                    });
                    $("#UpdateSelectCategory").html(html);
                }
            }
        });
        // Load select producer
        $.ajax({
            type: "GET",
            url: '../Admin/producer/GetProducersbyCategory/' + currentData.CategoryId,
            contentType: false,
            processData: false,
            method: 'GET',
            success: function (data) {
                if (data != null) {
                    var html = "";
                    $.each(data, function (key, value) {
                        if (value.Id == currentData.ProducerId) {
                            html += "<option value='" + value.Id + "' selected>" + value.Name + "</option>";
                        } else {
                            html += "<option value='" + value.Id + "'>" + value.Name + "</option>";
                        }
                    });
                    $("#UpdateSelectProducer").html(html);
                }
            }
        });

        $('#UpdateName').val(currentData.Name);
        $('#UpdateQuantity').val(currentData.Quantity);
        $('#UpdatePrice').val(currentData.PriceInt);
        CKEDITOR.instances.UpdateInformation.setData(currentData.Information);
        CKEDITOR.instances.UpdateDetail.setData(currentData.Detail);
        $('#UpdateimageView').removeAttr("src");
        currentData.Image != null ? $('#UpdateimageView').attr("src", "../Photos/Product/" + currentData.Image) : "";

        $('#UpdateModalMessage').html("");
        $('#UpdateModal').modal('show');
    });

    // Delete a record
    $('#myGrid').on('click', '.btn-danger', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        currentData = data;
        $('#DeleteAlertModal').modal('show');
    });

    // View a record
    $('#myGrid').on('click', '.btn-primary', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        currentData = data;
        $('#DetailProductImage').attr("src", "../Photos/Product/" + data.Image);
        $('#DetailName').html(data.Name);
        $('#DetailProducer').html(data.Producer);
        $('#DetailCategory').html(data.Category);
        $('#DetailPrice').html(data.Price);
        $('#DetailInformation').html(data.Information);
        $('#DetailDetail').html(data.Detail);

        $('#ViewDetaiModal').modal('show');
    });
    LoadModalAddProduct();
});