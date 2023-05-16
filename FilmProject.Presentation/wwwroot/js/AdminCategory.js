
$(document).ready(function () {
    var table = $('#categories-datatable');
    $.ajax({
        url: '/Category/ListAll',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Pre-process the data if necessary
            var preProcessedData = data;
         
            // Pass the pre-processed data to the DataTable
            table = $('#categories-datatable').DataTable({
                data: preProcessedData,
                pageLength: 25,
                dom: '<"html5buttons"B>lTfgitp',
                buttons: [
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
                columns: [
                    { data: "id" },
                    { data: "categoryName" },
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
                        "data": null,
                        "defaultContent": "<div class='d-flex  justify-content-between '><button class='btn btn-primary edit-button'>Edit</button><button class='btn btn-danger delete-button'>Delete</button></div>"
                    }

                ]
            });
        },
        error: function (xhr, status, error) {
            let text = xhr.responseText
            let icon = "error";
            showErrorToast(text, icon);
        }
    });

    // Delete button click event
    $('#categories-datatable tbody').on('click', '.delete-button', function () {

        let data = table.row($(this).parents('tr')).data();
        let id = data.id;
 
        $.ajax({
            url: '/MovieCategoryMap/AnyMoviesInThis/' + id,
            type: 'GET',
            dataType: 'array',
            success: function (data2) {
                console.log(data2);

                if (data2 == true) {
                    let text = "Bu Kategoriye Sahip filmler olduğu için bu kategori silinemez.";
                    showErrorToast(text, "error");
                }
                else {
                    Swal.fire({
                        title: 'Kategoriyi silmek istediğinize emin misiniz?',
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonText: 'Evet, sil',
                        cancelButtonText: 'Hayır, vazgeç'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $.ajax({
                                url: '/Category/Delete',
                                method: 'POST',
                                data: { id: id },
                                success: function (response) {
                                    let text = "Kategori silme işlemi başarılı.";
                                    let icon = "success";
                                    showErrorToast(text, icon);
                                    refreshDataTable(table);
                                },
                                error: function (xhr, status, error) {
                                    let text = xhr.responseText
                                    let icon = "error";
                                    showErrorToast(text, icon);
                                }
                            });
                        }
                    });
                }
            },
            error: function (xhr, status, error) {
                let text = xhr.responseText;
                let icon = "error";
                showErrorToast(text, icon);
            }
        });


    

       

    });

    // Edit butonuna tıklandığında, modal penceresindeki formu doldur
    $('#categories-datatable tbody').on('click', '.edit-button', function () {

        let data = table.row($(this).parents('tr')).data();

        $('#EditId').val(data.id);
        $('#EditCategoryName').val(data.categoryName);
        $('#editModal').modal('show');
        $("#categoryUpdateError").html("");
    });


    // update product


    $('#editForm').submit(function (event) {
        event.preventDefault();


        let categoryId = $('#EditId').val();
        let categoryName = $('#EditCategoryName').val();
        let viewmodel = {
            Id: categoryId,
            CategoryName: categoryName,
        };

        $.ajax({
            url: '/Category/Update',
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(viewmodel),
            success: function (response) {      
                if (response.success) {
                    $('#editModal').modal('hide');

                    let text = "Kategori update islemi başarılı.";
                    let icon = "success";
                   
                    showErrorToast(text, icon);
                    refreshDataTable(table);

                }
            },
            error: function (xhr, status, error) {
                console.log(xhr);
                $("#categoryUpdateError").html(xhr.responseText);
            }
        });

    });
    // kategoir ekleme

    $("#myForm2").submit(function (event) {
        event.preventDefault();
        let ctname = $("#category__name");
        let viewmodel = {
            CategoryName: ctname.val(),
        };

        $.ajax({
            url: "/Category/CreateCategory",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(viewmodel),
            success: function (data) {

                let text = "Kategori ekleme islemi başarılı.";
                let icon = "success";
                showErrorToast(text, icon);
                refreshDataTable(table);
                ctname.val("");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                let text = jqXHR.responseText
                let icon = "error";
                showErrorToast(text, icon);
            }
        });

    });


});



// fonksiyonlar 

    // sweet alert fonksiyonu
function showErrorToast(text,icon) {
    Swal.fire({
        position: 'middle',
        icon: icon,
        title: text,
        showConfirmButton: false,
        timer: 2500
    });
}


 // datatable refresh fonksiyonu
function refreshDataTable(table) {
    $.get('/Category/ListAll', function (data) {
        // DataTable'i yenile
        table.clear();
        table.rows.add(data);
        table.draw();
    });
}