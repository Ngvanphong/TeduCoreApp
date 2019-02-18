var checkoutController = function () {
    var connections;
    this.initilizer = function () {
        registerEvents();
        getShoppingCartForCheckout();
        getSignalr();
        loadBalanceUser();
    };
    function registerEvents() {
        $("#formBillShopping").validate({
            rules: {
                name: "required",
                address: "required",
                email: {
                    email: true
                },
                mobile: "required",
                citySelectList: "required"
            },
            messages: {
                name: "Bạn phải nhập tên",
                address: "Bạn phải nhập địa chỉ",
                email: {
                    email: "Email không đúng"
                },
                mobile: "Bạn phải nhập số điện thoại",
                citySelectList: ""
            }
        });

        $('body').on('change', '#citySelectList', function (e) {
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

        $('body').on('change', '#districtSelectList', function (e) {
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

        $('body').on('change', '.radioLogin', function (e) {
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
        $('body').on('change', '.updatePriceShoppingCart', function (e) {
            e.preventDefault();
            var productId = $(this).data('productid');
            var colorId = $(this).data('colorid');
            var sizeId = $(this).data('sizeid');
            var quantity = $(this).val();
            updateShoppingCart(productId, colorId, sizeId, quantity);
        });

        $('body').on('click', '#addBillShopping', function (e) {
            e.preventDefault();
            $("#addBillShopping").attr('disabled', true);
            var valid = $("#formBillShopping").valid();
            var totalMoneyPayment = parseInt($('#totalCountPaymentForPrice').text());
            var totalMoneyOrder = $('#totalMoneyShoppingCart').text();
            var feeShiping = parseInt($('.taxtransfer').text().split(".", 1));
            if (valid && parseInt(totalMoneyOrder) > 0 && feeShiping > 0) {
                var billVm = {
                    CustomerName: $('#name').val(),
                    CustomerAddress: $('#address').val(),
                    CustomerMobile: $('#phoneNumber').val(),
                    CustomerEmail: $('#email').val(),
                    CustomerMessage: $('#note').val(),
                    PaymentMethod: $('#pamentMethod').val()
                };
                var balanceForBill = parseInt(parseInt(totalMoneyOrder) * 5 / 100);
                addCheackout(billVm, feeShiping, totalMoneyOrder, balanceForBill, totalMoneyPayment);
            }
            else {
                notifications.printSuccesError("Phí vận chuyển không hợp lệ hay nhập chưa đủ thông tin");
                $("#addBillShopping").attr('disabled', false);
            }
        });
    }

    function loadDistrict(provinceId) {
        $.ajax({
            url: "/checkout/loadDistrict",
            type: "POST",
            dataType: "json",
            data: {
                provinceId: provinceId
            },
            success: function (res) {
                if (res.status) {
                    var html = '<option value="">Chọn quận/huyện</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Id + '">' + item.Name + '</option>';
                    });
                    $("#districtSelectList").html(html);
                }
            }
        });
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
                        $("#totalBalance").text($.number(res.data.Balance, 3));
                        $("#totalBalanceForPrice").text(res.data.Balance);
                    }
                    else {
                        notifications.printSuccesMessage("Qúy khách chưa đăng nhập");
                    }
                }
            });
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
                    }
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
                        TotalPriceItem: $.number(salePrice * item.Quantity, 3)
                    });
                });
                if (render != '') {
                    $('#tableShoppingContent').html(render);
                    $('#totalMoneyShoppingCart').text(totalMoneyShoppingCart);
                }
                else {
                    $('#tableShoppingContent').html("");
                    $('#totalMoneyShoppingCart').text("0");
                }
                setTimeout(getTotalPayment, 200);
            }
        });
    }

    function getTotalPayment() {
        var totalMoneyShopping = parseInt($('#totalMoneyShoppingCart').text());
        var shipping = parseInt($('.taxtransfer').text().split(".", 1));
        var totalBalance = parseInt($('#totalBalanceForPrice').text());
        var totalMoneyPayment = 0;
        if (totalBalance > parseInt(totalMoneyShopping + shipping)) {
            totalMoneyPayment = 0;
        }
        else {
            totalMoneyPayment = totalMoneyShopping + shipping - totalBalance;
        }
        $('#totalCountPayment').text($.number(totalMoneyPayment, 3));
        $('#totalCountPaymentForPrice').text(totalMoneyPayment);
    }

    function removeShoppingCart(productId, colorId, sizeId) {
        $.ajax({
            url: '/shopping/removeCartItem',
            data: {
                productId: productId,
                colorId: colorId,
                sizeId: sizeId
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                getShoppingCartForCheckout();
                shopingCarts.getShoppingCartToUpdate();
            }
        });
    }

    this.getShoppingCartForCheckoutUpdate = function () {
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
                    }
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
                        TotalPriceItem: $.number(salePrice * item.Quantity, 3)
                    });
                });
                if (render != '') {
                    $('#tableShoppingContent').html(render);
                    $('#totalMoneyShoppingCart').text(totalMoneyShoppingCart);
                }
                else {
                    $('#tableShoppingContent').html("");
                    $('#totalMoneyShoppingCart').text("0");
                }
                setTimeout(getTotalPayment, 200);
            }
        });
    };

    function updateShoppingCart(productId, colorId, sizeId, quantity) {
        $.ajax({
            url: '/shopping/updateShopping',
            data: {
                productId: productId,
                colorId: colorId,
                sizeId: sizeId,
                quantity: quantity
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                getShoppingCartForCheckout();
                shopingCarts.getShoppingCartToUpdate();
            }
        });
    }

    function addCheackout(billVm, feeShiping, totalMoneyOrder, balanceForBill, totalMoneyPayment) {
        $.ajax({
            url: '/checkout.html',
            data: {
                billVm: billVm,
                feeShipping: feeShiping,
                totalMoneyOrder: totalMoneyOrder,
                totalMoneyPayment: totalMoneyPayment,
                balanceForBill: balanceForBill
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                if (res.status) {
                    connections.invoke("NewMessage", res.billVm).catch(function (err) {
                        return console.error(err.toString());
                    });
                    notifications.printSuccesMessage("Quý khách đã đặt thành công");
                    setTimeout(() => { window.location.href = "/index.html";}, 2000);
                }
                else {
                    notifications.printSuccesError("Quý khách chưa đặt thành công");
                    $("#addBillShopping").attr('disabled', false);
                }
            }
        });
    }

    function loadBalanceUser() {
        $.ajax({
            url: "account/checkIsLogin",
            type: "POST",
            dataType: "Json",
            success: function (res) {
                if (res.status) {
                    $("#totalBalance").text($.number(res.data.Balance, 3));
                    $("#totalBalanceForPrice").text(res.data.Balance);
                }
            }
        });
    }

    function getSignalr() {
        var domainApi = $('#domainApiSignalr').text();

        var connection = new signalR.HubConnectionBuilder().withUrl(domainApi + "/hub").build();

        connection.on("messageReceived", function (message) {
        });

        connection.start().catch(function (err) {
            return console.error(err.toString());
        });
        connections = connection;
    }
};