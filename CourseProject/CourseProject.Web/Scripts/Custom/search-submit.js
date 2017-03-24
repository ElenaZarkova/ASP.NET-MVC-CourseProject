$(function () {
    $(".submit-on-checked").change(function (ev) {
        var $that = $(this);
        if ($that.attr("type") == "radio" && !$that.is(":checked")) {
            return;
        }
        $("#search-form").submit();
    })

    //$(".page").click(function (ev) {
    //    var $that = $(this);
    //    var page = $that.attr("data-page").trim();
    //    var $form = $("#search-form");

    //    var input = $("<input>")
    //           .attr("type", "hidden")
    //           .attr("name", "page")
    //            .val(page);
    //    $form.append($(input));
    //    $form.submit();
    //})
})