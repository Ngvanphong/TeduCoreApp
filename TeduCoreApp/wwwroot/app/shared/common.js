﻿var shareCommon = function () {
    this.initilizer = function () {
        registerEvents();
        loadReturnUrl();
    }
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
            })
        })      
    } 
    function loadReturnUrl() {
        var returnUrl = "/account/login.html?returnUrl="+window.location.href;
        $(".aSendLoginGet").attr("href", returnUrl);
    }
}

$(document).ajaxSend(function (e, xhr, options) {
    if (options.type.toUpperCase() == "POST" || options.type.toUpperCase() == "PUT") {
        var token = $('form').find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});