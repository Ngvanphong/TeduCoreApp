var checkoutController = function () {
    this.initilizer = function () {
        registerEvents();
    }
    function registerEvents() {

        $("#formBillShopping").validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    email: true,
                },
                mobile: "required",
                citySelectList: "required",

            },
            messages: {
                name: "Bạn phải nhập tên",
                address: "Bạn phải nhập địa chỉ",
                email: {
                    email: "Email không đúng"
                },
                mobile: "Bạn phải nhập số điện thoại",
                citySelectList: "",
            }
        });

        $("#addBillShopping").off('click').on('click', function (e) {
            e.preventDefault();
            var valid = $("#formBillShopping").valid();
           
        });

        $("#citySelectList").off('change').on('change', function (e) {
            e.preventDefault();
            var province = $("#citySelectList").val();
            if (province == 701) {
                $("#proviceDisplay").show();
                loadDistrict(province);
            }
            else {
                $("#proviceDisplay").hide();
                $("#taxtransfer").text("20.000 VND");
            }

        });

        $("#districtSelectList").off('change').on('change', function (e) {
            e.preventDefault();

            var districtId = $(this).val();
            if (districtId != "") {
               getTaxForHCM(districtId);
            }
            else {
                $("#taxtransfer").text("14.000 VND");
            }
        });
        $(".radioLogin").off('change').on('change', function (e) {
            e.preventDefault();
            var isLogin = $(this).val();
            getCustomerLogin(isLogin);
        });

    }

    function loadDistrict (provinceId) {
        $.ajax({
            url: "/checkout/loadDistrict",
            type: "POST",
            dataType: "json",
            data: {
                provinceId: provinceId,
            },
            success: function (res) {
                if (res.status) {
                    var html = '<option value="">Chọn quận/huyện</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Id + '">' + item.Name + '</option>'
                    });
                    $("#districtSelectList").html(html);
                }
            }

        })

    }

    function getTaxForHCM(districtId) {
        $.ajax({
            url: "/checkout/GetTaxHCM",
            type: "POST",
            dataType: "Json",
            data: {
                districtId: districtId
            },
            success: function (res) {
                if (res.status) {
                    var taxTransfer = res.data;
                    $("#taxtransfer").text(taxTransfer);
                }
            }

        });
    }

    function getCustomerLogin(isLogin) {
        if (isLogin == 1) {
            $.ajax({
                url: "account/checkIsLogin",
                type: "POST",
                dataType: "Json",

                success: function (res) {
                    if (res.status) {
                        $("#name").val(res.data.FullName);
                        $("#phoneNumber").val(res.data.PhoneNumber);
                        $("#email").val(res.data.Email);
                    }
                    else {                       
                        notifications.printSuccesMessage("Bạn chưa đăng nhập");
                    }
                }

            })
        }
        else {
            $("#name").val("");
            $("#phoneNumber").val("");
            $("#email").val("");
        }
    }

    
}