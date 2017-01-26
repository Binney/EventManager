$("button").click(function () {
    $.ajax({
        url: "events/filter?type=" + this.value,
        dataType: "html",
        success: function (data) {
            $("#events").children(".table").remove();
            $("#events").append(data);
        }
    });
});

$(".btn-toggle").click(function () {
    $(this).find(".btn").toggleClass("active");

    console.log($(this).find(".btn-primary"));
    if ($(this).find(".btn-primary").size() > 0) {
        $(this).find(".btn").toggleClass("btn-primary");
    }

    $(this).find(".btn").toggleClass("btn-default");
});
