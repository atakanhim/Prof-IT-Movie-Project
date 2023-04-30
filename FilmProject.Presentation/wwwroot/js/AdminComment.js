$(function () {

//Yorumların Getirilmesi
    $.get("/Category/ListAll", function (data, status) {
        console.log(data);
        console.log(status);
    });











});