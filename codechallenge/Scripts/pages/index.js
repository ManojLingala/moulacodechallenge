
//initialize component
var initSettings = function () {
    $('.DatePicker').datetimepicker({
        format: 'L'
    });
}


var resetCustmerForm = function () {
    $('#mod-customer-title').html('Add New Customer');
    $('#mod-customer .form-control, #hdnCustomerID, #hdnDelCustomerID').val('');
}


var alertNotify = function (msg, type) {
    $.notify({
        message: msg
    }, {
            type: type,
            delay: 50,
            timer:2000,
            z_index: 99999,
        });
}

var customerList = [];

//Function to load customer Data 
var refreshData = function () {
    $.ajax({
        type: "GET",
        url: '/Home/getCustomers',
        data: {},
        dataType: 'json',
        contentType: 'application/json'
    }).done(function (data) {
        $('#tbCustomers').html('');
        customerList = data.customers;
        if (!customerList.length)
            $('#tbCustomers').html('<tr><td colspan="5"><div class="alert alert-info">There is no data found</div></td></tr>');
        $.each(customerList, function (index, row) {
            $('#tbCustomers').append('<tr>\
                        <td>' + row.FullName + '</td>\
                        <td>' + row.Email + '</td>\
                        <td>' + row.BirthDate + '</td>\
                        <td>'+ row.CustomerCode + '</td>\
                        <td class="text-right">\
                            <button type="button" class="btn btn-xs btn-info btn-edit" data-id="' + row.CustomerID + '"><span class="glyphicon glyphicon-edit"></span></button>\
                            <button type="button" class="btn btn-xs btn-danger btn-delete" data-id="' + row.CustomerID + '"><span class="glyphicon glyphicon-trash"></span></button>\
                        </td>\
                    </tr>');
        });
    }).error(function (jqXHR, textStatus, errorThrown) {
    });
}


//After Saving
var successSave = function (result) {
    if (result.status == "error") {
        alertNotify(result.msg, "danger");
        return;
    }
    else
        alertNotify("Customer was saved successfully..", "success");

    resetCustmerForm();
    $('#mod-customer').modal('hide');
    refreshData();
}

//After Deleting
var successDelete = function (result) {
    if (result.status == "error") {
        alertNotify(result.msg, "danger");
        return;
    }
    else
        alertNotify("Customer was deleted successfully..", "success");
    resetCustmerForm();
    $('#delfultrackconfirm').modal('hide');
    refreshData();
}

//On Ajax Error
var errorAjax = function (xhr) {

    alertNotify(xhr.responseText, "danger");
}

$(document).ready(function () {
    initSettings();
    refreshData();


    $('.btnRefreshData').click(function () {
        refreshData();
    });
    //Open Add Customer Form
    $('.btnAddCustomer').click(function () {
        resetCustmerForm();
        $('#mod-customer').modal('show');
    });

    //Open Customer Form for Edit
    $(document).on('click', '.btn-edit', function () {
        resetCustmerForm();
        var id = $(this).data("id");
        var cust = $.grep(customerList, function (obj) {
            return obj.CustomerID == id;
        })[0];
        $('#mod-customer-title').html('Edit Customer: ' + cust.FullName);
        $('#hdnCustomerID').val(id);
        $('#txtFirstName').val(cust.FirstName);
        $('#txtLastName').val(cust.LastName);
        $('#txtEmail').val(cust.Email);
        $('#txtBirthDate').val(cust.BirthDate);
        $('#mod-customer').modal('show');
    });
    
    //Open Delete Dialog
    $(document).on('click', '.btn-delete', function () {
        //determined id of record
        var id = $(this).data("id");
        $('#hdnDelCustomerID').val(id);

        // show delete dialog confirmation 
        $('#delfultrackconfirm').modal({ backdrop: 'static', keyboard: false });
    });

});