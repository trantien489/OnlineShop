function AddToCart(productId)
{
    $.ajax({
        type: "POST",
        url: '/cart/AddtoCart/',
        data: { productId: productId },
        success: function (data) {
            $.ajax({
                type: "GET",
                url: '/cart/GetInfoCart/',
                success: function (data2) {
                    $('#CountCartItem').html("(" + data2.ItemCount + ") Giỏ hàng");
                    $('#alertMessage').html("Thêm giỏ hàng thành công");
                    $('#AlertModal').modal('show');
                }
            });
        }
    });
}

function AddCart(productId)
{
    $.ajax({
        type: "POST",
        url: '/cart/AddtoCart/',
        data: { productId: productId },
        success: function (data) {
            ReloadCart()
        }
    });
}

function SubtractionCart(productId) {
    $.ajax({
        type: "POST",
        url: '/cart/SubtractionCart/',
        data: { productId: productId },
        success: function (data) {
            ReloadCart()
        }
    });
}

function DeleteCart(productId) {
    $.ajax({
        type: "POST",
        url: '/cart/DeleteCart/',
        data: { productId: productId },
        success: function (data) {
            ReloadCart()
        }
    });
}

function ReloadCart() {
    $.ajax({
        type: "GET",
        url: '/cart/GetInfoCart/',
        success: function (data) {
            var html ="";
            $.each(data.carts, function (key, value) {
                html += "<tr >";
                html +=     "<td>";
                html +=         "<a href='#'>";
                html +=             "<img src='/Photos/Product/"+value.Image +"' alt='Lỗi ảnh'>";
                html +=         "</a>";
                html +=     "</td>";
                html +=     "<td>";
                html +=         "<a href='/san-pham/" +value.Product.MetaKeyword+"/chi-tiet'>"+value.Product.ProductName+"</a>";
                html +=     "</td>";
                html +=     "<td style='adding-top: 8px;'>";
                html += "<i class='fa fa-minus' style='float: left;padding-top: 11px;padding-right: 6px;cursor: pointer;' onclick='SubtractionCart(" + value.Product.Id + ")'></i>";
                html +=        "<input type='text' value='"+value.Quantity+"' class='form-control' style='float: left;' readonly>";
                html += "<i class='fa fa-plus' style='float: left;padding-top: 11px; padding-left: 8px;cursor: pointer;' onclick='AddCart(" + value.Product.Id + ")'></i>";
                html +=     "</td>";
                html +=     "<td>" + value.Price + " VND</td>";
                html +=     "<td>" + value.Money + " VND</td>";
                html +=      "<td>";
                html += "<i class='fa fa-trash-o' style='cursor:pointer;font-size: 20px;color: red;' onclick='DeleteCart(" + value.Product.Id + ")'></i>";
                html += "</td>";
                html += "</tr>";
            });
            $("#CartContent").html(html);
            $('#TotalMoneyCart').html(data.Total+ " VND");
        }
    });
}