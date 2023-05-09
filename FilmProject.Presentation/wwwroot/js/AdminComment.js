$(document).ready(function () {

    var table = $("#comment-table").DataTable({
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
            { "data": "id", "visible": false },
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
                        return "<button class='btn btn-danger commentChangeStatue w-100'>Ban</button>"
                    } else {
                        return "<button class='btn btn-primary commentChangeStatue w-100'>Verify</button>"
                    }
                }
            },

 
        ],
        "initComplete": function (settings, json) {
           /* var table = $("#comment-table").DataTable();*/
            $(".commentChangeStatue").click(function () {
                $(document).on("click", ".commentChangeStatue", function () {
                    console.log("test");
                    var data = $("#comment-table").DataTable().row($(this).closest("tr")).data();
                    var commentId = data.id;

                    console.log("Comment ID: " + commentId);
                    $.ajax({
                        url: 'https://localhost:7269/Comment/ChangeState/' + commentId,
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json',
                        success: function (result) {
                            console.log(result);
                            table.ajax.reload();
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log(jqXHR.responseText);
                        }
                    });
                });
            
            })
        }
    });



})

