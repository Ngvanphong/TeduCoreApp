var shareCommon = function () {
    this.initilizer = function () {
        registerEvents();
        loadReturnUrl();
        searchProduct();
        chatOnline();
    };
    function registerEvents() {
        $('body').on('click', '#logoutPost', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/account/logout.html',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status) {
                        window.location.href = '/index.html';
                    }
                }
            });
        });

        $('body').on('click', '#btnSearchProduct', function (e) {
            $("#search_mini_form").submit();
        });

        $('body').on('click', '#btnSubcribleEmail', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/subcrible/add',
                dataType: 'json',
                data: {
                    email: $("#emailSucbrile").val()
                },
                type: 'POST',
                success: function (res) {
                    if (res.status) {
                        notifications.printSuccesMessage("Cảm ơn quý khách đã đăng ký");
                    }
                    else {
                        notifications.printSuccesError("Email bị lỗi hoặc đã tồn tại");
                    }
                }
            });
        });
    }
    function loadReturnUrl() {
        try {
            var returnUrl = "/account/login.html?returnUrl=" + window.location.href;
            $(".aSendLoginGet").attr("href", returnUrl);
        }
        catch (error) {
            console.log(error);
        }
    }

    function searchProduct() {
        $("#search").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/product/search",
                    dataType: "json",
                    type: "GET",
                    data: {
                        term: request.term
                    },
                    success: function (data) {
                        response(data.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#search").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#search").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.label + "</div>")
                    .appendTo(ul);
            };
    }
    function chatOnline() {
        var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
        (function () {
            var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
            s1.async = true;
            s1.src = 'https://embed.tawk.to/5c5a734c7cf662208c943db5/default';
            s1.charset = 'UTF-8';
            s1.setAttribute('crossorigin', '*');
            s0.parentNode.insertBefore(s1, s0);
        })();     
    }
};