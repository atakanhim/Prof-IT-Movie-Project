// categort map
$('.custom__category').unbind().click(function () {
    $("#loadingMovies").show();
    $('.custom__category').removeClass('active');
    $('.custom__category').removeClass('left');

    var url = "";
    var text = $(this).find("a").data("id");

    if (text == "GetLastMovies" || text == "GetMostPopular" || text == "GetMostCommentedMovie") {
        url = "/Movie/" + text;
        switch (text) {
            case "GetLastMovies":
                text = "En Son Yüklenen Filmler";
                break;
            case "GetMostPopular":
                text = "En Çok Beğenilen Filmler";
                break;
            case "GetMostCommentedMovie":
                text = "En Çok Yorum Alan Filmler";
                break;
            default:
                break;
        }
    }
    else {
        url = "/Movie/MoviesWithCategory/" + text;
        text += " Kategorisindeki Filmler"
    }

    $.get(url, null, function (data) {
        $("#loadingMovies").hide();
        $("#movieRenderTitle").html(text);
        $("#movieRender").html(data);

    });

    // Tıklanan öğeye active sınıfını ekle
    $(this).addClass('active');
    $(this).addClass('left');
});