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
        console.log(formdata.values());
        $.ajax({
            type: "POST",
            url: '../admin/producer/add',
            data: formdata,
            contentType: false,
            processData: false,
            method: 'POST',
            success: function (data) {
                $('#AddModal').modal('hide');
                $('#message').html(data.result);
                $('#AlertModal').modal('show');
                table.ajax.reload();
            }
        });
    }

}

function Delete(Id) {
    $.ajax({
        type: "POST",
        url: '../admin/producer/delete/' + id,
        contentType: false,
        processData: false,
        method: 'DELETE',
        success: function (data) {
            $('#message').html(data.result);
            $('#AlertModal').modal('show');
            table.ajax.reload();
        }
    });
}

$(document).ready(function () {
    table = $('#myGrid').DataTable({
        dom: 'l<"toolbar">frtip',
        "ajax": {
            "url": "/Admin/Producer/GetProducers/",
            "dataSrc": ""
        },
        "columns": [

            { "data": "Id" },
            { "data": "Name" },
            {
                //data: "Image",
                //render: function (file_id) {
                //    return file_id ?
                //        '<img src="' + editor.file('files', file_id).web_path + '"/>' :
                //        null;
                //},
                "data": "Image",
                "render": function(data, type, row) {
                    return '<img width="40" src="../Photos/Producer/'+data+'"/>';
                },
                defaultContent: "No image",
            },
            {
                data: null,
                className: "center",
                defaultContent: '<a href="" class="editor_edit">Sửa</a> / <a href="" class="editor_remove">Xóa</a>'
            },
        ],
        initComplete: function () {
            $("div.toolbar").html('<br><button class="btn btn-outline-info" type="button" style="margin-right:90%; margin-top:5px" id="any_button" data-toggle="modal" data-target="#AddModal">Thêm</button></br>');
        }
    });
   
    //$('a.editor_create').on('click', function (e) {
    //    e.preventDefault();

    //    editor.create({
    //        title: 'Create new record',
    //        buttons: 'Add'
    //    });
    //});

    // Edit record
    $('#myGrid').on('click', 'a.editor_edit', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        console.log(data.Id);
      
    });

    // Delete a record
    $('#myGrid').on('click', 'a.editor_remove', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        id = data.Id;
        $('#DeleteAlertModal').modal('show');


    });


    

    //var table = $('#myGrid').DataTable();
    //$('#myGrid tbody').on('click', 'tr', function () {
    //    var data = table.row(this).data().Id;
    //    console.log();
    //    alert(data);

    //});

   
});