$(function () {
    $(".submit-on-checked").change(function (ev) {
        var $that = $(this);
        if ($that.attr("type") == "radio" && !$that.is(":checked")) {
            return;
        }
        $("#search-form").submit();
    })

    $("#search-results").click(function (ev) {
        var $target = $(ev.target);
        if (!$target.hasClass("page")) {
            return;
        }
        var page = $target.attr("data-page").trim();
        var $form = $("#search-form");

        var $input = $("<input>")
               .attr("type", "hidden")
               .attr("name", "page")
                .val(page);
        $form.append($input);
        $form.submit();
        $input.remove();
    })
})