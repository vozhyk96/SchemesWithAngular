/*
 * główny plik z definicją canvasa
 */

$(document).ready(function () {

    var canvas = new fabric.Canvas('canvas');
    document.getElementById("canvas").fabric = canvas;
    /*------- BACKGROUND -----------*/
    var src = 'img/bg.png';
    //canvas.setBackgroundColor({source: src, repeat: 'repeat'}, function () {
    //  canvas.renderAll();
    //});
    var grid = 10;
    var width = document.getElementById("canvas").width;
    var height = document.getElementById("canvas").height;

    // create grid

    for (var i = 0; i < (width / grid) ; i++) {
        canvas.add(new fabric.Line([i * grid, 0, i * grid, height], { stroke: '#ccc', selectable: false }));
    }
    for (var i = 0; i < (height / grid) ; i++) {
        canvas.add(new fabric.Line([0, i * grid, width, i * grid], { stroke: '#ccc', selectable: false }))
    }

    canvas.renderAll();
});

