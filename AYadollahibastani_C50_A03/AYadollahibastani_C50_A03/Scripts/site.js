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



//$(".btnCreate").load("/ShoppingCart/CreateButton");
$(".btnDelete").on("click", ()=> {
    console.log($(this).val());
    //$.post("/ShoppingCart/Delete/" + $(this).val() ,(data,status)=>{
    //    console.log(data);
    //});
});
