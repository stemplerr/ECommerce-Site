$(function () {
    $("#price-textbox").keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {    // if shift, ctrl or alt keys held down
            e.preventDefault();                      // Prevent character input
        } else {
            var n = e.keyCode;
            if (!((n == 8)                        // backspace
                    || (n == 46)                 // delete
                    || (n >= 35 && n <= 40)     // arrow keys/home/end
                    || (n >= 48 && n <= 57)    // numbers on keyboard
                    || (n >= 96 && n <= 105)  // number on keypad
                    || (n == 190))           // period 
                    ) {
                e.preventDefault();        // Prevent character input
            }
        }
        //How to prevent 2 periods?
        //($('#price-textbox').Index(".") != -1)
    });
    $('.add-to-cart-button').on('click', function () {
        var productid = $(this).data('product-id');
        $.post("/home/AddToCart", { productid: productid }, function () {
            $('.alert-success').removeClass('hide');
                var count = $('#shopping-cart-count').text();
                count++;
                $('#shopping-cart-count').text(count);
            });
        
    });
    $('.close').on('click', function () {
        $('.alert-success').addClass('hide');
    });
});

