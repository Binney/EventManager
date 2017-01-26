$(".btn-toggle").click(function () {
    $(this).find(".btn").toggleClass("active");

    $(this).find(".btn").toggleClass("btn-default");

});

$("form").submit(function () {
    alert($(this["options"]).val());
    return false;
});