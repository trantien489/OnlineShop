var table;
var editor;
var currentData;

function Update() {
    status = $('input[name=statusRadio]:checked').val();
    $.ajax({
        type: "POST",
        url: '/admin/user/update',
        data: { "customerId": currentData.CustomerId, "status": status },
        success: function (data) {
            $('#ViewDetaiModal').modal('hide');
            if (data.result == "Success") {
                $('#message').html(data.result);
                $('#alerticon').attr("src", "/Asset/Common/Photos/Success.png");
            }
            else {
                $('#message').html(data.error);
                $('#alerticon').attr("src", "/Asset/Common/Photos/Fail.png");
            }

            $('#AlertModal').modal('show');
            table.ajax.reload();
        }
    });
}
function Delete() {
    $.ajax({
        url: '/admin/user/delete/',
        type: 'POST',
        data: { "customerId": currentData.CustomerId },
        success: function (data) {
            $('#DeleteAlertModal').modal('hide');
            if (data.result == "Success") {
                $('#message').html(data.result);
                $('#alerticon').attr("src", "/Asset/Common/Photos/Success.png");
            }
            else {
                $('#message').html(data.error);
                $('#alerticon').attr("src", "/Asset/Common/Photos/Fail.png");
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
            "url": "/Admin/user/GetUsers/",
            "dataSrc": ""
        },
        "columns": [

            { "data": "CustomerId" },
            { "data": "CustomerId" },
            { "data": "UserName" },
            { "data": "JoinDate" },
            { "data": "StatusString" },
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
        $('#UserName').html(data.UserName);
        $('#CustomerName').html(data.FirstName + " " + data.LastName);
        $('#Phone').html(data.Phone);
        $('#Email').html(data.Email);
        $('#Address').html(data.Address);
      



        var status = "";
        status += "<label class='radio-inline'>";
        status += "<input type='radio' value='true' name='statusRadio' " + (data.LockStatus ? "checked" : "") + ">Bị khóa";
        status += "</label>";
        status += "<label class='radio-inline'>";
        status += "<input type='radio' value='false' name='statusRadio' " + (!data.LockStatus ? "checked" : "") + ">Hoạt động";
        status += "</label>";
        $('#Status').html(status);


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