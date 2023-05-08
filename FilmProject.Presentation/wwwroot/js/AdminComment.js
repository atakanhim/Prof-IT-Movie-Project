$(document).ready(function () {

    $("#comment-table").DataTable({
        "pageLength": 25,
        "dom": '<"html5buttons"B>lTfgitp',
        "buttons": [
            { extend: 'copy' },
            { extend: 'csv' },
            { extend: 'excel', title: 'ExampleFile' },
            { extend: 'pdf', title: 'ExampleFile' },

            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],

        "ajax": {
            "url": "/comment/AllComments",
            "type": "GET",
            "datatype": "json",
            "error": function (xhr, error, thrown) {
                console.log(xhr.responseText);
                console.log("Bir hata oluştu: " + xhr.status + " - " + xhr.responseText);
            }
        },
       
        "columns": [
            { "data": "content" },
            { "data": "isConfirm" },
            { "data": "movieName" },
            { "data": "userName" },
            { "data": "userId" },
            { "data": "created" },
        ]
    });



    //var jqxhr = $.get("/comment/AllComments", function () {
    //    console.log("success");
    //}).done(function () {
    //    console.log("second success");
    //})
    //    .fail(function () {
    //        console.log("error");
    //    })
    //    .always(function () {
    //        console.log("second finish");
    //    });
})