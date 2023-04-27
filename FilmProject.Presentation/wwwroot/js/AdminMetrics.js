$(document).ready(function () {


    // toplam film sayısını doner
    $.get('/Movie/GetCount', function (data) {
        $('#totalComment').html(data.count);

     
    });
    // en populer film
    $.get('/Movie/GetMostPopular/1', function (data) {
        data = JSON.parse(data);
      
        $('#mostPopulerMovie').html(data[0].MovieName);


    });


});


