$(function () {

    //Puanlama Yıldızları
    const stars = document.querySelectorAll(".stars i");
    console.log(stars);
    let counter = 0;
    stars.forEach((star, index1) => {
        star.addEventListener("click", () => {
            counter = index1;
            $.post("/Home/GivePoint", { Score: ++counter});
            console.log("counter" + counter)
            stars.forEach((star, index2) => {
                index1 >= index2 ? star.classList.add("active") : star.classList.remove("active");
            });
        });
    });


    //Yorumun Getirilip post edilmesi
    $("#btnSentComment").click(function () {
        var commentContent = $("#commentContent").val();
        var movieId = $("#movieHeader").attr("data-id");
        //$.post("/Comment/Create", { Content: commentContent, MovieId: movieId });
        //console.log("Yorum Ekle");



        console.log("comment content:" + commentContent);
        console.log("movie Id: " + movieId);
        var model = { Content: commentContent, MovieId: movieId };
        var commentModel = JSON.stringify(model);
        $.ajax({
            url: '/Comment/Create',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: commentModel,
            success: function (response) {
                console.log("Yorum ekleme başarılı");
            },
            error: function (xhr, status, error) {
              
                console.log("hata: " + error);
               
            }
        });
    });


    //Favorilere Ekleme Silme
    $("btnAddMyList").click(function () {

        //$.post("/Home/ChangeFavorites", { userId:, MovieId: });
    })



  

});



