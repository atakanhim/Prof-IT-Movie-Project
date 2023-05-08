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
            { "data": "movieName" },
            { "data": "userName" },
            //{ "data": "userId" },
            {
                data: "created", render: function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        var date = new Date(data);
                        return date.toLocaleDateString('tr-TR');
                    } else {
                        return data;
                    }
                }
            },
            {
                data: "isConfirm", render: function (data, type, row){
                    if (data == true) {
                        return "<button class='btn btn-danger edit-button  w-100'>Ban</button>"
                    } else {
                        return "<button class='btn btn-primary w-100'>Verify</button>"
                    }
                }
            },

 
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