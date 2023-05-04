$(document).ready(function () {


    $('.product-images').slick({
        dots: true
    });
    refreshDataTable();
    $('#isHighLighted').unbind().click(function () {     
            var favoriModel = { MovieId: movieId };
            favoriModel = JSON.stringify(favoriModel);
            $.ajax({
                url: '/Favorite/ChangeFavorite',
                method: 'POST',
                contentType: "application/json; charset=utf-8",
                data: favoriModel,
                success: function (response) {
                    console.log("Favoriye ekleme başarılı");
                },
                error: function (xhr, status, error) {
                    alertify.error(xhr.responseText);
                }
          
        })
    });

});


// fonksiyonlar 

// sweet alert fonksiyonu
function showErrorToast(text, icon) {
    Swal.fire({
        position: 'middle',
        icon: icon,
        title: text,
        showConfirmButton: false,
        timer: 2500
    });
}


// datatable refresh fonksiyonu
function refreshDataTable() {
    
    let url = "/Inspinia/Movie/IsHighLighted/" + movieId;
    $.get(url, null, function (data) {
        console.log(data);
        if (data == false)
            $('#isHighLighted').html("<i class='fa fa-star-o' style='color:green'></i> Filmi Öne Çıkar");
        else
            $('#isHighLighted').html("<i class='fa fa-star' style='color:green'></i> Filmi Öne Çıkarma");
    });


}