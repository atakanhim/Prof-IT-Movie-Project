$(document).ready(function () {
    $.get("https://localhost:7269/Inspinia/Admin/GetRoles", function (data) {
        var select = $("#role");

        $.each(data, function (index, role) {
            select.append('<option data-id="' + role.id + '">' + role.name + '</option>');
        });
    });

    $('#form').validate({
        errorClass: "text-danger",
        errorElement: "small",
        rules: {
            "username": {
                required: true
            },
            "birthdate": {
                required: true
            },
            "role": {
                required: true
            },
            "email": {
                required: true,
                email: true
            },
            "password": {
                required: true,
                minlength: 6,
                maxlength: 100
            },
            "confirm-password": {
                required: true,
                equalTo: '#password'
            }
        },
        messages: {
            "username": {
                required: 'Kullanıcı adı zorunlu bir alandır.'
            },
            "birthdate": {
                required: 'Doğum tarihi zorunlu bir alandır.'
            },
            "role": {
                required: 'Yetki seçimi zorunlu bir alandır.'
            },
            "email": {
                required: 'E-posta zorunlu bir alandır.',
                email: 'Lütfen geçerli bir e-posta adresi girin.'
            },
            "password": {
                required: 'Şifre zorunlu bir alandır.',
                minlength: 'Şifreniz en az 6 karakter olmalıdır.',
                maxlength: 'Şifreniz en fazla 100 karakter olabilir.'
            },
            'confirm-password': {
                required: 'Şifre tekrarı zorunlu bir alandır.',
                equalTo: 'Şifreler eşleşmiyor.'
            }
        },submitHandler: function (form) {
            // Form verilerini topla
            var formData = {
                UserName: $('#username').val(),
                BirthDate: $('#birthdate').val(),
                RoleId: $('#role').val(),
                Email: $('#email').val(),
                Password: $('#password').val(),
                ConfirmPassword: $('#confirm-password').val(),

            };

            // AJAX isteği gönder
            $.ajax({
                type: 'POST',
                url: 'https://localhost:7269/Inspinia/Admin/AddUserByAdmin',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(formData),
                success: function (data) {
                    swal("İşlem başarılı!", formData.UserName + " isimli kullanıcı eklendi.", "success");
                    $('#username').val('');
                    $('#birthdate').val('');
                    $('#role').val('');
                    $('#email').val('');
                    $('#password').val('');
                    $('#confirm-password').val('');
                },
                error: function (error) {
                    swal("İşlem başarısız!", formData.UserName + " isimli kullanıcı eklenemedi.", "error");
                    console.log(error);
                }
            });
            return false; // sayfa yenilenmesini önlemek için false döndürülür
        }
    });



});