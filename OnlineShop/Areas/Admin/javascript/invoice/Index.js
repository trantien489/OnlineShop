var table;
var editor;
var currentData;

function Update() {
    statusId = $('input[name=InvoiceStatusRadio]:checked').val();
    $.ajax({
        type: "POST",
        url: '/admin/invoice/UpdateStatusInvoice',
        data: { "invoiceId": currentData.Id, "invoiceStatus": statusId },
        success: function (data) {
            if (data.result == "Success") {
                $('#message').html(data.result);
                $('#alerticon').attr("src", "../Asset/Common/Photos/Success.png");
            }
            else {
                $('#message').html(data.error);
                $('#alerticon').attr("src", "../Asset/Common/Photos/Fail.png");
            }

            $('#ViewDetaiModal').modal('hide');
            $('#AlertModal').modal('show');
            table.ajax.reload();
        }
    });
}
function Delete() {
    $.ajax({
        url: '/admin/invoice/delete/',
        type: 'POST',
        data: { "invoiceId": currentData.Id },
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
$(document).ready(function () {
    table = $('#myGrid').DataTable({
        dom: 'l<"br">Bfrtip',
        "order": [[3, "desc"]],
        "ajax": {
            "url": "/Admin/invoice/GetInvoices/",
            "dataSrc": ""
        },
        "columns": [

            { "data": "Id" },
            { "data": "Id" },
            { "data": "Total" },
            { "data": "CreateDate" },
            { "data": "ReceiveData" },
            { "data": "InvoiceStatus" },
            {
                data: null,
                className: "center",
                defaultContent: '<button class="btn btn-primary btn-xs"><i class="fa fa-folder"></i> Chi tiết </button> <button class="btn btn-danger btn-xs"><i class="fa fa-trash-o"></i> Xóa </button>'
            },
        ],
        buttons: [

        ],
        initComplete: function () {
        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();

    // View detail record
    $('#myGrid').on('click', '.btn-primary', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        currentData = data;

        $('#CustomerId').html(data.CustomerId);
        $('#CustomerName').html(data.CustomerName);
        $('#InvoiceTotal').html(data.Total);
        $('#ReceiveName').html(data.NameReceive);
        $('#ReceivePhone').html(data.PhoneReceive);
        $('#ReceiveAddress').html(data.AddressReceive);
        $('#ReceiveEmail').html(data.EmailReceive);
        $('#InvoiceId').html(data.Id);



        var invoiceStatus = "";
        invoiceStatus += "<label class='radio-inline'>";
        invoiceStatus += "<input type='radio' value='-1' name='InvoiceStatusRadio' " + (data.InvoiceStatusInt == -1 ? "checked" : "") + ">Bị hủy";
        invoiceStatus += "</label>";
        invoiceStatus += "<label class='radio-inline'>";
        invoiceStatus += "<input type='radio' value='0' name='InvoiceStatusRadio' " + (data.InvoiceStatusInt == 0 ? "checked" : "") + ">Đang xử lý";
        invoiceStatus += "</label>";
        invoiceStatus += "<label class='radio-inline'>";
        invoiceStatus += "<input type='radio' value='1' name='InvoiceStatusRadio' " + (data.InvoiceStatusInt == 1 ? "checked" : "") + ">Đang vận chuyển";
        invoiceStatus += "</label>";
        invoiceStatus += "<label class='radio-inline'>";
        invoiceStatus += "<input type='radio' value='2' name='InvoiceStatusRadio' " + (data.InvoiceStatusInt == 2 ? "checked" : "") + ">Đã giao hàng";
        invoiceStatus += "</label>";
        $('#InvoiceStatus').html(invoiceStatus);


        $.ajax({
            type: "GET",
            url: '/Admin/invoice/GetInvoiceDetailbyId/',
            data: { invoiceId: currentData.Id },
            success: function (data) {
                if (data != null) {
                    var html = "";
                    var index = 1;
                    $.each(data, function (key, value) {
                        html += "<tr>";
                        html += "<td>" + index + "</td>";
                        html += "<td>" + value.ProductName + "</td>";
                        html += "<td>" + value.Price + "</td>";
                        html += "<td>" + value.Quantity + "</td>";
                        html += "<td>" + value.Money + "</td>";
                        html += "</tr>";

                        index++;

                    });
                    $("#InvoiceDetail").html(html);
                }
            }
        });
        $('#ViewDetaiModal').modal('show');
    });

    // Delete a record
    $('#myGrid').on('click', '.btn-danger', function (e) {
        e.preventDefault();
        var data = table.row($(this).parents('tr')).data();
        currentData = data;
        $('#DeleteAlertModal').modal('show');
    });

});