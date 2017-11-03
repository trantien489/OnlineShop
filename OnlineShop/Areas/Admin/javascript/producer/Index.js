var table;
var id;
function Add() {
    if ($('#name').val() == '') {
        $('#AddModalMessage').html("Điền tên hãng sản xuất");
    } else {
        var formdata = new FormData();
        formdata.append('name', $('#name').val());
        var data = document.getElementById('image').files[0];
        if (data != null) {
            formdata.append('image', data);
        }
        $.ajax({
            type: "POST",
            url: '../admin/producer/add',
            data: formdata,
            contentType: false,
            processData: false,
            method: 'POST',
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

function Delete() {
    $.ajax({
       // type: "GET",
        url: '../admin/producer/delete/' + id,
        contentType: false,
        processData: false,
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
            console.log(data.data);
        }
    });
}

function Update() {
    if ($('#Updatename').val() == '') {
        $('#EditModalMessage').html("Điền tên hãng sản xuất");
    }
    else {
        var formdata = new FormData();
        formdata.append('id', id);
        formdata.append('name', $('#Updatename').val());
        var data = document.getElementById('Updateimage2');
        if (data != null) {
            formdata.append('image', data.files[0]);
        }
        $.ajax({
            type: "POST",
            url: '../admin/producer/update',
            data: formdata,
            contentType: false,
            processData: false,
            method: 'POST',
            success: function (data) {
                $('#EditAlertModal').modal('hide');
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
        //dom: 'l<"toolbar">frtip',
        dom: 'l<"br">Bfrtip',
        
        "ajax": {
            "url": "/Admin/Producer/GetProducers/",
            "dataSrc": ""
        },
        "columns": [

            { "data": "Id" },
            { "data": "Name" },
            {
                "data": "Image",
                "render": function (data, type, row) {
                    return data!=null ?'<img width="40" src="../Photos/Producer/'+data+'"/>': "Không có ảnh";
                },
                defaultContent: "No image",
            },
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
                    //$('#name').val("");
                    //document.getElementById('image').value = "";
                    $('#AddForm')[0].reset();
                    $('#AddModal').modal('show');
                }
            }
        ],
        initComplete: function () {
            //$("div.toolbar").html('<br><button class="btn btn-outline-info" type="button" style="margin-right:90%; margin-top:5px" id="any_button" data-toggle="modal" data-target="#AddModal">Thêm</button></br>');
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
        $('#Updatename').val(data.Name);
        data.Image != null ? $('#Updateimage').attr("src", "../Photos/Producer/" + data.Image) : "";
        document.getElementById('Updateimage2').value = "";
        $('#EditAlertModal').modal('show');
    });

    // Delete a record
    $('#myGrid').on('click', '.btn-danger', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        id = data.Id;
        $('#DeleteAlertModal').modal('show');


    });
   
});