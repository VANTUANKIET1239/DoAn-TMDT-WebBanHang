// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const image = document.getElementById("hinhanh");
const input = document.getElementById("chonhinhanh");

input.addEventListener("change", () => {
    image.src = URL.createObjectURL(input.files[0]);
});