﻿$(function(){
    $(".like-button").click(function () {
        let currentComment = $(this).parent().find("small")
        let commentIdValue = $(this).parent().data("itemid");
       
        refreshCommentLike();

        var commentLikeModel = {
            CommentId: commentIdValue,
            userId: "0d161a0c-d0ce-414a-8bc6-02926d70d560"
        };
        
        var model = JSON.stringify(commentLikeModel);
        $.ajax({
            url: '/CommentLike/ChangeCommentLikeStatue',
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            data: model,
            success: function (response) {
                console.log("Listeme ekleme başarılı");
                
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

    function CalculateCommentLike(commentIdValue) {
        let commentLikeCount;
        $.get('/CommentLike/NumberOfCommentLike', { commentId: commentIdValue }, function (data, textStatus, jqXHR) {
            commentLikeCount = data;
            
        }).done(function () {
            console.log("test1 : " + commentLikeCount);
            return commentLikeCount;
        })
        return commentLikeCount;
    }

    



});