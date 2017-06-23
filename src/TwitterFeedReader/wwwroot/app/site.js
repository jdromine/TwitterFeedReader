function filter(element) {
    var value = $(element).val();

    $("#tweetList > li").each(function () {
        if ($(this).text().search(value) > -1) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
}