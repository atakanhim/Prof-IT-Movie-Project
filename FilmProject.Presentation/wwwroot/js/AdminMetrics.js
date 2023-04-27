$(document).ready(function () {

    // toplam yorum sayisi
    $.get('/Comment/TotalCommentCount', function (data) {


        $('#totalComment').html(data.count);


    });
    // toplam film sayısını doner
    $.get('/Movie/GetCount', function (data) {
        $('#moviesCount').html(data.count);

     
    });
    // en populer film
    $.get('/Movie/GetMostPopular/1', function (data) {
        data = JSON.parse(data);
       
        $('#mostPopulerMovieName').html(data[0].MovieName);
        $('#mostPopulerMovieOrtalama').html(data[0].Ortalama + "/10");

    });

    // en cok yorum alan film
    $.get('/Movie/GetMostCommentedMovie/1', function (data) {
        data = JSON.parse(data);
       
        $('#mostCommentedMovieName').html(data[0].MovieName);
        $('#mostCommentedMovieCommentCount').html(data[0].Comments.length+" yorum");


    });

    
});


