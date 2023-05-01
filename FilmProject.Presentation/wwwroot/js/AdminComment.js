$(function () {
    console.log("JS DOSYASI TEST");
    $.get('/Comment/AllComments',  // url
        function (data, textStatus, jqXHR) {  // success callback
            console.log("ADMİN JQUERY RUNNED");
            $("#allComments").html(data);
            console.log(data);
        });
});