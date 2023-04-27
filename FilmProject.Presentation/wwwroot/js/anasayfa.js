// partical view donduruyor

$(document).ready(function () {
    var url = "/Movie/MoviesWithCategory";


    $.get(url, null, function (data) {
        $("#movieRender").html(data);    
    });



});