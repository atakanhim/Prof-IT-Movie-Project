// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var divOgesi = $(".y-sidebar");

const menubtn = $(".menu-btn");
let menuOpen = false;

menubtn.click(function () {
    if (!menuOpen) {
        menubtn.addClass("open");
        menuOpen = true;
    } else {
        menubtn.removeClass("open");
        menuOpen = false;
    }
    if (divOgesi.hasClass("show")) {
        divOgesi.removeClass("show");

    } else {
        divOgesi.addClass("show");

    }
});