
$(document).ready(function () {
    $.get("https://localhost:7269/Category/AllCategories", function (data) {
        var select = $(".chosen-select");

        $.each(data, function (index, category) {
            select.append('<option value="' + category.id + '">' + category.categoryName + '</option>');
        });
        select.trigger("chosen:updated");
    });
    $('.chosen-select').chosen();
    $("#movieForm").validate({
        errorClass: "text-danger",
        errorElement: "small",
        rules: {
            "MovieName": {
                required: true
            },
            "MovieSummary": {
                required: true
            },
            "DirectorName": {
                required: true
            },
            "RatingAge": {
                required: true
            },
            "PublishYear": {
                required: true
            },
            "Photo": {
                required: true
            },
            "ImdbUrl": {
                required: true,
                url: true
            },
            "MovieLanguage": {
                required: true
            },
            "CategoriesId": {
                required: true,
                minlength: 1
            }
        },
        messages: {
            "MovieName": {
                required: "Bu alan zorunludur."
            },
            "MovieSummary": {
                required: "Bu alan zorunludur."
            },
            "DirectorName": {
                required: "Bu alan zorunludur."
            },
            "RatingAge": {
                required: "Bu alan zorunludur."
            },
            "PublishYear": {
                required: "Bu alan zorunludur."
            },
            "Photo": {
                required: "Bu alan zorunludur."
            },
            "ImdbUrl": {
                required: "Bu alan zorunludur.",
                url: "Adres bilgisi uygun formatta girilmelidir."
            },
            "MovieLanguage": {
                required: "Bu alan zorunludur."
            },
            "CategoriesId": {
                required: "Bu alan zorunludur.",
                minlength: "Bu alan zorunludur."
            }
        }
    });



    $("form").on("submit", function (e) {
        if (Array.from(document.querySelectorAll('select[name="CategoriesId"] option:checked')).map(option => option.value).length == 0) {
            e.preventDefault();
            $("#errorSelect").text("Bu alan zorunludur.");
        }
    });




});





