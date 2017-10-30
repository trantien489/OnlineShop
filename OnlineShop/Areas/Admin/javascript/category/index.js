var table;
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
                    html +=  "<div style='margin-bottom:5px'>";
                    html +=       "<div class='icheckbox_flat-green checked' style='position: relative;'>";
                    html +=           " <input type='checkbox' class='flat' checked='' style='position: absolute; opacity: 0;'>";
                    html +=       "</div> " + value.Name;
                    html += "</div>";
                });
                $("#CheckboxProducer").html(html);
            }
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
                defaultContent: '<i class="fa fa-edit fa-2x" style="cursor:pointer"></i> &nbsp&nbsp<i class="fa fa-trash-o fa-2x" style="cursor:pointer"></i>'
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
    LoadCheckboxProducer();
});