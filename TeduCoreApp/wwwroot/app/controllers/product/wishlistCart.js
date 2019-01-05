var wistlistCartController = function () {
    this.initilizer = function () {
        registerEvents();
    };
    registerEvents = function () {
        $('body').on('click', '.btnWistlistAddItem', function (e) {
            e.preventDefault();
            var productId = $(this).data('productid');           
            addwhistToItem(productId);
        });
        $('body').on('click', '.removeWisglistIeem', function (e) {
            e.preventDefault();
            var productId = $(this).data('productid');
           removeWishlistItem(productId);
        });
    };

    function removeWishlistItem(productId) {
        $.ajax({
            url: '/wishlist/removeItem',
            data: {
                productId: productId
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                window.location.href = '/wishlist.html';
            }
        });
    }
    function addwhistToItem(productId) {
        $.ajax({
            url: '/wishlist/addItem',
            data: {
                productId: productId
            },
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                notifications.printSuccesMessage("Đã thêm vào mục yêu thích");
            }
        });
    }
};