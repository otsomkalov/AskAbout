$(document).ready(function () {
    $(".like").click(function () {
        var model = {
            id: $(this).attr("question-id")
        };

        $.ajax({
            type: "POST",
            url: "/Question/Like",
            data: model,
            success: function () {
                var currentLikesCount = parseInt($("#likes-count").html());
                $("#likes-count").html(currentLikesCount + 1);
            }
        });
    });

    $(".dislike").click(function () {
        var model = {
            id: $(this).attr("question-id")
        };

        $.ajax({
            type: "POST",
            url: "/Question/Dislike",
            data: model,
            success: function () {
                var currentLikesCount = parseInt($("#likes-count").html());
                $("#likes-count").html(currentLikesCount - 1);
            }
        });
    })
});