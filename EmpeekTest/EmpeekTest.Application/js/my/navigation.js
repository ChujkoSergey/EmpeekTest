$("body div.container").each(function () {
    $(this).hide();
})

$("body div.container").first().show();

$(".nav-button").each(function () {
    $(this).click(function () {
        show("." + $(this).attr("id").split("-").pop() + "-container");
    });
});

function show(window) {
    $("body div.container").each(function () {
        $(this).hide();
    })
    $(window).show();
    if (window = ".stat-container") {
        angular.element($("#stat-container")).scope().RefreshStatTable();
    }
}

$(function () {
    $("#navItems").bind("click", function () {
        show($("#navItems").text());
    });

    $("#statItems").bind("click", function () {
        show($("#statItems").text());
    });
})