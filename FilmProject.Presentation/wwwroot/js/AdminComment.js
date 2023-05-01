$(function () {

//Yorumların Getirilmesi
    console.log("deneme");
    //$.get("/AdminVerifyComment/MovieComments", function (data, status) {
    //    console.log(data);
    //    console.log(status);
    //});


    $.ajax({
        url: '/Inspinia/AdminVerifyComment/MovieComments',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            
        },
        error: function (xhr, status, error) {
            let text = xhr.responseText
            let icon = "error";
            console.log(error);
        }
    });








});