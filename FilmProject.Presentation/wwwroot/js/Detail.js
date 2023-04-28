$(function () {
    var key = 0;
    //Puanlama Yıldızları
    const stars = document.querySelectorAll(".stars i");
    console.log(stars);
    let counter = 0;
    stars.forEach((star, index1) => {
        star.addEventListener("click", () => {
            counter = index1;
            $.post("/Home/GivePoint", { Score: ++counter});
            stars.forEach((star, index2) => {
                index1 >= index2 ? star.classList.add("active") : star.classList.remove("active");
            });
        });
    });


    //Add My List Button
    let addMyListButton = $("#btnAddMyList")
    if (true) {
        addMyListButton.addClass("add-list-btn--added");
        addMyListButton.html('<span> <i class="fa fa-trash"></i> Discard</span>');
    } 




    //Yorumun Getirilip post edilmesi
    $("#btnSentComment").click(function () {
        var commentContent = $("#commentContent").val();
        var movieId = $("#movieHeader").attr("data-id");
        //$.post("/Comment/Create", { Content: commentContent, MovieId: movieId });


        var model = { Content: commentContent, MovieId: movieId };
        var commentModel = JSON.stringify(model);
        $.ajax({
            url: '/Comment/Create',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: commentModel,
            success: function (response) {
                console.log("Yorum ekleme başarılı");
                window.location.href = "/Home/Detail?id=" + movieId;
            },
            error: function (xhr, status, error) {
              
                console.log("hata: " + error);
               
            }
        });
    });


    //Favorilere Ekleme Silme
    $("#btnAddMyList").click(function () {
        
        if (key == 0) {
            addMyListButton.removeClass("add-list-btn--added");
            addMyListButton.html('<span> <i class="fa fa-heart"></i> Add My List</span>');
            key = 1;
        } else {
            addMyListButton.addClass("add-list-btn--added");
            addMyListButton.html('<span> <i class="fa fa-trash"></i> Discard</span>');
            key = 0;
        }


        var movieId = $("#movieHeader").attr("data-id");

        var model = { MovieId: movieId};
        var commentModel = JSON.stringify(model);
        $.ajax({
            url: '/Favorite/ChangeFavorite',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: commentModel,
            success: function (response) {
                console.log("Listeme ekleme başarılı");
            },
            error: function (xhr, status, error) {

                console.log("hata: " + error);

            }
        });
    })



  

});



