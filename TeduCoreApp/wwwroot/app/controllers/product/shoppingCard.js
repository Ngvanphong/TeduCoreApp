var shoppingCard = function () {
    this.initializer = function () {
        registerEvents();
        getShoppingCart();
    };
    registerEvents = function () {
        $('body').on('click', '#btnShoppingCard', function (e) {
            e.preventDefault();
            var productId = $("#btnShoppingCard").data('id');
            var colorId = $("#productColor").val();
            var sizeId = $("#productSize").val();
            var quantity = $("#qty").val();
            addToCard(productId, colorId, sizeId, quantity);
        });
        $('body').on('click', '.removeForIcon', function (e) {
            e.preventDefault();
            var productId = $(this).data('productid');
            var colorId = $(this).data('colorid');
            var sizeId = $(this).data('sizeid');
            removeShoppingCart(productId, colorId, sizeId);
        });

    };

    function addToCard(productId, colorId, sizeId, quatity) {
        $.ajax({
            url: '/shopping/addToCart',
            data: {
                productId: productId,
                colorId: colorId,
                sizeId: sizeId,
                quantity: quatity
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                notifications.printSuccesMessage("Đã thêm sản phẩm vào giỏ hàng");
                getShoppingCart();
            }
        });
    }

    function getShoppingCart() {
        var template = $('#productShopping-template').html();
        var render = "";
        $.ajax({
            url: '/shopping/getall',
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                $(".cart_count").text(response.CountProduct);
                $.each(response.Items, function (i, item) {
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
                        SizeId: item.SizeVm.Id

                    });
                });
                if (render != '') {
                    $('#cart-sidebar').html(render);
                }
                else {
                    $('#cart-sidebar').html("<strong>CHƯA CÓ SẢN PHẨM NÀO!</strong>");
                }
            }
        });
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
                getShoppingCart();
                try {
                    productControllers.getShoppingCartForPageToUpdate();
                }
                catch (error) { console.log(error); }
                try {
                    checkoutControllers.getShoppingCartForCheckoutUpdate();
                }
                catch (error) { console.log(error); }
            }
        });
    }

    this.getShoppingCartToUpdate = function () {
        var template = $('#productShopping-template').html();
        var render = "";
        $.ajax({
            url: '/shopping/getall',
            dataType: 'json',
            type: 'GET',
            success: function (response) {
                $(".cart_count").text(response.CountProduct);
                $.each(response.Items, function (i, item) {
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
                        SizeId: item.SizeVm.Id

                    });
                });
                if (render != '') {
                    $('#cart-sidebar').html(render);
                }
                else {
                    $('#cart-sidebar').html("<strong>CHƯA CÓ SẢN PHẨM NÀO!</strong>");
                }
            }
        });
    };

};