$(function(){
    $(".like-button").click(function () {
        let commentIdValue = $(this).data("itemid");
        let model = { commentId: commentIdValue }
        CommentLikeModel = JSON.stringify(model);
        //console.log(movieId);


        //$.ajax({
        //    url: '/CommentLike/ChangeLike',
        //    method: 'POST',
        //    contentType: "application/json; charset=utf-8",
        //    data: CommentLikeModel,
        //    success: function (response) {
        //        console.log("beğeni başarılı")
        //        //refreshComments();


        //    },
        //    error: function (xhr, status, error) {
        //        console.log(error);
        //    }
        //});




        console.log(commentIdValue);
        //var commentLikeModel = { CommentId: commentIdValue, userId: "0d161a0c- d0ce - 414a-8bc6 - 02926d70d560"}
        //    //commentLikeModel = JSON.stringify(commentLikeModel);
        //    $.ajax({
        //        url: '/CommentLike/Test',
        //        method: 'POST',
        //        contentType: "application/json; charset=utf-8",
        //        data: commentLikeModel,
        //        success: function (response) {
        //            console.log("Listeme ekleme başarılı");
        //        },
        //        error: function (xhr, status, error) {
        //            alertify.error(xhr.responseText);
        //        }
        //    });


        var commentLikeModel = {
            CommentId: commentIdValue,
            userId: "0d161a0c-d0ce-414a-8bc6-02926d70d560"
        };
        console.log(commentLikeModel);
        var a = JSON.stringify(commentLikeModel);
        $.ajax({
            url: '/CommentLike/ChangeCommentLikeStatue',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: a,
            success: function (response) {
                console.log("Listeme ekleme başarılı");
            },
            error: function (xhr, status, error) {
                alertify.error(xhr.responseText);
            }
        });


        //$.post('/CommentLike/Test', { userId: "Yusuf", CommentId = 12 });
  
    })








});