// partical view donduruyor

$(document).ready(function () {
    var url = "/Movie/MoviesWithCategory";
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").append(data);
        // append dedigimiz icin bolye
    });

});