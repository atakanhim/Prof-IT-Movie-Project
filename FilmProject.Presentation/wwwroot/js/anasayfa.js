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



});