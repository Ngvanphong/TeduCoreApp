var productController = function () {
    this.initializers = function () {
        loadData(false);     
        registerEvents();
    }
    registerEvents = function () {
        $('#input-limit').on('change', function () {
            pageSize = $(this).val();
            pageIndex = 1;
            wrapPaging(totalRowsPaging, function () { loadData(true) },true)
        });
        $('#input-sort').on('change', function () {
            pageIndex = 1;
            flagPaging = false;
            if (flagPaging == false) {
                loadData(true)
            };            
        });
    }

    var totalRowsPaging;
    var pageIndex = 1;
    var pageSize = $('#input-limit').val();
    var flagPaging = false;
    loadData = function (isPageChanged) {
        var template = $('#productContent-template').html();
        var render = "";
        $.ajax({
            type: 'GET',
            data: {
                id: $('#productCategoryId').text(),
                sort: $('#input-sort').val(),
                page: pageIndex,
                pageSize: pageSize,
            },
            url: '/product/getProductByCategory',
            dataType: 'json',
            success: function (response) {  
                totalRowsPaging = response.TotalRows;               
                $.each(response.Items, function (i, item) {
                    render += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        SeoAlias: item.SeoAlias,
                        ThumbnailImage:item.ThumbnailImage,
                        PromotionPrice:item.PromotionPrice,
                        Price:item.Price,
                    });
                });
                if (render != '') {
                    $('#productContent').html(render);
                } 
                if (flagPaging == false) {                 
                    wrapPaging(response.TotalRows, function () {                        
                        loadData();
                    }, isPageChanged);
                }
               
            },
        })
    }

    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',           
            onPageClick: function (event, p) {
                pageIndex = p;
                if (flagPaging == false) {
                    flagPaging = true;
                }
                else {
                    setTimeout(callBack(), 200);
                }
                
            }
        });
    }



}
