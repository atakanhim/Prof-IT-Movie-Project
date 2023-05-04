alertify.error("TEST ALERIFY");
$(function () {
    $(".like-button").click(function () {
        let currentComment = $(this).parent().find("small")
        let commentIdValue = $(this).parent().data("itemid");
       
        refreshCommentLike();

        var commentLikeModel = {
            CommentId: commentIdValue,
        };
        
        var model = JSON.stringify(commentLikeModel);
        $.ajax({
            url: '/CommentLike/ChangeCommentLikeStatue',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: model,
            success: function (response) {
                
                $.get('/CommentLike/NumberOfCommentLike', { commentId: commentIdValue }, function (data, textStatus, jqXHR) {
                    commentLikeCount = data;
                    currentComment.text(commentLikeCount + " " + "likes");
                })
                refreshCommentLike();
            },
            error: function (xhr, status, error) {
                alertify.error(xhr.responseText);
            }
        });

      

  
    })
    refreshCommentLike();

    function refreshCommentLike() {
        let comments = $(".comment-like-item")
        comments.each(function () {
            let commentLikeSection = $(this);
            let commentLikeId = ($(this).data("itemid"));
            $.get('/CommentLike/IsCommentLiked', { commentId: commentLikeId }, function (data, textStatus, jqXHR) {
                let commentLikeStatue = data;
                if (commentLikeStatue == true) {
                    commentLikeSection.find("i").addClass("like-button--active");
                } else {  
                    commentLikeSection.find("i").removeClass("like-button--active");
                }

            });
            $.get('/CommentLike/NumberOfCommentLike', { commentId: commentLikeId }, function (data, textStatus, jqXHR) {
                commentLikeCount = data;
                commentLikeSection.find("small").text(commentLikeCount + " " + "likes");
            })
        })
    }


});