$(document).ready(function () {
            var usersTable = $('.footable').footable();
            

            $.get("https://localhost:7269/Inspinia/Admin/GetUsersList", function(data) {
                
               
                $("#spinner").toggleClass("spinner");
                $.each(data, function(index, value) {
                    var tr = $("<tr>");
                    tr.append($("<td>").text(value.userName));
                    tr.append($("<td>").text("Aktif"));
                    tr.append($("<td>").text(value.email));
                    tr.append($("<td>").text(value.roles));
                    tr.append($("<td>").text(value.birthDate));
                    tr.append($("<td>").text(value.phoneNumber));
                    tr.append($("<td>").text(value.isTwoFactorEnabled ? "Açık" : "Kapalı"));
                    tr.append($('<td>').append(
                      $('<div>').addClass('dropdown').append(
                        $('<button>').addClass('btn btn-sm btn-outline-primary dropdown-toggle')
  .attr('type', 'button')
  .attr('data-toggle', 'dropdown')
  .attr('aria-haspopup', 'true')
  .attr('aria-expanded', 'false')
  .html('<i class="fa fa-cog"></i> İşlemler'),
                          $('<div>').addClass('dropdown-menu dropdown-menu-right').append(
  $('<a>').addClass('dropdown-item').attr('href', '#').attr("data-id", value.id).attr("data-role", value.roles).addClass("updateRole").attr("data-username", value.userName).html('Rol Güncelle')
)
                      )
                    ));
                    $("#users").append(tr);
                });

                usersTable.trigger('footable_redraw');
            });

            $('#search-bar').on('input', function(e) {
                e.preventDefault();
                var filter = $(this).val();
                usersTable.trigger('footable_filter', {filter: filter, column: 0});
            });

            $('.dropdown-menu a').click(function(e) {
            e.preventDefault();
            var role = $(this).data('role');
            if (!role) {
                $('#users tr').show();
            } else {
                $('#users tr').each(function() {
                    var roles = $(this).find('td:nth-child(4)').text();
                    if (roles.indexOf(role) > -1) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            }
            });

            // Modal açıldığında seçilen rolün değişikliklerini takip etmek için event listener ekleniyor
            $('#roleModal input[name="role"]').on('change', function() {
                $('#saveRoleBtn').prop('disabled', false); // Kaydet butonunu etkinleştirme
            });


            $(document).on("click", ".updateRole", function (e) {
            e.preventDefault();
            var userId = $(this).data('id');
            var userRole = $(this).data('role');
            var userName = $(this).data("username");
            $('#userId').val(userId); // Gizli input alanına userId değerini set ediyoruz
            $('#updateRoleUsername').html(userName);
            $('input[name="role"]').removeAttr('checked');
            $('input[name="role"][value="' + userRole + '"]').prop('checked', true);
             $('#saveRoleBtn').prop('disabled', true); // Kaydet butonunu devre dışı bırakma
            $('#roleModal').modal('show'); // Modalı açıyoruz
        });
        $(document).on("click", "#saveRoleBtn", function () {
        
    var userId = $('#userId').val(); // Gizli input alanındaki userId değerini alıyoruz
    var roles = [];
    $('input[name=role]:checked').each(function() {
        roles.push($(this).val());
    }); // Seçilen rolü alıyoruz
    var requestData = JSON.stringify({ userId: userId, roles: roles });
    // POST isteği ile verileri gönderiyoruz
    $.ajax({
        url: "https://localhost:7269/Inspinia/Admin/UpdateUserRoles",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: requestData,
        success: function(response) {
            // Başarılı durumu
            $('#roleModal').modal('hide');
             //var rolesCell = $('#users tr[data-id="' + requestData.userId + '"] td:nth-child(4)');
             var row = $('[data-id="' + userId + '"]').closest('tr');
             var roleUpdateButton = $('[data-id="' + userId + '"]').data('role', roles.join(', '));
            // Satırdaki rolü alın
            var role = row.find('td:nth-child(4)');
             var rolesCell = role;
             console.log(rolesCell.text());
             rolesCell.text(roles.join(', '));
             usersTable.trigger('footable_redraw');
            // SweetAlert ile başarılı mesajını gösterme
            Swal.fire(
              'İşlem Başarılı!',
              'Rol güncelleme başarılı.',
              'success'
            )
        },
        error: function() {
            // Başarısız durumu
            // SweetAlert ile hata mesajını gösterme
            Swal.fire(
              'İşlem Başarısız.',
              'Rol güncelleme işlemi sırasında bir hata meydana geldi.',
              'error'
            )
        }
    });

    
    
    $('#roleModal').modal('hide'); // Modal


        });
        });