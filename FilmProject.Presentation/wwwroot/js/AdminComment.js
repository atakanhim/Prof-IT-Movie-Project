$(function () {
    $.get('/Comment/AllComments', 
        function (data, textStatus, jqXHR) { 

            $("#allComments").html(data);
        });
});