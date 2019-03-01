var productDetailController = function () {
    this.initializer = function () {
        loadSize();
        regiterEvents();       
    };
    regiterEvents = function () {
        $('body').on('change','#productColor', function (e) {
            e.preventDefault();
            loadSize();           
        });
    };
    loadSize = function () {         
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                productid: $('#productSize').data('productidsize'),
                colorid: $('#productColor').val()
            },
            url: '/product/getsizebycolor',
            dataType: 'json',
            success: function (response) {
                $.each(response, function (i, item) {                
                    render += '<option value="' + item.Id + '">' + item.Name + '</option>';                               
                });
                $("#productSize").html(render);
                setTimeout(hideSize, 100);
                setTimeout(disableButtonAdd, 200);
                if (render == '') {
                    notifications.printSuccesMessage("Mời quý khách chọn lại màu");
                }
            }
            
        });
    };

    function disableButtonAdd() {
        var sizeId = $("#productSize").val();
        var colorId = $("#productColor").val();
        if (sizeId != null && sizeId != undefined && colorId != null && colorId != undefined) {
            $("#btnShoppingCard").attr('disabled', false);
        }
        else {
            $("#btnShoppingCard").attr('disabled', true);
        }
    }
    function hideSize() {
        var sizeName = $("#productSize").text();
        if (sizeName == "HideSize") {
            $('#productSize').hide();
        }
    }

};