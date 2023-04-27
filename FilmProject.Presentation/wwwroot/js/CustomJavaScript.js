
$('#last_uploadedMovies').unbind().click(function () {
    var url = "/Movie/GetLastMovies"; // son yuklenen filmler listeleniyors
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").html(data);

    });
});
$('#most_popularMovies').unbind().click(function () {
    var url = "/Movie/GetMostPopular"; // en cok begeni alan filmler
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").html(data);

    });
});
$('#most_commentedMovies').unbind().click(function () {
    var url = "/Movie/GetMostCommentedMovie"; // en cok yorum alan filmler
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").html(data);

    });
});
$('#headerLogoClick').unbind().click(function () {
    var url = "/Movie/MoviesWithCategory"; // anasayfa normal listeleniyor  
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").html(data);

    });
});

// categort map
$('.custom__category a').unbind().click(function () {
 
    /*var text = $(this)[0].innerText;*/
    var text = $(this).data("id");  
    var url = "/Movie/MoviesWithCategory/" + text + "";
    $.get(url, null, function (data) {
        console.log(data);
        $("#movieRender").html(data);

    });
});