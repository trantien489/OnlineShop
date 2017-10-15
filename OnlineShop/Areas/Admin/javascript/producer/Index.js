function Add() {
    alert();;
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
                data: "Image",
                render: function (file_id) {
                    return file_id ?
                        '<img src="' + editor.file('files', file_id).web_path + '"/>' :
                        null;
                },
                defaultContent: "No image",
            },
            {
                data: null,
                className: "center",
                defaultContent: '<a href="" class="editor_edit">Edit</a> / <a href="" class="editor_remove">Delete</a>'
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

    //// Edit record
    //$('#myGrid').on('click', 'a.editor_edit', function (e) {
    //    e.preventDefault();

    //    editor.edit($(this).closest('tr'), {
    //        title: 'Edit record',
    //        buttons: 'Update'
    //    });
    //});

    //// Delete a record
    //$('#myGrid').on('click', 'a.editor_remove', function (e) {
    //    e.preventDefault();

    //    editor.remove($(this).closest('tr'), {
    //        title: 'Delete record',
    //        message: 'Are you sure you wish to remove this record?',
    //        buttons: 'Delete'
    //    });
    //});

    

    //var table = $('#myGrid').DataTable();
    //$('#myGrid tbody').on('click', 'tr', function () {
    //    var data = table.row(this).data().Id;
    //    console.log();
    //    alert(data);

    //});

   
});