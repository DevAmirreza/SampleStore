$(".unique").on("keydown", () => {
    $.post("/ShoppingCart/IsDuplicate",
    {
        name: $(".unique").val(),
    },
    function (data, status) {

        if (data == true) {
            $(".unique").css({
                "background-color": "red",
                "color": "white"
            });
        } else  {
            $(".unique").css({
                "background-color": "green",
                "color": "black"
            });
        }

        if ($(".unique").val() == "") {
            $(".unique").CSS({
                "background-color": "white",
                "color": "black"
            });
        }

       
    });

});



$(".btnCreate").load("/ShoppingCart/CreateButton");

$(".btnDelete").on("click",  $(".btnDelete") , (el)=> {
    $.get("/ShoppingCart/Delete/" + $(el.target).val(), (data, status) => {
        $("#deleteConfirmation").html(data);
    });
});



$('#popoverData').popover(() => {});

$("#popoverData").on("mouseover", () => {

    $.post("/ShoppingCart/ProfileJson/", (data, status) => {
        let info = "";
        for (let i = 0 ; i < data.length ; i++) {
            info += data[i].Quantity + " -  " + data[i].ProductName; 
        }
        
       $("#popoverData").attr("data-content", info);

    });

  


});