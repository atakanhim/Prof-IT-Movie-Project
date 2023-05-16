$(document).ready(function () {


    $('.product-images').slick({
        dots: true
    });
    $('#isHighLighted').unbind().click(function () {
        var favoriModel = { Id: movieId };
        favoriModel = JSON.stringify(favoriModel);
        $.ajax({
            url: '/Movie/ChangeIsHighLighted',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: favoriModel,
            success: function (response) {
                refreshIsHigLighted(response);
            },
            error: function (xhr, status, error) {
                showErrorToast(xhr.responseText, "error")
            }

        })
    });
    $('#addToArchive').unbind().click(function () {
        var favoriModel = { Id: movieId };
        favoriModel = JSON.stringify(favoriModel);
        $.ajax({
            url: '/Movie/AddToArchive',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: favoriModel,
            success: function (response) {
                refreshAddToArchive(response);
            },
            error: function (xhr, status, error) {
                showErrorToast(xhr.responseText,"error")
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
function refreshIsHigLighted(response) {
        if (response == false)
            $('#isHighLighted').html("<i class='fa fa-star-o' style='color:green'></i> Filmi Öne Çıkar");
        else
            $('#isHighLighted').html("<i class='fa fa-star' style='color:green'></i> Filmi Öne Çıkarma");
}

function refreshAddToArchive(response) {

    if (response == false)
        $('#addToArchive').html("<i class='fa fa-file-archive-o' style='color:green'></i> Filmi Rafa Kaldır ");
    else
        $('#addToArchive').html("<i class='fa fa-archive' style='color:green'></i> Filmi Raftan çıkar ");

}