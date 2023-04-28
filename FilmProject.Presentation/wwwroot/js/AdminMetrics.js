$(document).ready(function () {

    // toplam yorum sayisi
    $.get('/Comment/TotalCommentCount', function (data) {
        $('#totalComment').html(data.count);
    });
    // toplam film sayısını doner
    $.get('/Movie/GetCount', function (data) {
        $('#moviesCount').html(data.count); 
    });
    // toplam category sayısını doner
    $.get('/Category/GetCount', function (data) {
        $('#categoryCount').html(" " + data);
    });
    // toplam user sayısını doner
    $.get('/Home/GetUserCount', function (data) {
        $('#userCount').html(" "+data);
    });

    
    // en populer film
    $.get('/Movie/GetMostPopular/1', function (data) {
        data = JSON.parse(data);
       
        $('#mostPopulerMovieName').html(" "+data[0].MovieName);
        $('#mostPopulerMovieOrtalama').html(" " +data[0].Ortalama + "/10");

    });

    // en cok yorum alan film
    $.get('/Movie/GetMostCommentedMovie/1', function (data) {
        data = JSON.parse(data);
       
        $('#mostCommentedMovieName').html(" " +data[0].MovieName);
        $('#mostCommentedMovieCommentCount').html(" " +data[0].Comments.length+" yorum");
    });





    // tanımlı dil ve sayısını doner
    $.get('/Movie/GetLanguages', function (data) {
        
        $('#languageH5').html(data[0].language + " / " + data[1].language + " Diline Sahip Filmler");
        $('#languageOran').html(data[0].count + " / " + data[1].count);

        // sparkline
        $("#sparkline1").sparkline([data[0].count, data[1].count], {
            type: 'pie',
            height: '140',
            sliceColors: ['#1ab394', '#F5F5F5']
        });

    });

    $.get('/Movie/GetRatingAge', function (data) {

        $('#ratingAgeH5').html(data[0].ratingAge + " / " + data[1].ratingAge + " ");
        $('#ratingAgeOran').html(data[0].count + " / " + data[1].count);


        $("#sparkline2").sparkline([data[0].count, data[1].count], {
            type: 'pie',
            height: '140',
            sliceColors: ['#1ab394', '#F5F5F5']
        });
    });

    
});


