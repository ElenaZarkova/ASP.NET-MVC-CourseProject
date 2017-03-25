$(function () {
    const attrValue = +($("#star-rating").attr("data-checked"));
    const value = Math.round(attrValue);
    $("input[name=rating][value=" + value + "]").attr('checked', 'checked');

    $('#star-rating').rating(function (vote, event) {

        const id = $("#star-rating").attr("data-id");
        $.ajax({
            url: "/book/rate",
            type: "GET",
            data: { rate: vote, id: id },
            success: function (data) {
                const newRating = Math.round(data.rating);
                const $input = $("input[name=rating][value=" + newRating + "]");
                    $input
                    .prop('checked', true)
				    .siblings('input').prop('checked', false);

                    // $("#star-rating").rating.set($input, newRating);
                const ratingCalc = +data.rating;
                $("#rating-calc").text(ratingCalc.toFixed(2));
                $("#user-rating").removeClass("display-none");
                $("#user-rating span").text(vote);
            },
            error: function(err){
                $("#rating-error").text(err.message);
            }
        });
    });
});