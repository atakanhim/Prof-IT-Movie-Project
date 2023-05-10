$(function () {
    var movieId = $("#movieHeader").attr("data-id");
    console.log(movieId);
    var favoriteStatue;
    let addMyListButton = $("#btnAddMyList")
    var key;


    "use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/commentHub").build();
    var filmIdString = $("#movieHeader").attr("data-id");
 
    connection.on("ReceiveComment", function (comment) {
        console.log(comment);
        
        console.log("Veri geldi");
        refreshComments();
        
    });

    connection.start().then(function () {
        connection.invoke("JoinFilmGroup", filmIdString).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("Bağlantı kuruldu.");
    }).catch(function (err) {
        return console.error(err.toString());
    });


    //  ||Favori

    //Favoriye eklenme kontrolü
    $.get('/Favorite/IsMyFavorite', { MovieId: movieId }, function (data, textStatus, jqXHR) {
        favoriteStatue = data;
        if (favoriteStatue == false) {
            key = false;
        } else {
            key = true;
            addMyListButton.html('<i class="fa fa-trash"></i> Remove from My List');
            addMyListButton.addClass("add-list-btn--added");
        }
    });

    //Favorilere Ekleme Silme
    $("#btnAddMyList").click(function () {

        if (key == false) {
            addMyListButton.addClass("add-list-btn--added");
            
            addMyListButton.html('<i class="fa fa-trash"></i> Remove from My List');
            key = true;
        } else {
            addMyListButton.removeClass("add-list-btn--added");
            addMyListButton.html('<i class="fa fa-heart-o"></i> <span>Add My List</span>');
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
                if (xhr.status == 401) {
                    alertify.error(localizer.add_list_required_signin);
                } else{
                    alertify.error(localizer.connection_error);
                }
         
             
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
                alertify.success(localizer.thanks_message_comment);
                //connection.invoke("AddComment", movieId, commentContent);

            },
            error: function (xhr, status, error) {
                alertify.error(xhr.responseText);
            }
        });
    });




    // refresh comments
    function refreshComments() {
    
        $("#commentContent").val("");
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



