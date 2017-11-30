const PREFIX = "/api/ShoppingCartRest/";

$(function () {
    $("#sortable1, #sortable2").sortable({
        connectWith: ".connectedSortable"
    }).disableSelection();
});


let body = ""; 
$.ajax(PREFIX, {
    success: function (data) {
        $(data).each(function (key , value ) {
            console.log(value)
            body += "<li class='ui-state-default'>" + value.ProductName + " <button class'btnRemove'  id='" + value.Id + "' > Remove </button></li>";
            $("#sortable1").html(body);
        });
    },
    error: function () {
        // error goes 
    }
});





$("#btnRemove").on("click", (e) => {
    $.ajax(PREFIX + e.target.id, {
        type: 'DELETE',
        success: function (data) {
            $(data).each(function (key, value) {
                
            });
        },
        error: function () {
            // error goes 
        }
    });
});