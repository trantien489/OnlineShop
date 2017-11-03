
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

    LoadCheckboxProducer();
});