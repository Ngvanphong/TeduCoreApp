var checkoutController = function () {
    this.initilizer = function () {
        registerEvents();
        getShoppingCartForCheckout();
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
                $(".taxtransfer").text("20.000");
                setTimeout(getTotalPayment, 200);
            }
        });

        $("#districtSelectList").off('change').on('change', function (e) {
            e.preventDefault();
            var districtId = $(this).val();
            if (districtId != "") {
                getTaxForHCM(districtId);
                setTimeout(getTotalPayment, 200);
            }
            else {
                $(".taxtransfer").text("14.000");
                setTimeout(getTotalPayment, 200);
            }
        });
        $(".radioLogin").off('change').on('change', function (e) {
            e.preventDefault();
            var isLogin = $(this).val();
            getCustomerLogin(isLogin);
        });
        $('body').on('click', '.removeCheckout', function (e) {
            e.preventDefault();
            var productId = $(this).data('productid');
            var colorId = $(this).data('colorid');
            var sizeId = $(this).data('sizeid');
            removeShoppingCart(productId, colorId, sizeId);
        });
    }

    function loadDistrict(provinceId) {
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
                    $(".taxtransfer").text(taxTransfer);
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

    function getShoppingCartForCheckout() {
        var template = $('#productShoppingTable-template').html();
        var render = "";
        $.ajax({
            url: '/shopping/getall',
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                var totalMoneyShoppingCart = 0;
                $.each(response.Items, function (i, item) {
                    var salePrice;
                    if (item.ProductVm.PromotionPrice > 0) {
                        salePrice = item.ProductVm.PromotionPrice;
                    } else {
                        salePrice = item.ProductVm.Price;
                    };
                    totalMoneyShoppingCart = totalMoneyShoppingCart + salePrice * item.Quantity;
                    render += Mustache.render(template, {
                        Id: item.ProductVm.Id,
                        Name: item.ProductVm.Name,
                        SeoAlias: item.ProductVm.SeoAlias,
                        ThumbnailImage: item.ProductVm.ThumbnailImage,
                        Quantity: item.Quantity,
                        DomainApi: response.DomainApi,
                        ProductId: item.ProductId,
                        Size: item.SizeVm.Name,
                        Color: item.ColorVm.Name,
                        ColorId: item.ColorVm.Id,
                        SizeId: item.SizeVm.Id,
                        Price: $.number(item.ProductVm.Price, 3),
                        PromotionPrice: $.number(item.ProductVm.PromotionPrice, 3),
                        TotalPriceItem: $.number(salePrice * item.Quantity, 3),
                    });
                });
                if (render != '') {
                    $('#tableShoppingContent').html(render);
                    $('#totalMoneyShoppingCart').text(totalMoneyShoppingCart);
                }
                setTimeout(getTotalPayment, 200);
            }
        })
    }

    function getTotalPayment() {
        var totalMoneyShopping = $('#totalMoneyShoppingCart').text();
        var shipping = $('.taxtransfer').text().split(".",1);
        $('#totalCountPayment').text($.number(parseInt(shipping) + parseInt(totalMoneyShopping), 3));
    }

    function removeShoppingCart(productId, colorId, sizeId) {
        $.ajax({
            url: '/shopping/removeCartItem',
            data: {
                productId: productId,
                colorId: colorId,
                sizeId: sizeId,
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                getShoppingCartForCheckout();               
                shopingCarts.getShoppingCartToUpdate();                             
            }
        })
    }

    this.getShoppingCartForCheckoutUpdate=function() {
        var template = $('#productShoppingTable-template').html();
        var render = "";
        $.ajax({
            url: '/shopping/getall',
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                var totalMoneyShoppingCart = 0;
                $.each(response.Items, function (i, item) {
                    var salePrice;
                    if (item.ProductVm.PromotionPrice > 0) {
                        salePrice = item.ProductVm.PromotionPrice;
                    } else {
                        salePrice = item.ProductVm.Price;
                    };
                    totalMoneyShoppingCart = totalMoneyShoppingCart + salePrice * item.Quantity;
                    render += Mustache.render(template, {
                        Id: item.ProductVm.Id,
                        Name: item.ProductVm.Name,
                        SeoAlias: item.ProductVm.SeoAlias,
                        ThumbnailImage: item.ProductVm.ThumbnailImage,
                        Quantity: item.Quantity,
                        DomainApi: response.DomainApi,
                        ProductId: item.ProductId,
                        Size: item.SizeVm.Name,
                        Color: item.ColorVm.Name,
                        ColorId: item.ColorVm.Id,
                        SizeId: item.SizeVm.Id,
                        Price: $.number(item.ProductVm.Price, 3),
                        PromotionPrice: $.number(item.ProductVm.PromotionPrice, 3),
                        TotalPriceItem: $.number(salePrice * item.Quantity, 3),
                    });
                });
                if (render != '') {
                    $('#tableShoppingContent').html(render);
                    $('#totalMoneyShoppingCart').text(totalMoneyShoppingCart);
                }
                else {
                    $('#tableShoppingContent').html("");
                    $('#totalMoneyShoppingCart').text("");
                }
                setTimeout(getTotalPayment, 200);
            }
        })
    }
    
}