$(document).ready(function () {
    var table = $('#metrics-datatable');
    $.ajax({
        url: '/Movie/GetLastMovies/5',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            // Pre-process the data if necessary
            var preProcessedData = data;

            // Pass the pre-processed data to the DataTable
            table = $('#metrics-datatable').DataTable({
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
                    { data: "MovieName" },
                    { data: "DirectorName" },
                    { data: "MovieLanguage" },
                    { data: "Ortalama" },
                    {
                        data: "Created", render: function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                var date = new Date(data);
                                return date.toLocaleDateString('tr-TR');
                            } else {
                                return data;
                            }
                        }
                    },
                  
                ]
            });
        },
        error: function (xhr, status, error) {
            let text = xhr.responseText
            let icon = "error";
            showErrorToast(text, icon);
        }
    });







    // toplam yorum sayisi
    $.get('/Comment/TotalCommentCount', function (data) {
        $('#totalComment').html(data.count);
    });
    // toplam film sayısını doner
    $.get('/Movie/GetCount', function (data) {
        $('#moviesCount').html(data.count); 
    });
    // toplam category sayısını doner
    $.get('/Category/GetCount', function (data) {
        $('#categoryCount').html(" " + data);
    });
    // toplam user sayısını doner
    $.get('/Home/GetUserCount', function (data) {
        $('#userCount').html(" "+data);
    });

    
    // en populer film
    $.get('/Movie/GetMostPopular/1', function (data) {
        data = JSON.parse(data);
       
        $('#mostPopulerMovieName').html(" "+data[0].MovieName);
        $('#mostPopulerMovieOrtalama').html(" " +data[0].Ortalama + "/10");

    });

    // en cok yorum alan film
    $.get('/Movie/GetMostCommentedMovie/1', function (data) {
        data = JSON.parse(data);
       
        $('#mostCommentedMovieName').html(" " +data[0].MovieName);
        $('#mostCommentedMovieCommentCount').html(" " +data[0].Comments.length+" yorum");
    });





    // tanımlı dil ve sayısını doner
    $.get('/Movie/GetLanguages', function (data) {

        var cdata1 = data[0] ? data[0] : { language: "Türkçe", count: 0 };
        var cdata2 = data[1] ? data[1] : { language: cdata1.language.trim() == "İngilizce" ? "Türkçe" : "İngilizce" , count: 0 };
  
        $('#languageH5').html(cdata1.language + " / " + cdata2.language  + " Diline Sahip Filmler");
        $('#languageOran').html(cdata1.count + " / " + cdata2.count);

        // sparkline
        $("#sparkline1").sparkline([cdata1.count, cdata2.count], {
            type: 'pie',
            height: '140',
            sliceColors: ['#1ab394', '#F5F5F5']
        });

    });

    $.get('/Movie/GetRatingAge', function (data) {

        $('#ratingAgeH5').html(data[0].ratingAge + " / " + data[1].ratingAge + " ");
        $('#ratingAgeOran').html(data[0].count + " / " + data[1].count);


        $("#sparkline2").sparkline([data[0].count, data[1].count], {
            type: 'pie',
            height: '140',
            sliceColors: ['#1ab394', '#F5F5F5']
        });
    });

    
});


