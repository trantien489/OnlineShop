$(document).ready(function () {
    table = $('#myGrid').DataTable({
        dom: 'l<"br">Bfrtip',

        "ajax": {
            "url": "/Admin/Category/GetCategories/",
        },
        "columns": [
            { "data": "Id" },
            { "data": "Name" },
            { "data": "Producers[, ]" }
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
        initComplete: function (data) {
            $("div.br").html('<br></br>');
            
        }
    });

    // Edit record
    $('#myGrid').on('click', 'i.fa-edit', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        id = data.Id;
        $('#Updatename').val(data.Name);
        data.Image != null ? $('#Updateimage').attr("src", "../Photos/Producer/" + data.Image) : "";
        document.getElementById('Updateimage2').value = "";
        $('#EditAlertModal').modal('show');
    });

    // Delete a record
    $('#myGrid').on('click', 'i.fa-trash-o', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        id = data.Id;
        $('#DeleteAlertModal').modal('show');


    });

});