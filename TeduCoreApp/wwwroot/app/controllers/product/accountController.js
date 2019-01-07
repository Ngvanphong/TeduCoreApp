var accountController = function () {
    this.initializers = function () {
        loadData();
    };
    function loadData() {
        $.ajax({
            type: 'GET',
            url: 'account/loadUserAccount',
            dataType: 'json',
            success: function (response) {                
                $("#id").val(response.data.Id);
                $("#email").val(response.data.Email);
                $("#phonenumber").val(response.data.PhoneNumber);
                $("#fullname").val(response.data.FullName);
                $("#balanceMoney").text($.number(response.data.Balance, 3));
            }
        });
    }
}