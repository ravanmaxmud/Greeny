$(document).on("click", ".add-product-to-basket-btn", function (e) {
    e.preventDefault();

    let aHref = e.target.href;
    console.log(aHref)

    $.ajax(
        {
            type: "POST",
            url: aHref,
            success: function (response) {
                console.log(response)
                $('.cart-block').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);
             
            }

        });
})

$(document).on("click", ".remove-product-to-basket-btn", function (e) {
    e.preventDefault();
    console.log(e.target.parentElement.previousElementSibling)
    var url = e.target.parentElement.previousElementSibling.href
    console.log(url)

    $.ajax(
        {
            type: "POST",
            url: url,
            success: function (response) {
                console.log(response)
                $('.cart-block').html(response);


            },
            error: function (err) {
                $(".product-details-modal").html(err.responseText);

            }

        });
})

$(document).on("click", ".remove-product-to-basket-page", function (e) {
    e.preventDefault();

    var aHref = e.target.parentElement.href;


    console.log(e.target.parentElement.href)
    fetch(e.target.parentElement.href)
        .then(response => response.text())
        .then(data => {
            $('.basket-block').html(data);


            fetch(e.target.parentElement.nextElementSibling.href)
                .then(response => response.text())
                .then(data => {
                    console.log(data)
                    $('.cart-block').html(data);
                })
        })
})