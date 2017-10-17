function Add() {
    //$("#AddForm").ajaxSubmit({ url: '~/admin/producer/add', type: 'post' })
    var formdata = new FormData();
    formdata.append('name', $('#name').val());
    formdata.append('image', document.getElementById('image').files[0]);
    //formdata.append('name', $('input[type="text"]').value);
    //formdata.append('image', $('input[type="file"]').files);

    console.log(formdata.values());
    $.ajax({
        type: "POST",
        url: '../admin/producer/add',
        data: formdata,
        contentType: false,
        processData: false,
        method: 'POST',
        type: 'POST',
        success: function (data) {
            console.log(data);
        }
    });

}
$(document).ready(function () {

    $('#myGrid').DataTable({
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
            $("div.toolbar").html('<br><button class="btn btn-outline-info" type="button" style="margin-right:90%; margin-top:5px" id="any_button" data-toggle="modal" data-target="#myModal">Thêm</button></br>');
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

      
    });

    // Delete a record
    $('#myGrid').on('click', 'a.editor_remove', function (e) {
        e.preventDefault();

       
    });

    

    //var table = $('#myGrid').DataTable();
    //$('#myGrid tbody').on('click', 'tr', function () {
    //    var data = table.row(this).data().Id;
    //    console.log();
    //    alert(data);

    //});

   
});