$(function () {
    const value = $("#star-rating").attr("data-checked");
    $("input[name=rating][value=" + value + "]").attr('checked', 'checked');

    $('#star-rating').rating(function (vote, event) {
        const id = $("#star-rating").attr("data-id");
        $.ajax({
            url: "/book/rate",
            type: "GET",
            data: { rate: vote, id: id },
            success: function (data) {
                $("input[name=rating][value=" + vote + "]").attr('checked', 'checked');
                $("#rating-calc").text(data.rating);
            },
            error: function(err){
                $("#rating-error").text(err.message);
            }
        });
    });
});