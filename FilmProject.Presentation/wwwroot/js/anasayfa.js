// partical view donduruyor

$(document).ready(function () {
    var url = "/Movie/MoviesWithCategory";
    $("#loadingMovies").show();

    // Veri çekme işlemi
    $.get(url, null, function (data) {

        $("#loadingMovies").hide();
        $("#movieRenderTitle").html("Tavsiye Filmler");
        $("#movieRender").html(data);



    });

    $('a[href^="#"]').on('click', function (e) {
        e.preventDefault();
        var target = this.hash;
        var $target = $(target);
        $('html, body').stop().animate({
            'scrollTop': $target.offset().top
        }, 1000, 'swing', function () {
            window.location.hash = target;
        });
    });

});