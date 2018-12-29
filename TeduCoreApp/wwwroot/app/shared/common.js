var shareCommon = function () {
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
        try {
            var returnUrl = "/account/login.html?returnUrl=" + window.location.href;
            $(".aSendLoginGet").attr("href", returnUrl);
        }
        catch (error) {
           
        }
        
    }
}
