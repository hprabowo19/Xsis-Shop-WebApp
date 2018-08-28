$('#CreateOrderItem').click( function () {
    var id = $('#OrderNumber').val();
    $.ajax({
        type: "GET",
        url: '/OrderItem/Create',
        contentType: "application/json; charset=utf-8",
        data: { "Id": id },
        datatype: "json",
        success: function (data) {
            $('.OrderItemModalContent').html(data);
            $('#OrderItemModal').on('shown.bs.modal', function () {
                $('.chosen', this).chosen({ width: "100%" });
            });
            $('#OrderItemModal').modal('show');
        }
    })
});

$('#AddItem').click(function () {
    var res = validate();
    if (res == false) {
        return false;
    }
    var OrderItemObj = {
        ProductId: $('#ProductId').val(),
        Quantity: $('#Quantity').val(),
    };
    $.ajax({
        type: "POST",
        url: '/OrderItem/Add',
        data: JSON.stringify(OrderItemObj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            GetListOrderItem();
            Success("Item telah ditambahkan.");
            $('#OrderItemModal').modal('hide');
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
});

function Delete(Id) {
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to delete this Record?",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel plx!",
        confirmButtonColor: 'Gray',
        cancelButtonColor: '#d33',
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: "/OrderItem/RemoveItem/" + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    GetListOrderItem();
                    Success("Item telah dihapus.");
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
            swal("Deleted!", "Your record has been deleted.", "success");
        } else {
            swal("Cancelled", "Your record is safe :)", "error");
        }
    });
};

function GetListOrderItem() {
    $.ajax({
        type: "GET",
        url: "/OrderItem/Get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var ListOrderItem = '';
            var totalAmount = 0;
            var i = 0;
            $.each(response, function (key, item) {
                i++;
                ListOrderItem += '<tr>';
                ListOrderItem += '<td></td>';
                ListOrderItem += '<td>' + i + '</td>';
                ListOrderItem += '<td>' + item.ProductName + '</td>';
                ListOrderItem += '<td>' + item.UnitPrice + '</td>';
                ListOrderItem += '<td>' + item.Quantity + '</td>';
                ListOrderItem += '<td>' + item.Price + '</td>';
                ListOrderItem += '<td><a href="#" class="btn btn-danger btn-icon" id="test" onclick="Delete(' + item.Id + ')"><i class="zmdi zmdi-delete"></i></a></td>';
                ListOrderItem += '</tr>';
                totalAmount += item.Price;
            });
            $('.tbody').html(ListOrderItem);
            $('#TotalAmount').val(totalAmount).focus();
        },
        error: function (response) {
            alert(response.responseText);
        }

    });
}

function validate() {
    var isValid = true;
    if ($('#ProductId').val().trim() == "") {
        $('.validProductId').html('Harus memilih produk terlebih dahulu');
        Danger('Harus memilih produk terlebih dahulu.');
        isValid = false;
    } else {
        $('.validProductId').html('');
    }
    if ($('#Quantity').val().trim() == "") {
        $('#Quantity').val(1).focusin().focusout();
    } else if ($('#Quantity').val() <= 0) {
        $('.validQuantity').html('Quantity harus lebih dari 0.');
        Danger('Quantity harus lebih dari 0.');
        isValid = false;
    }
    return isValid;
}

function setDateTime() {
    var d = new Date(),
        dd = (d.getDate() < 10 ? '0' : '') + d.getDate(),
        mm = ((d.getMonth() + 1) < 10 ? '0' : '') + (d.getMonth() + 1),
        yy = d.getFullYear();
    $('#OrderDate').val(mm + '/' + dd + '/' + yy).focus();
}