$(".show-more a").on("click", function () {
    var $this = $(this);
    var $content = $this.parent().prev("div.content");
    var linkText = $this.text();

    if (linkText === "Hiển thị thêm") {
        linkText = "Thu gọn";
        $content.switchClass("hideContent", "showContent", 400);
    } else {
        linkText = "Hiển thị thêm";
        $content.switchClass("showContent", "hideContent", 400);
    };

    $this.text(linkText);
});