// partical view donduruyor

$(document).ready(function () {
    var url = "/Movie/MoviesWithCategory";
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").html(data);
        // append dedigimiz icin bolye
    });

});