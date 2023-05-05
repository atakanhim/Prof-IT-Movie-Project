// partical view donduruyor

$(document).ready(function () {
    var url = "/Movie/MoviesWithCategory";
    $("#loadingMovies").show();

    $('a[href^="#"]').on('click', function (event) {
        var target = $(this.getAttribute('href'));
        if (target.length) {
            event.preventDefault();
            $('html, body').stop(0).delay(0).animate({
                scrollTop: target.offset().top
            }, 100);
        }
    });


    // Veri çekme işlemi
    $.get(url, null, function (data) {

        $("#loadingMovies").hide();
        $("#movieRenderTitle").html("Tavsiye Filmler");
        $("#movieRender").html(data);



    });



});