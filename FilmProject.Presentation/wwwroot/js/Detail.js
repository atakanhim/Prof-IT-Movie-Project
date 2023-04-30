$(function () {
    var movieId = $("#movieHeader").attr("data-id");
    var key = 0;

    // yorumlar listeleniyor
    refreshComments();
    refreshMovieLike()
    //Puanlama Yıldızları
    const stars = document.querySelectorAll(".stars i");

    let counter = 0;
    stars.forEach((star, index1) => {
        star.addEventListener("click", () => {
            counter = index1;
            $.post("/Home/GivePoint", { Score: ++counter })
                .done(function (data) {
                    // işlem başarılı oldu
                    refreshMovieLike();
                    alertify.success("Puan verdiğiniz için teşekkürler");
                   
                })
                .fail(function (xhr) {
                    // işlem başarısız oldu
                    alertify.error(xhr.responseText);
                });                ;
            stars.forEach((star, index2) => {
                index1 >= index2 ? star.classList.add("active") : star.classList.remove("active");
            });
        });
    });


    //Add My List Button
    let addMyListButton = $("#btnAddMyList")
    if (true) {
        addMyListButton.addClass("add-list-btn--added");
    } 




    //Yorumun Getirilip post edilmesi
    $("#btnSentComment").click(function () {
        var commentContent = $("#commentContent").val();
        //$.post("/Comment/Create", { Content: commentContent, MovieId: movieId });


        var model = { Content: commentContent, MovieId: movieId };
        var commentModel = JSON.stringify(model);
        $.ajax({
            url: '/Comment/Create',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: commentModel,
            success: function (response) {
                refreshComments();
                alertify.success("Puan verdiğiniz için teşekkürler");

            },
            error: function (xhr, status, error) {
                alertify.error(xhr.responseText);
            }
        });
    });


    //Favorilere Ekleme Silme
    $("#btnAddMyList").click(function () {
        
        if (key == 0) {
            addMyListButton.removeClass("add-list-btn--added");
            key = 1;
        } else {
            addMyListButton.addClass("add-list-btn--added");
            key = 0;
        }




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
                alertify.success(xhr.responseText);
                console.log("hata: " + error);
            }
        });
    })


    // refresh comments
    function refreshComments() {
        $("#loadingComments").show();
        $.get('/Comment/List/' + movieId, function (data) {          
            $("#loadingComments").hide();
            $("#commentRender").html(data);
        });
        $.get('/Comment/Count/' + movieId, function (data) {
            if (data > 0)
                $("#commentCount").html(data + " " + localizer.reviews);
            else 
                $("#commentCount").html(localizer.noComment);
        });


    }

    function refreshMovieLike() {

        $.get('/Movie/LikeAvarage/' + movieId, function (data) {     
                $("#movieScore").html(data);    
        });
        $.get('/Movie/LikeCount/' + movieId, function (data) {
            $("#movieLikeCount").html(data);
        });
    }
});



