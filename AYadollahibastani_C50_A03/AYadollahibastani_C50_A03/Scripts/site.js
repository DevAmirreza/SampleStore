//Author: Amirreza Yadollahibastani
//Professor: Richard Chan 
//Date: 11-Nov-2017


//Load body in slow mow 
$("body").fadeIn(200);


//Duplicate Validation 
$(".unique").on("focusout", () => {
    $.post("/ShoppingCart/IsDuplicate",
    {
        name: $(".unique").val(),
    },
    function (data, status) {

        if ($(".unique").val().length == 0) {
            $(".unique").CSS({
                "background-color": "white",
                "color": "black"
            });
        }
        //if product name already list duplicate name 
        if (data == true) {

            $(".productNameError").text("Product Name Already Exist")

            $(".unique").css({
                "background-color": "#EF4836",
                "color": "white"
            });
        } else {
            $(".unique").css({
                "background-color": "#2ECC71",
                "color": "black"
            });
        }
    });

});



//Ajax load create button on page 
$(".btnCreate").load("/ShoppingCart/CreateButton").fadeIn(4000);
$(".btnDelete").on("click", $(".btnDelete"), (el) => {
    $.get("/ShoppingCart/Delete/" + $(el.target).val(), (data, status) => {
        $("#deleteConfirmation").fadeIn(800);
        $("#deleteConfirmation").html(data);
    });
});


//Sends ajax to get content of shopping cart contents 
$('#popoverData').popover(() => { });
$("#popoverData").on("mouseover", () => {
    $.post("/ShoppingCart/ProfileJson/", (data, status) => {
        let info = "";
        for (let i = 0 ; i < data.length ; i++) {
            info += "<div class='product'><h4><span class='productQuan'>" + data[i].Quantity + "</span> <span class='productName'>" + data[i].ProductName + "</span></h4></div>  ";
        }

        $("#popoverData").attr("data-content", info);

    });
});