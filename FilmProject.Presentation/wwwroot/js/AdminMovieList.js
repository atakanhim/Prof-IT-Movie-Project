$(document).ready(function () {

    var url = "/Movie/MoviesWithCategoryForAdmin";
    $("#loadingMoviesForAdmin").show();

    // Veri çekme işlemi
    $.get(url, null, function (data) {
     
        $("#loadingMoviesForAdmin").hide();
        $("#MovieListRender").html(data);



    });



});