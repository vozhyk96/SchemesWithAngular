/*
 * główny plik z definicją canvasa
 */
$(document).ready(function () {
    var myjson;
    var canvas = new fabric.Canvas('canvas');
    document.getElementById("canvas").fabric = canvas;
    /*------- BACKGROUND -----------*/
    var src = '/editorApp/img/bg.png';
    var grid = 10;
    var width = document.getElementById("canvas").width;
    var height = document.getElementById("canvas").height;
    document.getElementById("canvas").click()
    canvas.setBackgroundColor({ source: src, repeat: 'repeat' }, function () {
        document.getElementById("canvas").click()
        canvas.renderAll();
    });
    

    

    // create grid
    $.ajax({
        type: "GET",
        url: "/Post/MyApp",
        success: function (response) {
            canvas.loadFromJSON(response);
        },
        error: function () {
            
        }
    });
    canvas.renderAll();
});


