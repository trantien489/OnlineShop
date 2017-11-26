function AddCart(productId)
{
    $.ajax({
        type: "POST",
        url: '/cart/AddtoCart/',
        data: { productId: productId },
        success: function (data) {
            console.log(data);
            $('#alertMessage').html("Thêm giỏ hàng thành công");
            $('#AlertModal').modal('show');
        }
    });
}