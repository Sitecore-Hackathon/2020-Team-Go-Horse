$(function() {
    
    $("#module-doc-tabs button").click(function() {
        var allTabs = $("#module-doc-tabs button");
        var clickedIndex = allTabs.index(this);
        var allCards = $("#module-doc-pages .card");
        var cardToShow = $(allCards[clickedIndex]);

        // Enable correct Tab
        allTabs.removeClass("active");
        $(this).addClass("active");

        // Show correct Page
        allCards.hide();
        cardToShow.show();
    });
});