$(document).ready(() => {
    $(".ui.dropdown.item")
        .dropdown();
});

let search = (el) => {
    $.post("/Questions/Search",
        {
            title: el.value
        },
        (data, status) => {
            if (status === "success") {
                console.log(data);
            }
        });
};
$(".ui.search")
    .search({
        minCharacters: 3,
        apiSettings: {
            onResponse: (res) => {
                res.results.forEach((r) => {
                    r.id = `/Questions/Details/${r.id}`;
                });
                console.log(res);
                return res;
            },
            url: "/Questions/Search?title={query}"
        },
        fields: {
            results: "results",
            title: "title",
            url: "id"
        }
    });
$(document)
    .ready(function() {
        $(".special.card .image").dimmer({
            on: "hover"
        });
        $(".card .dimmer")
            .dimmer({
                on: "hover"
            });
    });

$(".menu .browse").popup();

// more click
$('.card-profile-stats-more-link').click(function (e) {
    e.preventDefault();
    if ($(".card-profile-stats-more-content").is(':hidden')) {
        $('.card-profile-stats-more-link').find('i').removeClass('fa-angle-down').addClass('fa-angle-up');
    } else {
        $('.card-profile-stats-more-link').find('i').removeClass('fa-angle-up').addClass('fa-angle-down');
    }
    $(this).next('.card-profile-stats-more-content').slideToggle();
});

