$(function () {
    var movieId = $("#movieHeader").attr("data-id");
    var favoriteStatue;
    let addMyListButton = $("#btnAddMyList")
    var key;



    //  ||Favori

    //Favoriye eklenme kontrolü
    $.get('/Favorite/IsMyFavorite', { MovieId: movieId }, function (data, textStatus, jqXHR) {
        favoriteStatue = data;
        if (favoriteStatue == false) {
            addMyListButton.addClass("add-list-btn--added");
            key = false;
        } else {
            key = true;
        }
    });

    //Favorilere Ekleme Silme
    $("#btnAddMyList").click(function () {

        if (key == false) {
            addMyListButton.removeClass("add-list-btn--added");
            key = true;
        } else {
            addMyListButton.addClass("add-list-btn--added");
            key = false;
        }

        var favoriModel = { MovieId: movieId };
        favoriModel = JSON.stringify(favoriModel);
        $.ajax({
            url: '/Favorite/ChangeFavorite',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: favoriModel,
            success: function (response) {
                console.log("Listeme ekleme başarılı");
            },
            error: function (xhr, status, error) {
                alertify.error(xhr.responseText);
            }
        });
    })
    function starLight(value) {
        value = value - 1;
        stars.forEach((star, index2) => {
            value >= index2 ? star.classList.add("active") : star.classList.remove("active");
        });
    }


    //  ||Yorumlar ve Movie Score

    // yorumlar listeleniyor
    refreshComments();
    refreshMovieLike()
    //Puanlama Yıldızları
    const stars = document.querySelectorAll(".stars i");

    let counter = 0;

    stars.forEach((star, index1) => {
        star.addEventListener("click", () => {
            counter = index1;
            let viewmodel = {
                MovieId: movieId,
                Point: ++counter,
            };
            viewmodel = JSON.stringify(viewmodel);
            $.ajax({
                url: '/MovieLike/GivePoint',
                method: 'POST',
                contentType: "application/json; charset=utf-8",
                data: viewmodel,
                success: function (response) {
                    refreshMovieLike();
                    alertify.success(response);
                    starLight(counter);
                },
                error: function (xhr, status, error) {
                    alertify.error(xhr.responseText);
                }
            });

           
        });
    
    });



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
                alertify.success("Yorum attığınız için teşekkürler");

            },
            error: function (xhr, status, error) {
                alertify.error(xhr.responseText);
            }
        });
    });




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
            $("#movieLikeCount").html(data + " " + localizer.vote);
        });
        $.get('/MovieLike/GetPoint/' + movieId, function (data) {
           starLight(data);
        });
       
      
    }
});



