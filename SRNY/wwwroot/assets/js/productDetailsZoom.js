var mainImage = document.getElementById("details-main-img");
var zoomContainer = document.getElementById("details-img-zoom");
var imgDimentions = mainImage.getBoundingClientRect();

mainImage.onmouseenter = function () {
    mainImage.style.width = "100%";
    mainImage.style.height = "100%";
    mainImage.onmousemove = function (event) {
        var mousePositionX = event.pageX - imgDimentions.left;
        var mousePositionY = event.pageY - imgDimentions.top;
        if (mousePositionY < 900) {
            zoomContainer.style.marginTop = "-" + mousePositionY + "px";
        }
        else {
            zoomContainer.style.marginTop = "-900px";
        }
        zoomContainer.style.marginLeft = "-" + mousePositionX + "px";
    }
}
mainImage.onmouseleave = function () {
    mainImage.style.width = "450px";
    mainImage.style.height = "450px";
    zoomContainer.style.marginTop = "0";
    zoomContainer.style.marginLeft = "0";
}