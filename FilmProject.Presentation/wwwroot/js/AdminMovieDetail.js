$(document).ready(function () {


    $('.product-images').slick({
        dots: true
    });
    let url = "/Inspinia/Movie/IsHighLighted/" + movieId;
    $.get(url, null, function (data) {
        console.log(data);
        if (data == false)
            $('#isHighLighted').html("<i class='fa fa-star-o' style='color:green'></i> Filmi Öne Çıkar");
        else
            $('#isHighLighted').html("<i class='fa fa-star' style='color:green'></i> Filmi Öne Çıkarma");
    });



});